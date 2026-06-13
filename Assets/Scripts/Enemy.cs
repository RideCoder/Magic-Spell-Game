using System;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float health = 100f;
    public EnemyBehavior behavior;

    public static event Action<Enemy, float> OnDamaged;
    public event Action<float> OnThisEnemyDamaged;
    void Start()
    {
        
    }

    // Update is called once per frame
    public void Tick()
    {
        behavior.Behavior(this);
       
    }

    public void TakeDamage(float damage)
    {
        OnDamaged?.Invoke(this, damage);
        OnThisEnemyDamaged(damage);
        health -= damage;
        if (health <= 0f)
        {
            EnemyManager.Instance.RemoveEnemy(this);
        }
    }
}
