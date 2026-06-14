using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class PlayerMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    CharacterController characterController;
  
    public float gravity = -9.81f;
    public float yVel = 0f;
    public float xVel = 0f;
    public float zVel = 0f;
    float xRotation = 0f;
    float yRotation = 0f;
    public float speed = 5f;
    public Vector2 oldMouseVel = Vector2.zero;
    public GameObject img;
    float imgx;
    float imgy;
    void Start()
    {
        imgx = img.GetComponent<RectTransform>().anchoredPosition.x;
        imgy = img.GetComponent<RectTransform>().anchoredPosition.y;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
       
        if (characterController.velocity.x != 0f || characterController.velocity.z != 0f)
        {
            img.GetComponent<RectTransform>().anchoredPosition = new Vector3(imgx + -90f * Mathf.Cos(Time.time * 10f), imgy- 120f * Mathf.Abs(Mathf.Sin(Time.time * 10f)), 0);
        }
        else
        {
            img.GetComponent<RectTransform>().anchoredPosition = new Vector3(imgx, imgy, 0);
            
        }
            xRotation += -Mouse.current.delta.value.y;
            yRotation += Mouse.current.delta.value.x;
         xRotation = Mathf.Clamp(xRotation, -179f, 179f);
         Camera.main.transform.eulerAngles = new Vector3(xRotation, yRotation, 0)*.5f;
          

        if (!characterController.isGrounded)
        {
            yVel += (gravity*Time.deltaTime);
        }
        else
        {
            yVel = 0f;
        }



            characterController.Move(new Vector3(0, yVel*Time.deltaTime, 0));


        Vector3 forward = Camera.main.transform.forward;
        forward.y = 0;
        forward.Normalize();

        Vector3 right = Camera.main.transform.right;
        right.y = 0;
        right.Normalize();

        Vector3 move = Vector3.zero;

        if (Keyboard.current.wKey.isPressed) move += forward;
        if (Keyboard.current.sKey.isPressed) move -= forward;
        if (Keyboard.current.aKey.isPressed) move -= right;
        if (Keyboard.current.dKey.isPressed) move += right;
        if (Keyboard.current.spaceKey.isPressed && characterController.isGrounded)
        {
            yVel = 5f;
        }
        if (move.sqrMagnitude > 1)
            move.Normalize(); // prevents diagonal speed boost

        characterController.Move(move * Time.deltaTime * speed);
    }
}
