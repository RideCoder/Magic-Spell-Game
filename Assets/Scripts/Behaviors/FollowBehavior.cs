using UnityEngine;

public class FollowBehavior : EnemyBehavior
{
    public float followSpeed = 4f;


    public override void Behavior(Enemy e)
    {
        if (e.GetComponent<Collider>() == null)
        {
            return;
        }

       
        e.GetComponent<CharacterController>().Move(-e.transform.forward * Time.deltaTime * followSpeed);
        e.transform.eulerAngles = new Vector3(0, Mathf.Atan2(e.transform.position.x - Camera.main.transform.position.x, e.transform.position.z - Camera.main.transform.position.z) * Mathf.Rad2Deg, 0);
    }
}
