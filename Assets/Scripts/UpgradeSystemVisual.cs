using UnityEditor.Rendering.Universal;
using UnityEngine;

public class UpgradeSystemVisual : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject ui;
    void Start()
    {
        UpgradeSystem.OnStatusChange += StatusChange;
    }

    public void StatusChange(bool status)
    {
        ui.SetActive(status);
    }
}
