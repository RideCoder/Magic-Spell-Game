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
           // SpawnEnemy(enemy, Camera.main.transform.position+ new Vector3(Random.Range(-5, 5),0 , Random.Range(-5, 5)));
        }
    }
   

    // Update is called once per frame
    void Update()
    {
        foreach (Enemy enemy in enemies)
        {
            enemy.Tick();
        }
        time -= Time.deltaTime;
        if (time <= 0f)
        {
            Vector2 randomDir = Random.insideUnitCircle.normalized;
            float distance = Random.Range(10f, 15f);
            Vector3 spawnPos = Camera.main.transform.position +
                   new Vector3(randomDir.x, 0, randomDir.y) * distance;

            SpawnEnemy(enemy, spawnPos);
            time = timeCooldown;
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
