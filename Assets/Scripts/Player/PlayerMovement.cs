using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    CharacterController characterController;

    [Header("Look")]
    public float lookSensitivity = 0.5f; // matches your old "* .5f"
    float xRotation = 0f;
    float yRotation = 0f;

    [Header("Vertical / Gravity")]
    public float gravity = -9.81f;
    public float jumpForce = 5f;
    public float yVel = 0f;

    [Header("Horizontal Movement (Megabonk-style momentum)")]
    // How quickly you ramp up to full speed on the ground.
    public float groundAcceleration = 60f;
    // How quickly you ease to a stop on the ground with no input. Higher = snappier stop.
    public float groundFriction = 55f;
    // You can barely steer in the air...
    public float airAcceleration = 15f;
    // ...and you barely slow down, so momentum carries you (this is the "air resistance").
    public float airResistance = 4f;

    // Current horizontal velocity (y is always 0). This is what holds your momentum.
    public Vector3 horizontalVelocity = Vector3.zero;

    // kept around in case anything else still reads these
    public float xVel = 0f;
    public float zVel = 0f;

    public Player player;

    // ---------- read by HandsUI (and anything that cares how fast we're going) ----------
    public float HorizontalSpeed => horizontalVelocity.magnitude;
    public float CurrentMaxSpeed => player != null ? player.stats[PlayerStat.Speed] : 0f;
    // 0 when standing still, 1 at full speed. Perfect for driving UI / animation.
    public float NormalizedSpeed =>
        CurrentMaxSpeed > 0.001f ? Mathf.Clamp01(HorizontalSpeed / CurrentMaxSpeed) : 0f;

    void Start()
    {
        player = GetComponent<Player>();
        characterController = GetComponent<CharacterController>();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        HandleLook();
        HandleMovement();
    }

    void HandleLook()
    {
        if (Time.timeScale == 0f) return;

        xRotation += -Mouse.current.delta.value.y;
        yRotation += Mouse.current.delta.value.x;
        xRotation = Mathf.Clamp(xRotation, -179f, 179f);
        Player.cam.transform.eulerAngles = new Vector3(xRotation, yRotation, 0f) * lookSensitivity;
    }

    void HandleMovement()
    {
        bool grounded = characterController.isGrounded;

        // ---- vertical ----
        if (grounded && yVel < 0f)
            yVel = -2f; // small downward stick force; stops isGrounded from flickering

        if (grounded && Keyboard.current.spaceKey.isPressed)
            yVel = jumpForce;

        yVel += gravity * Time.deltaTime;

        // ---- horizontal (camera-relative) ----
        Vector3 forward = Player.cam.transform.forward;
        forward.y = 0f;
        forward.Normalize();

        Vector3 right = Player.cam.transform.right;
        right.y = 0f;
        right.Normalize();

        Vector3 wishDir = Vector3.zero;
        if (Keyboard.current.wKey.isPressed) wishDir += forward;
        if (Keyboard.current.sKey.isPressed) wishDir -= forward;
        if (Keyboard.current.dKey.isPressed) wishDir += right;
        if (Keyboard.current.aKey.isPressed) wishDir -= right;
        if (wishDir.sqrMagnitude > 1f) wishDir.Normalize(); // no diagonal speed boost

        float maxSpeed = CurrentMaxSpeed;

        if (wishDir.sqrMagnitude > 0.0001f)
        {
            // input held -> accelerate toward the direction we want
            Vector3 target = wishDir * maxSpeed;
            float accel = grounded ? groundAcceleration : airAcceleration;
            horizontalVelocity = Vector3.MoveTowards(horizontalVelocity, target, accel * Time.deltaTime);
        }
        else
        {
            // no input -> friction (ground) or air resistance (air) eases us to a smooth stop
            float decel = grounded ? groundFriction : airResistance;
            horizontalVelocity = Vector3.MoveTowards(horizontalVelocity, Vector3.zero, decel * Time.deltaTime);
        }

        xVel = horizontalVelocity.x;
        zVel = horizontalVelocity.z;

        // ---- apply it all in a single move ----
        Vector3 velocity = horizontalVelocity + Vector3.up * yVel;
        characterController.Move(velocity * Time.deltaTime);
    }
}