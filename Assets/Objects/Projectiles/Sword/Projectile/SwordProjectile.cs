using UnityEngine;

public class SwordProjectile : Projectile
{
    public void Awake()
    {
        originalSize = 0.5f;
    }
    public override void Update()
    {
        transform.eulerAngles = new Vector3(90, Mathf.Atan2(transform.position.x - Player.cam.transform.position.x, transform.position.z - Player.cam.transform.position.z) * Mathf.Rad2Deg, 0);
    }

    public override void OnHit()
    {

    }
}