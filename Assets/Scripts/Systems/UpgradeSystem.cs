using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeSystem : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public List<Button> buttons = new List<Button>();
    public Player player;
    void Start()
    {
        PlayerLevel.OnLevelChange += LeveledUp;
        foreach (Button button in buttons)
        {
            button.onClick.AddListener(
                () => ApplyUpgrade(PlayerStats.FireRate, 1)
                );
              
        }
    }

    public void LeveledUp()
    {

    }

    public void ApplyUpgrade(PlayerStats stats, float change)
    {

    }
   
}
