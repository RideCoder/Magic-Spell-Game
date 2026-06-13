using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
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


        bool lookingAtEnemy = false;
        foreach (Enemy enemy in EnemyManager.Instance.enemies)
        {
            if (IsVisible(enemy))
            {
              

                Vector3 offset = enemy.transform.position - Camera.main.transform.position;

                if (offset.sqrMagnitude < closest)
                {
                    aimPosition = offset;
                    closest = offset.sqrMagnitude;
                }
                lookingAtEnemy = true;
            }

            
        }
        if (lookingAtEnemy)
        {
            
            foreach (var weapon in weapons)
            {
                weapon.Tick();
            }
        }
    }


    private bool IsVisible(Enemy obj)
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);

        return planes.All(plane => plane.GetDistanceToPoint(obj.transform.position) >= 0);

    }
}
