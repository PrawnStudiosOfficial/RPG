using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameTime : MonoBehaviour 
{
    public int day = 00;
    public int hour = 00;
    public int min = 00;

    private string hourString;
    private string minString;

    public string time;

    void Start () 
	{
        InvokeRepeating("Minuits", 1, 1);
    }

    public void Minuits ()
    {
        if (min + 1 <= 59)
        {
            min++;
        }
        else
        {
            if (hour + 1 <= 23)
            {
                hour++;
            }
            else
            {
                hour = 0;
                day++;
            }
            min = 0;
        }
    }

    void OnGUI ()
    {
        if(hour < 10)
        {
            hourString = "0" + hour.ToString();
        }
        else
        {
            hourString = hour.ToString();
        }

        if(min < 10)
        {
            minString = "0" + min.ToString();
        }
        else
        {
            minString = min.ToString();
        }

        time = hourString + ":" + minString;

        var style = GUI.skin.GetStyle("Label");
        style.fontSize = 14;
        GUI.Label(new Rect(0, 0, 75, 25), time);
    }
}
