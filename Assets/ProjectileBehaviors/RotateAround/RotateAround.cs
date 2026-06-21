using UnityEngine;

public class RotateAround : ProjectileBehavior
{

    public override void Behavior(Projectile projectile)
    {
        if (projectile.count > 0)
        {
            
           // projectile.timeExisted += Time.deltaTime * ;
            Debug.Log(projectile.weapon.stats[WeaponStat.ProjectileSpeed]);
            float angleOffset = (projectile.index / (float)projectile.count) * Mathf.PI * 2f;
            Debug.Log(projectile.count);
            float angle = (projectile.weapon.stats[WeaponStat.ProjectileSpeed] / 10f)*Time.time + angleOffset;
            Debug.Log(angleOffset);
            projectile.rb.MovePosition(
                Player.cam.transform.position - new Vector3(0, .25f, 0)
                + new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)));
            projectile.rb.MoveRotation(Quaternion.Euler(new Vector3(90,0,0)));
        }
        
    }
}
