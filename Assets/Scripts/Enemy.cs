using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public EnemyBehavior behavior;
    void Start()
    {
        
    }

    // Update is called once per frame
    public void Tick()
    {
        behavior.Behavior(this);
    }
}
