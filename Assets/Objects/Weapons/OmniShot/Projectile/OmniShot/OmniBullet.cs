using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class OmniBullet : Projectile
{

    
    public bool specialHit = true;


    public override void OnHit()
    {
        if (specialHit)
        {
            GameObject clone = Instantiate(gameObject);
            clone.transform.position += transform.right * 5f;
            var b1 = clone.GetComponent<OmniBullet>();
            b1.specialHit = false;
            b1.direction = Quaternion.Euler(0, -90, 0) * direction;

            GameObject clone2 = Instantiate(gameObject);
            clone2.transform.position -= transform.right * 5f;
            var b2 = clone2.GetComponent<OmniBullet>();
            b2.specialHit = false;
            b2.direction = Quaternion.Euler(0, 90, 0) * direction;
        }
        
        if (pierce < 1)
        {
            Destroy(gameObject);
        }
        pierce--;
       
    }


}
