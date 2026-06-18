using System.Collections;
using UnityEngine;

public class XPOrb : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
   
    Vector3 targetPosition;
    Vector3 startPosition;
    float t;
    bool start = false;
    private PlayerLevel playerLevel;

  
    private void Start()
    {
        t = 0f;
        playerLevel = FindFirstObjectByType<PlayerLevel>();
        targetPosition = Player.cam.transform.position - Vector3.up;
        

    }

    float easeInBack(float x)
    {
        float c1 = 1.70158f;
        float c3 = c1 + 1;

        return c3 * x * x * x - c1 * x * x;
    }

    // Update is called once per frame
    void Update()
    {
        if ((Camera.main.transform.position-transform.position).magnitude <= 3.5f)
        {
            
            if (!start)
            {
                startPosition = transform.position;
            }
            start = true;
        }
        if (start)
        {
            t += Time.deltaTime*2;

            targetPosition = Player.cam.transform.position - Vector3.up;

            transform.position = Vector3.LerpUnclamped(
                startPosition,
                targetPosition,
                easeInBack(t)
            );
            //transform.LookAt(Player.cam.transform);
            // transform.Rotate(0, 180, 0);
            
           if (t >= 1f)
            {

                playerLevel.AddXp(2);
                Destroy(gameObject);
            }
        }

    }
  

   
         
        
    
}
