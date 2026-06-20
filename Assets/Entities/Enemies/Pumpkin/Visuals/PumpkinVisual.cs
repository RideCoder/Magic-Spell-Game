using UnityEngine;

public class PumpkinVisual : MonoBehaviour
{
    public float flashTime = .1f;
    public float flashTimer = .1f;
    public bool flashing = false;
    public Color flashColor = Color.white;
    private Enemy enemy;
    private float currentTime = 0f;
       public void Awake()
    {
        enemy = GetComponentInParent<Enemy>();
    }
    public void Start()
    {
        enemy.OnThisEnemyDamaged += Damaged;
        enemy.OnThisEnemyDeath += Death;
    }
    public void Update()
    {
        currentTime += Time.deltaTime;
        GetComponent<MeshRenderer>().material.SetFloat("_Flash_Amount", 0);
        if (flashing)
        {
            GetComponent<MeshRenderer>().material.SetFloat("_Flash_Amount", flashTime * 10);
            flashTime -= Time.deltaTime;

        }
        if (flashTime <= 0)
        {
            flashTime = flashTimer;
            flashing = false;

        }

        transform.localPosition = new Vector3(0, Mathf.Sin(currentTime*5f)/6f, 0);
    }

    public void Damaged(float damage)
    {
        flashing = true;
    }

    public void Death()
    {
       
            GameObject clone = Instantiate(enemy.deathAnimation, transform.position, transform.rotation);
        
       
    }


}
