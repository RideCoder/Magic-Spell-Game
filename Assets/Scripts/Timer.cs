using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static float time = 1f;
    public int seconds = 0;
    // Update is called once per frame
   

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        if (time <= 0f)
        {
            time = 1f;
            seconds++;
        }
        gameObject.GetComponent<TMP_Text>().text = IntToText(seconds);
    }

    public string IntToText(int time)
    {

        int minutes = Mathf.FloorToInt(seconds / 60);
        return Mathf.FloorToInt(minutes / 10).ToString() + minutes.ToString() + ":" + Mathf.FloorToInt((time % 60) / 10) + Mathf.FloorToInt(time % 10);

    }
}
