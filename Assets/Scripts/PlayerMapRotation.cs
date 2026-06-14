using UnityEngine;

public class PlayerMapRotation : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles = new Vector3(0, 0, -Camera.main.transform.eulerAngles.y+270);
    }
}
