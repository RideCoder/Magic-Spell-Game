using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandsUI : MonoBehaviour
{
    public GameObject handImage;
    public List<GameObject> images = new List<GameObject>();
    public List<Weapon> weapons = new List<Weapon>();
    public Player player;
    public Dictionary<GameObject, Vector3> handPositions = new Dictionary<GameObject, Vector3>();

    [Header("Hand bob")]
    // How fast the hands swing at full running speed.
    public float bobFrequency = 10f;
    // Swing distance (in UI units) at full speed.
    public float horizontalBob = 60f;
    public float verticalBob = 60f;
    // How quickly the bob ramps up when you start and eases out when you stop.
    // Higher = snappier, lower = floatier. This is what makes stopping smooth.
    public float bobSmoothing = 8f;

    PlayerMovement movement;
    float bobPhase = 0f;     // advances only while moving
    float smoothedSpeed = 0f; // eased 0..1, drives both bob speed AND amplitude

    void Awake()
    {
        Player.OnHandAdded += HandAdded;
        Player.OnWeaponAdded += WeaponAdded;
    }

    void OnDestroy()
    {
        // avoid leaking subscriptions / null callbacks on reload
        Player.OnHandAdded -= HandAdded;
        Player.OnWeaponAdded -= WeaponAdded;
    }

    void Start()
    {
        if (player != null)
            movement = player.GetComponent<PlayerMovement>();
    }

    public void Update()
    {
        // 0 when still, 1 at full speed - taken straight from the player's momentum.
        float targetSpeed = movement != null ? movement.NormalizedSpeed : 0f;

        // Frame-rate independent easing. When you stop, smoothedSpeed glides to 0,
        // so the bob shrinks back to the rest pose instead of snapping off.
        smoothedSpeed = Mathf.Lerp(smoothedSpeed, targetSpeed, 1f - Mathf.Exp(-bobSmoothing * Time.deltaTime));

        // Phase moves faster the faster you go, and freezes when you're stopped.
        bobPhase += Time.deltaTime * bobFrequency * smoothedSpeed;

        foreach (var hand in images)
        {
            if (hand == null) continue;

            // Multiplying the whole offset by smoothedSpeed is the trick: it scales the
            // swing down to zero as you slow, guaranteeing a smooth return to neutral.
            Vector3 offset = new Vector3(
                Mathf.Cos(bobPhase) * horizontalBob,
                -Mathf.Abs(Mathf.Sin(bobPhase)) * verticalBob,
                0f) * smoothedSpeed;

            hand.GetComponent<RectTransform>().anchoredPosition = handPositions[hand] + offset;
        }
    }

    public void WeaponAdded(List<Weapon> p_weapons, Weapon weapon, int num)
    {
        images[num - 1].GetComponent<RawImage>().texture = weapon.weaponImage;
        weapons = p_weapons;
    }

    public void HandAdded(List<Hand> hands)
    {
        handPositions.Clear();
        foreach (GameObject hand in images)
        {
            Destroy(hand);
        }
        images.Clear();
        Debug.Log(hands.Count);
        int handNum = 1;
        float yPos = 0f;
        float height = 850;
        if (hands.Count > 4)
        {
            height /= (Mathf.Ceil((float)hands.Count / 2));
        }
        else
        {
            height /= 2;
        }

        foreach (Hand hand in hands)
        {
            GameObject clone = Instantiate(handImage, transform);

            if (handNum % 2 == 0)
            {
                Debug.Log(yPos);
                clone.transform.localScale = new Vector2(-1, 1);
                clone.GetComponent<RectTransform>().sizeDelta = new Vector2(height, height);
                clone.GetComponent<RectTransform>().anchoredPosition = new Vector2(-1920, yPos);
                yPos += clone.GetComponent<RectTransform>().sizeDelta.y;
            }
            else
            {
                clone.GetComponent<RectTransform>().sizeDelta = new Vector2(height, height);
                clone.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, yPos);
            }
            //Fix later
            //clone.GetComponent<RawImage>().texture = hand.image.texture;
            images.Add(clone);
            handPositions.Add(clone, clone.GetComponent<RectTransform>().anchoredPosition);

            if (weapons.Count >= handNum)
            {
                Debug.Log(weapons.Count + " " + handNum);
                images[handNum - 1].GetComponent<RawImage>().texture = weapons[handNum - 1].weaponImage;
            }

            handNum++;
        }
    }
}