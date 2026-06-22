using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropSystem : MonoBehaviour
{

    // Update is called once per frame
    public List<GameObject> drops = new List<GameObject>();
    private void Start()
    {
        Enemy.OnDeath += Drop;
    }
  
    public void Drop(Enemy enemy)
    {
        GameObject dropClone = Instantiate(drops[0]);
        dropClone.transform.position = enemy.transform.position;
        if (Random.Range(0, 50) == 5)
        {
            GameObject dropClone2 = Instantiate(drops[1]);
            dropClone2.transform.position = enemy.transform.position;
        }
    }

}
