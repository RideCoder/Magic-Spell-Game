


using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[Serializable]
public class WaveInstance
{
    public Enemy enemy;
    public float spawnTime;
    public float time;
    public float spawnAmount;


    public void Spawn()
    {

        for (int i = 0; i < spawnAmount; i++)
        {
           
            Vector2 randomDir = UnityEngine.Random.insideUnitCircle.normalized;
            float distance = UnityEngine.Random.Range(10f, 15f);
            Vector3 spawnPos = Player.cam.transform.position +
                   new Vector3(randomDir.x, 0, randomDir.y) * distance;

            EnemyManager.Instance.SpawnEnemy(enemy, spawnPos);
            
          
        }

    }
}

[Serializable]
public class Wave
{
    public float duration;
    public List<WaveInstance> waveInstances = new List<WaveInstance>();
}

public class WaveSystem : MonoBehaviour
{
    public List<Wave> waves = new List<Wave>();
    public int maxWaves;
    public static event Action<float> waveDuration;
    public int currentWave;
    public float timer = 30f;
    public void Start()
    {
        maxWaves = waves.Count;
        currentWave = 1;
        timer = waves[currentWave-1].duration;


    }

    public void Update()
    {
        timer -= Time.deltaTime;
        
        waveDuration?.Invoke(timer);
        if (currentWave > maxWaves)
            return;

        foreach (WaveInstance wave in waves[currentWave - 1].waveInstances)
        {
            wave.time += Time.deltaTime;
            if (wave.time >= wave.spawnTime)
            {
                wave.time = 0;
                wave.Spawn();
            }
        }

        if (timer <= 0f)
        {

            if (currentWave < maxWaves)
            {
                currentWave++;

           
                timer = waves[currentWave - 1].duration;
            }
            
           
        }
        /*waves[currentWave - 1].duration -= Time.deltaTime;

        if (waves[currentWave-1].duration <= 0)
        {
            currentWave++;
            EnemySystem.Instance.NextWave(currentWave);
        }*/

    }
}
