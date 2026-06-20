using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Weapon
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int swordCount = 0;
    public int projectileCount = 4;
    public List<Projectile> projectiles = new List<Projectile>();
    public override Projectile Fire()
    {
        if (swordCount < projectileCount)
        {
            Projectile p = base.Fire();
            p.index = swordCount;
            projectiles.Add(p);
            foreach (Projectile p2 in projectiles)
            {
                p2.count = projectiles.Count;
            }
            swordCount++;
        }

        return null;
    }
}
