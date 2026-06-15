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
        OnStatusChange?.Invoke(true);

    }

    public void ApplyUpgrade(PlayerStat stat, float change)
    {
        OnStatusChange?.Invoke(false);
        player.stats[stat] *= change;
        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

    }
   
}
