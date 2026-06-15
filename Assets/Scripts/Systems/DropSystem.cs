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
        if (Random.Range(0, 7) == 5)
        {
            GameObject dropClone = Instantiate(drops[Random.Range(0, drops.Count)]);
            dropClone.transform.position = enemy.transform.position;
        }
    }

}
