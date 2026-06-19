using UnityEngine;

public class RotateAround : ProjectileBehavior
{
    
    public override void Behavior(Projectile projectile)
    {
        
        projectile.timeExisted += Time.deltaTime * (projectile.weapon.stats[WeaponStat.ProjectileSpeed]/10f);
        projectile.rb.MovePosition(Player.cam.transform.position + new Vector3(Mathf.Cos(projectile.timeExisted),0, Mathf.Sin(projectile.timeExisted)));
    }
}
