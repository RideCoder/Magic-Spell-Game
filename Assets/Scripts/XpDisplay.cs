using UnityEngine;
using UnityEngine.UI;

public class XpDisplay : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private RawImage image;
    void Start()
    {
        PlayerLevel.OnXpChange += UpdateXpDisplay;
        image = GetComponent<RawImage>();
    }

    public void UpdateXpDisplay(int oldXp, int newXp, int requiredXp)
    {
        
        image.rectTransform.sizeDelta = new Vector2(((float)newXp/(float)requiredXp)*1000,image.rectTransform.sizeDelta.y);
    }
}
