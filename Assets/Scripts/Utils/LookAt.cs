using UnityEngine;

public class LookAt : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
  
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Player.cam.transform);
        // transform.eulerAngles = new Vector3(0, Mathf.Atan2(transform.position.x - Player.cam.transform.position.x, transform.position.z - Player.cam.transform.position.z) * Mathf.Rad2Deg, 0);
    }
}
