using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Weapon
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int swordCount = 0;
 
    
    public override void  Start()
    {
       
        stats = new()
    {
      
        { WeaponStat.Damage, 1f },
        { WeaponStat.ProjectileSpeed, 25f },
        { WeaponStat.CritChance, 1.00f },
        { WeaponStat.CritDamage, 1f },
            {WeaponStat.ProjectileCount, 1 },
              {WeaponStat.ProjectileSize, 1 }


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
        
        if (swordCount < stats[WeaponStat.ProjectileCount])
        {
            Projectile p = base.Fire();
            p.index = swordCount;
            //projectiles.Add(p);
            foreach (Projectile p2 in projectiles)
            {
                p2.count = projectiles.Count;
            }
            swordCount++;
         
        }

        return null;
    }

   
}
