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
        transform.eulerAngles = new Vector3(0, Mathf.Atan2(transform.position.x - Camera.main.transform.position.x, transform.position.z - Camera.main.transform.position.z) * Mathf.Rad2Deg, 0);
        
    }

    private IEnumerator Delete()
    {
        yield return new WaitForSeconds(2.0f);
        Destroy(gameObject);
    }
        
}
