using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Vector3 direction;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody>().MovePosition(transform.position + direction);
        transform.eulerAngles = new Vector3(0, Mathf.Atan2(transform.position.x - Camera.main.transform.position.x, transform.position.z - Camera.main.transform.position.z) * Mathf.Rad2Deg, 0);
    }
 

    private void OnTriggerEnter(Collider collision)
    {
        
        if (collision.gameObject.layer == 0)
        {
            if (collision.gameObject.GetComponent<IDamageable>() != null)
            {
                
                collision.gameObject.GetComponent<IDamageable>().TakeDamage(5);
            }
          
            Destroy(gameObject);
        }
    }
}
