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
            Upgrade upg = button.GetComponent<Upgrade>();
            button.onClick.AddListener(
                () => ApplyUpgrade(upg.stat, upg.newValue, upg.weapon)
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

    public void ApplyUpgrade(WeaponStat stat, float change, Weapon weapon)
    {
        OnStatusChange?.Invoke(false);
        
        player.weapons[player.weapons.IndexOf(weapon)].stats[stat] = change;
        weapon.ApplyStats();
        Debug.Log("Stat: " + stat);
        Debug.Log("Stat Value New: " + player.weapons[player.weapons.IndexOf(weapon)].stats[stat]);
        //player.stats[stat] *= change;
        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

    }
   
}
