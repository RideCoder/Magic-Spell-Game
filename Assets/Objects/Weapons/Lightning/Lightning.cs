using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Lightning : Weapon
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created



    public override Projectile Fire()
    {



        Projectile clone = Instantiate(projectile, player.enemyLookingAt.transform.position+new Vector3(0,0,0), Quaternion.identity);
        foreach (IOnWeaponFire item in player.items.OfType<IOnWeaponFire>())
        {
            item.OnWeaponFire(this);
        }

        foreach (IProjectileEffect effect in player.items.OfType<IProjectileEffect>())
        {
            clone.items.Add(effect);
        }
        clone.weapon = this;
        clone.critChance = (stats[WeaponStat.CritChance] * player.stats[PlayerStat.CritChance]) - 1;
        clone.critDamage = stats[WeaponStat.CritDamage] * player.stats[PlayerStat.CritDamage];
        clone.transform.localScale *= stats[WeaponStat.ProjectileSize];
        clone.damage = stats[WeaponStat.Damage] * player.stats[PlayerStat.Damage];
        clone.direction = new Vector3(0, 0, 0);
        projectiles.Add(clone);
        return clone;
    }

}
