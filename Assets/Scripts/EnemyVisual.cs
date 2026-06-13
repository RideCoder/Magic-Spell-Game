using UnityEngine;

public class EnemyVisual : MonoBehaviour
{
    public float flashTime = .1f;
    public float flashTimer = .1f;
    public bool flashing = false;
    public Color flashColor = Color.white;
    private Enemy enemy;
       public void Awake()
    {
        enemy = GetComponentInParent<Enemy>();
    }
    public void Start()
    {
        enemy.OnThisEnemyDamaged += Damaged;
    }
    public void Update()
    {
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

        transform.localPosition = new Vector3(0, Mathf.Sin(Time.time*5f)/6f, 0);
    }

    public void Damaged(float damage)
    {
        flashing = true;
    }


}
