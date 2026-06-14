using UnityEngine;

public class Raise : Item, IOnProjectileUpdate
{
    public void OnProjectileUpdate(Projectile projectile)
    {
        projectile.transform.position += new Vector3(0, Time.deltaTime, 0);
    }
}
