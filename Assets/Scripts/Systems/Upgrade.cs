using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Upgrade : MonoBehaviour
{

    public PlayerStat stat;
    public float change;
    public Player player;
    public Weapon weapon;
    public void Start()
    {
        player = FindFirstObjectByType<Player>();
        RandomizeStats();
        
    }
    public void RandomizeStats()
    {
        Array values = Enum.GetValues(typeof(PlayerStat));

        player = FindFirstObjectByType<Player>();
        int randomIndex = UnityEngine.Random.Range(0, values.Length);
        weapon = player.weapons[UnityEngine.Random.Range(0,player.weapons.Count)]; 

        stat = (PlayerStat)values.GetValue(randomIndex);
        change = UnityEngine.Random.Range(1.05f, 1.16f);

        GetComponentInChildren<TMP_Text>().text = weapon.weaponName+ " "+stat.ToString() + " "+ change.ToString() + "%";  

    }
}
