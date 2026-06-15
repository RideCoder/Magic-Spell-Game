using UnityEngine;
using UnityEngine.UI;

public class XpDisplay : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private RawImage image;
    private float imageWidth;
    void Start()
    {
        
        PlayerLevel.OnXpChange += UpdateXpDisplay;
        image = GetComponent<RawImage>();
        imageWidth = image.rectTransform.sizeDelta.x;
        image.rectTransform.sizeDelta = new Vector2(0, image.rectTransform.sizeDelta.y);
        
    }

    public void UpdateXpDisplay(int oldXp, int newXp, int requiredXp)
    {
        
        image.rectTransform.sizeDelta = new Vector2(((float)newXp/(float)requiredXp)*imageWidth,image.rectTransform.sizeDelta.y);
    }
}
