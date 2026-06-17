using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using static FastScriptReload.Examples.FunctionLibrary;

public class DamageNumber : MonoBehaviour
{

    // Update is called once per frame
    Vector3 targetPosition;
    Vector3 startPosition;
    float t;
    private void Start()
    {
        t = 0f;
        targetPosition = transform.position + 
            Vector3.up + 
            new Vector3(Random.Range(-1f,1f), Random.Range(-.25f, .25f), Random.Range(-1f, 1f));
        startPosition = transform.position;
        StartCoroutine(Delete());
    }

    float easeOutBack(float x) {
        float c1 = 1.70158f;
        float c3 = c1 + 1;

        return 1 + c3* Mathf.Pow(x - 1, 3) + c1* Mathf.Pow(x - 1, 2);
    }

void Update()
    {
        t += Time.deltaTime*.5f;

        

        transform.position = Vector3.LerpUnclamped(
            startPosition,
            targetPosition,
            easeOutBack(t)
        );
        transform.LookAt(Player.cam.transform);
        transform.Rotate(0, 180, 0);

    }

    private IEnumerator Delete()
    {
        yield return new WaitForSeconds(2.0f);
        Destroy(gameObject);
    }
        
}
