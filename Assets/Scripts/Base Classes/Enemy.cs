using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float health = 100f;
    public float damage = 5f;
    public List<EnemyBehavior> behavior = new List<EnemyBehavior>();

    public static event Action<Enemy, float> OnDamaged;
    public event Action<float> OnThisEnemyDamaged;
    public static event Action<Enemy> OnDeath;
    public Rigidbody rb;

    public void Awake()
    {
        rb = GetComponent<Rigidbody>();  
    }
    // Update is called once per frame
    public void Tick()
    {
        foreach (EnemyBehavior behavior in behavior)
        {
            behavior.Behavior(this);
        }
       
       
    }

    public void TakeDamage(float damage)
    {
        OnDamaged?.Invoke(this, damage);
        OnThisEnemyDamaged(damage);
        health -= damage;
        if (health <= 0f)
        {
           
            OnDeath?.Invoke(this);
            EnemyManager.Instance.RemoveEnemy(this);
        }
    }
   
    private void OnCollisionStay(Collision collision)
    {
        
        if (collision.gameObject.GetComponent<Player>() != null)
        {
            collision.gameObject.GetComponent<Player>().ChangeHealth(-Time.deltaTime*damage);
        }
    }
}
