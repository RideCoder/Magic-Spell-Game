using UnityEngine;

public class SpeedRing : Item, IOnWeaponFire
{
    float cap = 2f;
    float amount = 0f;
    public void OnWeaponFire(Weapon weapon)
    {
        if (amount < cap)
        {
           
            amount += 0.05f;
            weapon.player.stats[PlayerStat.Speed] += 0.05f;
        }
    }
}
