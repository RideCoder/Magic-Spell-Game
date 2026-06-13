using System.Collections;
using UnityEngine;

public class DamageNumber : MonoBehaviour
{

    // Update is called once per frame
    private void Start()
    {
        StartCoroutine(Delete());
    }
    void Update()
    {
        transform.position += new Vector3(0, Time.deltaTime, 0);
        transform.LookAt(Camera.main.transform);
        transform.Rotate(0, 180, 0);

    }

    private IEnumerator Delete()
    {
        yield return new WaitForSeconds(2.0f);
        Destroy(gameObject);
    }
        
}
