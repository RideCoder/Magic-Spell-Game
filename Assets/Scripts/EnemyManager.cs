using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static EnemyManager Instance;

    public List<Enemy> enemies = new List<Enemy>();

    public void Start()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
            
        }
        Instance = this;
    }
   

    // Update is called once per frame
    void Update()
    {
        foreach (Enemy enemy in enemies)
        {
            enemy.Tick();
        }
    }
}
