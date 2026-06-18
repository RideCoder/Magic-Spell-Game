using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class HandsUI : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject handImage;
    public List<GameObject> images = new List<GameObject>();
    public List<Weapon> weapons = new List<Weapon>();
    public Player player;
    public Vector3 playerPos = Vector3.zero;
    public Dictionary<GameObject, Vector3> handPositions = new Dictionary<GameObject, Vector3>();
    void Awake()
    {

        Player.OnHandAdded += HandAdded;
        Player.OnWeaponAdded += WeaponAdded;
    }
    float x = 0f;
    public void Update()
    {
        x += Time.deltaTime;
        if (playerPos.x - player.transform.position.x != 0f || playerPos.z - player.transform.position.z != 0f)
        {

            foreach (var hand in images)
            {
                hand.GetComponent<RectTransform>().anchoredPosition = handPositions[hand] + new Vector3(Mathf.Cos(x * 10f) * 60f, -Mathf.Abs(Mathf.Sin(x * 10f) * 60f), 0);
            }
        }
        else
        {
            x = 0f;
            foreach (var hand in images)
            {
                hand.GetComponent<RectTransform>().anchoredPosition = handPositions[hand] + new Vector3(Mathf.Cos(x * 10f) * 60f, -Mathf.Abs(Mathf.Sin(x * 10f) * 60f), 0);
            }
        }
        playerPos = player.transform.position;
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