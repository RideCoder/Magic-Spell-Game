using System.Collections;
using UnityEngine;

public class ProjectileParticle : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    Projectile proj;
    public ParticleSystem particleHit;
    void Start()
    {
        proj = GetComponentInParent<Projectile>();
    }

    // Update is called once per frame
    void Update()
    {
        if (proj == null)
        {
            if (particleHit != null)
            {
                ParticleSystem clone = Instantiate(particleHit);
                clone.transform.position = transform.position;
            }
            GetComponent<ParticleSystem>().Stop();
            StartCoroutine(Delete());
        }
    }

    IEnumerator Delete()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
