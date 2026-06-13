using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Projectile projectile;
    public float cooldown = 1f;
    public float currentCooldown = 1f;

    public Player player;
    public void Update()
    {
       
        currentCooldown -= Time.deltaTime;

        if (currentCooldown <= 0f)
        {
            Fire();
            currentCooldown = cooldown;
        }
    }
    public void Fire()
    {
        Projectile clone = Instantiate(projectile);
        clone.transform.position = Camera.main.transform.position;
        clone.direction = player.aimPosition.normalized * Time.deltaTime *25f ;
    }
}
