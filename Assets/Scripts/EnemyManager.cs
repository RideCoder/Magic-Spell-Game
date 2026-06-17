using NUnit.Framework;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static EnemyManager Instance;
    public Enemy enemy;
    public List<Enemy> enemies = new List<Enemy>();
    public float time = 1f;
    public float timeCooldown = 1f;
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    private static void ResetStatics()
    {
        Instance = null;
    }
    public void Start()
    {

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
            
        }
        Instance = this;
        for (int i = 0; i < 1; i++)
        {
           // SpawnEnemy(enemy, Player.cam.transform.position+ new Vector3(Random.Range(-5, 5),0 , Random.Range(-5, 5)));
        }
    }
   

    // Update is called once per frame
    void FixedUpdate()
    {
        for (int i = enemies.Count - 1; i >= 0; i--)
        {
            enemies[i].Tick();
        }
     
    }
    public void RemoveEnemy(Enemy enemy)
    {
     
        enemies.Remove(enemy);
        Destroy(enemy.gameObject);
    }

    public void SpawnEnemy(Enemy enemy, Vector3 position)
    {
        Enemy clone = Instantiate(enemy, position, Quaternion.identity);
        enemies.Add(clone);
        
    }
}
