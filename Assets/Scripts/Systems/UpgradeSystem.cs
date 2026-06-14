using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeSystem : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public List<Button> buttons = new List<Button>();
    public static event Action<bool> OnStatusChange;
    public Player player;
    void Start()
    {
        PlayerLevel.OnLevelChange += LeveledUp;
        foreach (Button button in buttons)
        {
            button.onClick.AddListener(
                () => ApplyUpgrade(button.GetComponent<Upgrade>().stat, button.GetComponent<Upgrade>().change)
                );
              
        }
    }

    public void LeveledUp()
    {
        foreach (Button button in buttons)
        {
            button.GetComponent<Upgrade>().RandomizeStats();
        }
        OnStatusChange(true);
    }

    public void ApplyUpgrade(PlayerStat stat, float change)
    {
        OnStatusChange(false);
        player.stats[stat] *= change;
        
    }
   
}
