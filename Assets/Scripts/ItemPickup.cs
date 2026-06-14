using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item item;
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Player>() != null)
        {
            Item clone = Instantiate(item);
            other.gameObject.GetComponent<Player>().items.Add(clone);
            Destroy(gameObject);
        }
    }
}
