using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


public enum WeaponStat
{
    FireRate,
    Damage,
    CritChance,
    CritDamage,
    ProjectileSpeed,
}

[System.Serializable]
public struct WeaponStatEntry
{
    public WeaponStat stat;
    public float value;
}
public class Weapon : MonoBehaviour
{
    public Projectile projectile;
    public float cooldown = .01f;
    public float currentCooldown = .01f;
    public List<WeaponStatEntry> weaponStats;
    
    public Dictionary<WeaponStat, float> stats = new()
    {
        { WeaponStat.FireRate, 1f },
        { WeaponStat.Damage, 1f },
        { WeaponStat.ProjectileSpeed, 25f },
        { WeaponStat.CritChance, 1.00f },
        { WeaponStat.CritDamage, 1f },
   
    };
    public Texture weaponImage;
    public Player player;
    public string weaponName;

    public void Start()
    {
        foreach (var weapon in weaponStats)
        {
            stats[weapon.stat] = weapon.value;
        }
    }
    public virtual void Tick()
    {
       
        currentCooldown -= Time.deltaTime;
       
        if (currentCooldown <= 0f)
        {
            Fire();
            currentCooldown = (cooldown/player.stats[PlayerStat.FireRate])/stats[WeaponStat.FireRate];
        }
    }
    public virtual void Fire()
    {


        
        Projectile clone = Instantiate(projectile,(Player.cam.transform.position), Quaternion.identity);
        foreach (IOnWeaponFire item in player.items.OfType<IOnWeaponFire>())
        {
            item.OnWeaponFire(this);
        }

        foreach (IProjectileEffect effect in player.items.OfType<IProjectileEffect>())
        {
            clone.items.Add(effect);
        }
        clone.weapon = this;
        clone.critChance = (stats[WeaponStat.CritChance] * player.stats[PlayerStat.CritChance])-1;
        clone.critDamage = stats[WeaponStat.CritDamage] * player.stats[PlayerStat.CritDamage];
        
        clone.damage = stats[WeaponStat.Damage] * player.stats[PlayerStat.Damage];
        clone.direction = player.aimPosition.normalized  * stats[WeaponStat.ProjectileSpeed];

    }
}
