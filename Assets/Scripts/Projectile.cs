using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Vector3 direction;
    public Rigidbody rb;
    public float damage;
    public List<Item> items = new List<Item>();
    void Start()
    {
     rb = GetComponent<Rigidbody>();       
    }

    // Update is called once per frame
    void Update()
    {
      
        transform.eulerAngles = new Vector3(0, Mathf.Atan2(transform.position.x - Camera.main.transform.position.x, transform.position.z - Camera.main.transform.position.z) * Mathf.Rad2Deg, 0);
    }
    private void FixedUpdate()
    {
        rb.MovePosition(transform.position + direction * .01f);
    }
   
    private void OnTriggerEnter(Collider collision)
    {
        
        if (collision.gameObject.layer == 0)
        {
            if (collision.gameObject.GetComponent<IDamageable>() != null)
            {
            
                collision.gameObject.GetComponent<IDamageable>().TakeDamage(damage);
            }

     
          
            Destroy(gameObject);
        }
    }
}
