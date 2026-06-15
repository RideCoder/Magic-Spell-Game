using UnityEngine;
using UnityEngine.UI;

public class DamageIndicator : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public RawImage image;
    
    void Start()
    {
        image = GetComponent<RawImage>();
        Player.OnHealthUpdated += ShowIndicator;
    }
    public void Update()
    {
        image.color += new Color(0, 0, 0, -Time.deltaTime) ;
    }
    public void ShowIndicator(float health, float max, float amount)
    {
        if (amount < 0)
        {
            image.color = new Color(1f, 0, 0,.5f);
        }
    }
}
