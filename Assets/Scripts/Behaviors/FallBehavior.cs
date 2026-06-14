using UnityEngine;

public class FallBehavior : EnemyBehavior
{
    private float yVel = 0f;
    public float gravity = 9.81f;


    public override void Behavior(Enemy e)
    {
        if (e.GetComponent<Collider>() == null)
        {
            return;
        }

        if (!e.GetComponent<CharacterController>().isGrounded)
        {
            yVel -= (gravity * Time.deltaTime);
        }
        else
        {
            yVel = 0f;
        }
        e.GetComponent<CharacterController>().Move(new Vector3(0, yVel * Time.deltaTime, 0));
     
    }
}
