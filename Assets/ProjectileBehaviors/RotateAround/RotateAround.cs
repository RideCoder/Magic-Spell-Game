using UnityEngine;

public class RotateAround : ProjectileBehavior
{
    public float x = 0;
    public override void Behavior(Projectile projectile)
    {
        x += Time.deltaTime*3f;
        projectile.rb.MovePosition(Player.cam.transform.position + new Vector3(Mathf.Cos(x),0, Mathf.Sin(x)));
    }
}
