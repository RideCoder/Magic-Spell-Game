using UnityEngine;

public class BobUpAndDown : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float currentTime;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        transform.localPosition = new Vector3(0, Mathf.Sin(currentTime * 5f) / 6f, 0);
    }
}
