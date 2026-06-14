using UnityEngine;

public class SpeedRing : Item, IOnWeaponFire
{
    public void OnWeaponFire()
    {
        Debug.Log("SPEED UP!");
    }
}
