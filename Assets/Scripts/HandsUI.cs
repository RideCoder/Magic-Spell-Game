using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandsUI : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject handImage;
    public List<GameObject> images = new List<GameObject>();
    void Start()
    {
        Player.OnHandAdded += HandAdded;
    }

    public void HandAdded(List<Hand> hands)
    {
        foreach (GameObject hand in images)
        {
            
            Destroy(hand);
        }
        images.Clear();
        int handNum = 1;
        foreach (Hand hand in hands)
        {
           
            
            GameObject clone = Instantiate(handImage,transform);
            if (handNum % 2 == 0)
            {
                
                clone.transform.localScale = new Vector2(-1, 1);
                clone.GetComponent<RectTransform>().anchoredPosition = new Vector2(-1920,0);
            }
            //Fix later
            //clone.GetComponent<RawImage>().texture = hand.image.texture;
            images.Add(clone);
            handNum++;
        }
    }
}
