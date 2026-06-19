using UnityEngine;

public class Forward : ProjectileBehavior
{
    public override void Behavior(Projectile projectile)
    {
        projectile.rb.MovePosition(projectile.transform.position + projectile.direction * .01f);
    }
}
