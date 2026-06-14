using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    public Projectile projectile;
    public float cooldown = .01f;
    public float currentCooldown = .01f;
    public Texture weaponImage;
    public Player player;
    public void Tick()
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


        
        Projectile clone = Instantiate(projectile,Camera.main.transform.position,Quaternion.identity);
        

        clone.direction = player.aimPosition.normalized  * 50f ;

    }
}
