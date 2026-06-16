using UnityEngine;

public class FollowBehavior : EnemyBehavior
{
    public float followSpeed = 4f;


    public override void Behavior(Enemy e)
    {
        if (e.rb == null) return;

        Vector3 dir = Player.cam.transform.position - e.rb.position;
        dir.y = 0f;
        float dist = dir.magnitude;
        if (dist < 0.01f) return;

        Quaternion targetRot = Quaternion.LookRotation(dir, Vector3.up);
        Quaternion newRot = Quaternion.Slerp(e.rb.rotation, targetRot, Time.deltaTime * 10f);

        Vector3 newPos = e.rb.position;
        if (dist > 1f)
        {
            float step = Mathf.Min(followSpeed * Time.deltaTime, dist - 1f);
            newPos = e.rb.position + (newRot * Vector3.forward) * step;
        }

        e.rb.Move(newPos, newRot);
    }
}
