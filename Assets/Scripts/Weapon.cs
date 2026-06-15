using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    public Projectile projectile;
    public float cooldown = .01f;
    public float currentCooldown = .01f;
    public float projectileSpeed = 50f;
    public float critChance = 0.04f;
    public float critDamage = 2f;
    public Texture weaponImage;
    public Player player;
    
    
    public void Tick()
    {
       
        currentCooldown -= Time.deltaTime;
       
        if (currentCooldown <= 0f)
        {
            Fire();
            currentCooldown = cooldown/player.stats[PlayerStat.FireRate];
        }
    }
    public void Fire()
    {


        
        Projectile clone = Instantiate(projectile,Player.cam.transform.position,Quaternion.identity);
        foreach (IOnWeaponFire item in player.items.OfType<IOnWeaponFire>())
        {
            item.OnWeaponFire(this);
        }

        foreach (IProjectileEffect effect in player.items.OfType<IProjectileEffect>())
        {
            clone.items.Add(effect);
        }
        clone.weapon = this;
        clone.damage = projectile.damage * player.stats[PlayerStat.Damage];
        clone.critChance = critChance * player.stats[PlayerStat.CritChance];
        clone.critDamage = critDamage * player.stats[PlayerStat.CritDamage];
        clone.direction = player.aimPosition.normalized  * 50f ;

    }
}
