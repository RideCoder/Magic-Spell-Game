using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Upgrade : MonoBehaviour
{

    public PlayerStat stat;
    public float change;

    public void Start()
    {
        RandomizeStats();
        
    }
    public void RandomizeStats()
    {
        Array values = Enum.GetValues(typeof(PlayerStat));


        int randomIndex = UnityEngine.Random.Range(0, values.Length);


        stat = (PlayerStat)values.GetValue(randomIndex);
        change = UnityEngine.Random.Range(1.05f, 1.16f);

        GetComponentInChildren<TMP_Text>().text = stat.ToString();  

    }
}
