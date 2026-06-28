using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class WaveInstance
{
    public string enemy;        // name/id from JSON
    public float spawnTime;
    public float time;
    public float spawnAmount;

    [NonSerialized] public Enemy enemyRef;   // resolved at load, not from JSON

    public void Spawn()
    {
        for (int i = 0; i < spawnAmount; i++)
        {
            Vector2 randomDir = UnityEngine.Random.insideUnitCircle.normalized;
            float distance = UnityEngine.Random.Range(10f, 15f);
            Vector3 spawnPos = Player.cam.transform.position +
                   new Vector3(randomDir.x, 0, randomDir.y) * distance;
            EnemyManager.Instance.SpawnEnemy(enemyRef, spawnPos);
        }
    }
}

[Serializable]
public class Wave
{
    public float duration;
    public List<WaveInstance> waveInstances = new List<WaveInstance>();
}

[Serializable]
public class WaveConfig
{
    public List<Wave> waves = new List<Wave>();
}

public class WaveSystem : MonoBehaviour
{
    [Header("JSON")]
    public TextAsset waveJson;              // drag your waves.json here

    [Header("Enemy Lookup")]
    public Enemy[] enemyPrefabs;            // assign all Enemy types here

    public List<Wave> waves = new List<Wave>();
    public int maxWaves;
    public static event Action<float> waveDuration;
    public int currentWave;
    public float timer = 30f;

    public void Start()
    {
        LoadWaves();
        maxWaves = waves.Count;
        currentWave = 1;
        timer = waves[currentWave - 1].duration;
    }

    void LoadWaves()
    {
        WaveConfig config = JsonUtility.FromJson<WaveConfig>(waveJson.text);
        waves = config.waves;

        // build name -> Enemy map
        var map = new Dictionary<string, Enemy>();
        foreach (Enemy e in enemyPrefabs)
            if (e != null) map[e.name] = e;

        // resolve every instance's enemy reference
        foreach (Wave w in waves)
            foreach (WaveInstance inst in w.waveInstances)
            {
                if (map.TryGetValue(inst.enemy, out Enemy found))
                    inst.enemyRef = found;
                else
                    Debug.LogWarning($"No Enemy prefab named '{inst.enemy}'");
            }
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
    }
}