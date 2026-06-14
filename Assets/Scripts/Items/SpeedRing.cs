using UnityEngine;

public class SpeedRing : Item, IOnProjectileFire
{
    public void OnProjectileFire()
    {
        Debug.Log("SPEED UP!");
    }
}
