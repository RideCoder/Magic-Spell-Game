using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class OmniShot : Weapon
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    
    public override void  Start()
    {
       
        stats = new()
    {

         { WeaponStat.FireRate, 1f },
        { WeaponStat.Damage, 1f },
        { WeaponStat.ProjectileSpeed, 25f },
        { WeaponStat.CritChance, 1.00f },
        { WeaponStat.CritDamage, 1f },
        { WeaponStat.ProjectileSize, 1f },
            {WeaponStat.Pierce, 1f }


    };
        base.Start();
    }

    public override void Tick()
    {
        projectiles.RemoveAll(proj => proj == null);
        currentCooldown -= Time.deltaTime;

        if (currentCooldown <= 0f)
        {
            Fire();
            currentCooldown = cooldown;//(cooldown / player.stats[PlayerStat.FireRate]) / stats[WeaponStat.FireRate];
        }
    }
    public override Projectile Fire()
    {


        Projectile p = base.Fire();
        p.pierce = stats[WeaponStat.Pierce];



        return null;
    }



}
