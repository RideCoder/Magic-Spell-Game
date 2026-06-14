using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public Weapon weapon;
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Player>() != null)
        {
            Weapon clone = Instantiate(weapon);
            other.gameObject.GetComponent<Player>().AddWeapon(clone);
            Destroy(gameObject);
        }
    }
}
