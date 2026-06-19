using System;
using System.Collections;
using System.Linq;
using TMPro;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.UI;

public class Upgrade : MonoBehaviour
{

    public WeaponStat stat;
    public float change;
    public Player player;
    public Weapon weapon;
    public void Start()
    {
        player = FindFirstObjectByType<Player>();
       // RandomizeStats();
        
    }
    public void RandomizeStats()
    {
      
        player = FindFirstObjectByType<Player>();
        //Gets random weapon from player
        weapon = player.weapons[UnityEngine.Random.Range(0, player.weapons.Count)];
        //Debug.Log(weapon);
        //Gets random weapon stat index from weapon chosen
        stat = weapon.stats.ElementAt(UnityEngine.Random.Range(0,weapon.stats.Count)).Key;
        Debug.Log("Original Stat" + stat);

      
        change = UnityEngine.Random.Range(1.08f, 1.16f);

        GetComponentInChildren<TMP_Text>().text = weapon.weaponName+ " "+stat.ToString() + " "+ change.ToString() + "%";  

    }
}
