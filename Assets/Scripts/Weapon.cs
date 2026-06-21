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
    ProjectileCount,
    ProjectileSize,
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
    public List<Projectile> projectiles = new List<Projectile>();
   
    public Dictionary<WeaponStat, float> stats = new()
    {
        { WeaponStat.FireRate, 1f },
        { WeaponStat.Damage, 1f },
        { WeaponStat.ProjectileSpeed, 25f },
        { WeaponStat.CritChance, 1.00f },
        { WeaponStat.CritDamage, 1f },
        { WeaponStat.ProjectileSize, 1f },
    };
    public Texture weaponImage;
    public Player player;
    public string weaponName;

    public virtual void Start()
    {
        foreach (var weapon in weaponStats)
        {
            stats[weapon.stat] = weapon.value;
        }
    }
    public virtual void Tick()
    {
        projectiles.RemoveAll(proj => proj == null);
        currentCooldown -= Time.deltaTime;
       
        if (currentCooldown <= 0f)
        {
            Fire();
            currentCooldown = (cooldown/player.stats[PlayerStat.FireRate])/stats[WeaponStat.FireRate];
        }
    }

    public void ApplyStats()
    {
        foreach (Projectile proj in projectiles)
        {
            proj.critChance = (stats[WeaponStat.CritChance] * player.stats[PlayerStat.CritChance]) - 1;
            proj.critDamage = stats[WeaponStat.CritDamage] * player.stats[PlayerStat.CritDamage];
            proj.transform.localScale = proj.originalSize* new Vector3(stats[WeaponStat.ProjectileSize], stats[WeaponStat.ProjectileSize], stats[WeaponStat.ProjectileSize]);
            proj.damage = stats[WeaponStat.Damage] * player.stats[PlayerStat.Damage];
        }
    }
    public virtual Projectile Fire()
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
        clone.transform.localScale *= stats[WeaponStat.ProjectileSize];
        clone.damage = stats[WeaponStat.Damage] * player.stats[PlayerStat.Damage];
        clone.direction = player.aimPosition.normalized  * stats[WeaponStat.ProjectileSpeed];
        projectiles.Add(clone);
        return clone;
    }
}
