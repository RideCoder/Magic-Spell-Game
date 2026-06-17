using UnityEngine;

public class Dissolve : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private float t = 1f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        t -= Time.deltaTime;
        Material material = GetComponent<MeshRenderer>().material;
        material.SetFloat("_Dissolve", t);
    }
}
