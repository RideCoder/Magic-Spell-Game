using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.UI;


public enum UpgradeType {
    Multiplicative,
    Additive
}


public class Upgrade : MonoBehaviour
{

    public WeaponStat stat;
    public float newValue;
    public Player player;
    public Weapon weapon;
    public UpgradeSystem upgSystem;
    public bool addWeapon = false;
    
    private Dictionary<WeaponStat, UpgradeType> types = new Dictionary<WeaponStat, UpgradeType>()
        {
            {WeaponStat.ProjectileSpeed, UpgradeType.Multiplicative},
            {WeaponStat.CritDamage, UpgradeType.Multiplicative},
            {WeaponStat.FireRate, UpgradeType.Multiplicative},
            {WeaponStat.Damage, UpgradeType.Multiplicative},
            {WeaponStat.CritChance, UpgradeType.Multiplicative},
            {WeaponStat.ProjectileCount, UpgradeType.Additive},
            {WeaponStat.ProjectileSize, UpgradeType.Multiplicative},
            {WeaponStat.Pierce, UpgradeType.Additive},
        };
   
    //maybe make it so that if you have a lot of hands you have a higher chance to get a weapon (weapon chance = free hands/(free hands + occupied hands)
    public void RandomizeStats()
    {


      
        player = FindFirstObjectByType<Player>();

        if (player.hands.Count - player.weapons.Count > 0 && UnityEngine.Random.Range(0,2)==1 && upgSystem.allowedWeapons.Count > 0)
        {
            Weapon clone = Instantiate(upgSystem.allowedWeapons[UnityEngine.Random.Range(0, upgSystem.allowedWeapons.Count)]);
            weapon = clone;
            addWeapon = true;
            GetComponentInChildren<TMP_Text>().text = weapon.weaponName;

        }
        else
        {
            //Gets random weapon from player
            addWeapon = false;
            weapon = player.weapons[UnityEngine.Random.Range(0, player.weapons.Count)];
            //Debug.Log(weapon);
            //Gets random weapon stat index from weapon chosen
            stat = weapon.stats.ElementAt(UnityEngine.Random.Range(0, weapon.stats.Count)).Key;

            Debug.Log("Original Stat" + stat);
            Debug.Log(types.Count);

            float change = 1f;
            if (types[stat] == UpgradeType.Multiplicative)
            {
                change = UnityEngine.Random.Range(1.08f, 1.16f);
                newValue = player.weapons[player.weapons.IndexOf(weapon)].stats[stat] * change;
            }
            else
            {
                change = 1;
                newValue = player.weapons[player.weapons.IndexOf(weapon)].stats[stat] + change;
            }


            GetComponentInChildren<TMP_Text>().text = weapon.weaponName + " " + stat.ToString() + " " + change.ToString() + "%";

        }

    }
}
