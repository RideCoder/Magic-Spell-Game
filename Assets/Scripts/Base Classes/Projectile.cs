using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Vector3 direction;
    public Rigidbody rb;
    public float damage;
    public Weapon weapon;
    public float critChance = 0.04f;
    public float critDamage = 2f;
    public List<IProjectileEffect> items = new List<IProjectileEffect>();
    void Start()
    {
     rb = GetComponent<Rigidbody>();      
       // transform.eulerAngles = new Vector3(90, 0, Mathf.Atan2(transform.position.x - Player.cam.transform.position.x, transform.position.z - Player.cam.transform.position.z) * Mathf.Rad2Deg);
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.eulerAngles = new Vector3(0, Mathf.Atan2(transform.position.x - Player.cam.transform.position.x, transform.position.z - Player.cam.transform.position.z) * Mathf.Rad2Deg, 0);
    }
    private void FixedUpdate()
    {
        foreach (IOnProjectileUpdate effect in items)
        {
            effect.OnProjectileUpdate(this);
        }
        rb.MovePosition(transform.position + direction * .01f);
    }
   
    private void OnTriggerEnter(Collider collision)
    {
        
        if (collision.gameObject.layer == 0)
        {
            if (collision.gameObject.GetComponent<IDamageable>() != null)
            {
                foreach (IProjectileEffect item in items)
                {

                }
                if (Random.Range(0, 1f) <= critChance)
                {
                    collision.gameObject.GetComponent<IDamageable>().TakeDamage(damage * critDamage);
                }
                else
                {
                    collision.gameObject.GetComponent<IDamageable>().TakeDamage(damage);
                }

            }


            for (int i = transform.childCount - 1; i >= 0; i--)
            {

                Transform child = transform.GetChild(i);
                child.SetParent(null);
                child.localScale = Vector3.one;

            }
            Destroy(gameObject);
        }
    }
}
