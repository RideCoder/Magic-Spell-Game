using UnityEngine;

public class SpeedRing : Item, IOnWeaponFire
{
    float cap = 15f;
    float amount = 0f;
    public void OnWeaponFire(Weapon weapon)
    {
        if (amount < cap)
        {
            Debug.Log("What");
            amount += 0.505f;
            weapon.player.speed += 0.505f;
        }
    }
}
