using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private RawImage image;
    private float imageWidth;
    void Start()
    {
        
        Player.OnHealthUpdated += UpdateHealthDisplay;
        image = GetComponentInParent<RawImage>();
        imageWidth = image.rectTransform.sizeDelta.x;
    }

    public void UpdateHealthDisplay(float health, float maxHealth)
    {
        
        image.rectTransform.sizeDelta = new Vector2(((float)health/(float)maxHealth)*imageWidth,image.rectTransform.sizeDelta.y);
    }
}
