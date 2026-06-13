using UnityEngine;

public class FollowBehavior : EnemyBehavior
{
    public float yVel = 0f;
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
        e.GetComponent<CharacterController>().Move(-e.transform.forward * Time.deltaTime * 5);
        e.transform.eulerAngles = new Vector3(0, Mathf.Atan2(e.transform.position.x - Camera.main.transform.position.x, e.transform.position.z - Camera.main.transform.position.z) * Mathf.Rad2Deg, 0);
    }
}
