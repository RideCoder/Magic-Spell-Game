using UnityEngine;

public class Raise : Item, IOnProjectileUpdate
{
    public void OnProjectileUpdate(Projectile projectile)
    {

        projectile.direction += Vector3.up * 0.1f;
    }
}
