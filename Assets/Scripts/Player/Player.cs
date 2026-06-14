using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;


public enum PlayerStat
{
    FireRate,
    Damage,
    Health,
    MaxHealth,
    CritChance,
    CritDamage
}
public class Player : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public List<Weapon> weapons;
    public Vector3 aimPosition;
    public float speed;
    public float health;
    public Dictionary<PlayerStat, float> stats = new()
    {
        { PlayerStat.FireRate, 1f },
        { PlayerStat.Damage, 1f },
        { PlayerStat.MaxHealth, 100f },
        { PlayerStat.CritChance, 0.04f },
        { PlayerStat.CritDamage, 2f }
    };
    public List<Item> items = new List<Item>();
    public List<Hand> hands = new List<Hand>();




    public void AddWeapon(Weapon weapon)
    {
        if (hands.Count > weapons.Count)
        {
            hands[weapons.Count].image.texture = weapon.weaponImage;
            weapon.player = this;
            weapons.Add(weapon);
            
        }
    }
    void Start()
    {
       
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
