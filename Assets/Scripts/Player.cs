using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public List<Weapon> weapons;
    public Vector3 aimPosition;

    void Start()
    {
        foreach (var weapon in weapons)
        {
            weapon.player = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float closest = Mathf.Infinity;
        foreach (Enemy enemy in EnemyManager.Instance.enemies)
        {
            Vector3 offset = enemy.transform.position - Camera.main.transform.position;
            
            if (offset.sqrMagnitude < closest)
            {
                aimPosition = offset;
                closest = offset.sqrMagnitude;
            }
        }
        foreach (var weapon in weapons)
        {
            weapon.Tick();
        }
    }
}
