using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandsUI : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject handImage;
    public List<GameObject> images = new List<GameObject>();
    public List<Weapon> weapons = new List<Weapon>();
    void Start()
    {
        Player.OnHandAdded += HandAdded;
        Player.OnWeaponAdded += WeaponAdded;
    }


    public void WeaponAdded(List<Weapon> p_weapons, Weapon weapon, int num)
    {
        images[num-1].GetComponent<RawImage>().texture = weapon.weaponImage;
        weapons = p_weapons;
    }
    public void HandAdded(List<Hand> hands)
    {
        foreach (GameObject hand in images)
        {
            
            Destroy(hand);
        }
        images.Clear();
        Debug.Log(hands.Count);
        int handNum = 1;
        float yPos = 0f;
        float height = 850;
        if (hands.Count > 4) {

          
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
            if (weapons.Count >= handNum)
            {
                Debug.Log(weapons.Count + " " + handNum);
                images[handNum - 1].GetComponent<RawImage>().texture = weapons[handNum - 1].weaponImage;
            }

            handNum++;
            }
    }
}
