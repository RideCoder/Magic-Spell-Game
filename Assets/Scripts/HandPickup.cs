using UnityEngine;

public class HandPickup : MonoBehaviour
{
    public Hand hand;
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Player>() != null)
        {
            
                
                Hand clone = Instantiate(hand);
                other.gameObject.GetComponent<Player>().AddHand(clone);
                Destroy(gameObject);
            
           
        }
    }
}
