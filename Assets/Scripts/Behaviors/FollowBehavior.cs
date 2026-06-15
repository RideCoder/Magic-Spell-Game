using UnityEngine;

public class FollowBehavior : EnemyBehavior
{
    public float followSpeed = 4f;


    public override void Behavior(Enemy e)
    {
        if (e.rb == null)
        {
            return;
        }

       
        e.rb.Move(e.transform.position +  (-e.transform.forward * Time.deltaTime * followSpeed), Quaternion.identity);
        e.transform.eulerAngles = new Vector3(0, Mathf.Atan2(e.transform.position.x - Player.cam.transform.position.x, e.transform.position.z - Player.cam.transform.position.z) * Mathf.Rad2Deg, 0);
    }
}
