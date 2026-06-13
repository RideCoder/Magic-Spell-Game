using TMPro;
using UnityEngine;

public class DamageNumberManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject damageNumber;
    void Start()
    {
        Enemy.OnDamaged += SpawnDamageText;
    }

    public void SpawnDamageText(Enemy e, float dmg)
    {
        GameObject clone = Instantiate(damageNumber);
        clone.transform.position = e.transform.position;
        clone.transform.parent = e.transform;
        clone.GetComponent<TMP_Text>().text = dmg.ToString();
    }
}
