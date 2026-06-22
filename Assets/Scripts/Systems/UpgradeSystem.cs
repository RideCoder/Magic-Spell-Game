using JetBrains.Annotations;
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
    public List<Weapon> allowedWeapons = new List<Weapon>();
    public Player player;
    void Start()
    {
       
        PlayerLevel.OnLevelChange += LeveledUp;
        foreach (Button button in buttons)
        {
            Upgrade upg = button.GetComponent<Upgrade>();
            upg.upgSystem = this;
            button.onClick.AddListener(
                () => ApplyUpgrade(upg.stat, upg.newValue, upg.weapon, upg.addWeapon)
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

    public void ApplyUpgrade(WeaponStat stat, float change, Weapon weapon, bool addWeapon)
    {
        OnStatusChange?.Invoke(false);
        if (addWeapon == true)
        {
            player.AddWeapon(weapon);
        }
        else
        {
            player.weapons[player.weapons.IndexOf(weapon)].stats[stat] = change;
            weapon.ApplyStats();
            Debug.Log("Stat: " + stat);
            Debug.Log("Stat Value New: " + player.weapons[player.weapons.IndexOf(weapon)].stats[stat]);


        }


        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
      


        //player.stats[stat] *= change;


    }

}
