using System;
using System.Collections;
using UnityEngine;

public class Upgrade : MonoBehaviour
{

    public PlayerStat stat;
    public float change;

    public void RandomizeStats()
    {
        Array values = Enum.GetValues(typeof(PlayerStat));


        int randomIndex = UnityEngine.Random.Range(0, values.Length);


        stat = (PlayerStat)values.GetValue(randomIndex);
        change = UnityEngine.Random.Range(1.05f, 1.16f);

    }
}
