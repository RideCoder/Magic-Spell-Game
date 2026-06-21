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
            clone.GetComponent<OmniBullet>().specialHit = false;
            clone.GetComponent<OmniBullet>().direction += new Vector3(0, -90, 0);

            GameObject clone2 = Instantiate(gameObject);
            clone2.transform.position -= transform.right * 5f;
            clone2.GetComponent<OmniBullet>().specialHit = false;
            clone2.GetComponent<OmniBullet>().direction += new Vector3(0, 90, 0);
        }
        Destroy(gameObject);
    }
   
}
