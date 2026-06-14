using UnityEngine;
using UnityEngine.UI;

public class Hand : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    
    public RawImage image;
    void Start()
    {
        image = GetComponent<RawImage>();
       // image.texture = player.weapons[0].weaponImage;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
