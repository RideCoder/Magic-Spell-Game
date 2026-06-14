using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;


public enum PlayerStat
{
    FireRate,
    Damage,
    MaxHealth,
    CritChance,
    CritDamage,
    Speed
}
public class Player : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public List<Weapon> weapons;
    public Vector3 aimPosition;
    public static event Action<float,float> OnHealthUpdated;
    public static event Action<int, int> OnHandAdded;

    public float health;
    public Dictionary<PlayerStat, float> stats = new()
    {
        { PlayerStat.FireRate, 1f },
        { PlayerStat.Damage, 1f },
        { PlayerStat.MaxHealth, 100f },
        { PlayerStat.CritChance, 0.04f },
        { PlayerStat.CritDamage, 2f },
        { PlayerStat.Speed, 5f }
    };
    public List<Item> items = new List<Item>();
    public List<Hand> hands = new List<Hand>();

    public void ChangeHealth(float amount)
    {
        health += amount;
        OnHealthUpdated?.Invoke(health, stats[PlayerStat.MaxHealth]);
    }

    public void AddHand(Hand hand)
    {
        
        hands.Add(hand);
        OnHandAdded?.Invoke(hands.Count,1);
    }
    public bool CanAddWeapon()
    {
        if (hands.Count > weapons.Count)
        {
            return true;
        }
        return false;
    }
    public void AddWeapon(Weapon weapon)
    {
            hands[weapons.Count].image.texture = weapon.weaponImage;
            weapon.player = this;
            weapons.Add(weapon);

    }


    // Update is called once per frame
    void Update()
    {
        //Debug.Log(stats[PlayerStat.FireRate]);
        float closest = Mathf.Infinity;


        bool lookingAtEnemy = false;
        foreach (Enemy enemy in EnemyManager.Instance.enemies)
        {
            if (IsVisible(enemy))
            {
                if (Physics.Raycast(Camera.main.transform.position, enemy.transform.position - Camera.main.transform.position, out RaycastHit hit))
                {
                   
                    if (hit.collider.GetComponent<Enemy>() != null)
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
