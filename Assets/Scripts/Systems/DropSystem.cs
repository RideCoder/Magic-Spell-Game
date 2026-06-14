using System.Collections;
using UnityEngine;

public class DropSystem : MonoBehaviour
{

    // Update is called once per frame
    public GameObject drop;
    private void Start()
    {
        Enemy.OnDeath += Drop;
    }
  
    public void Drop(Enemy enemy)
    {
        GameObject dropClone = Instantiate(drop);
        dropClone.transform.position = enemy.transform.position;
    }

}
