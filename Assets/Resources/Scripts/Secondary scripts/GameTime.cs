using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTime : MonoBehaviour {
	public int StartTime;
    public int minutes;
    public int seconds;

	public int totalseconds = 3600;
	public int additionalseconds = 0;
    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        //Displays game timer
		minutes = ((int)Time.time-(int)StartTime) / 60; // minutes is the integer part of seconds/60
		seconds = ((int)Time.time-(int)StartTime) % 60; // % is the "modulo" or "remainder" operator
		//print(minutes +"';" +seconds);
        if (seconds < 10)
        {
            GetComponent<Text>().text = minutes + ":0" + seconds;
        }
        else
        {
            GetComponent<Text>().text = minutes + ":" + seconds;
        }

    }

	public void ResetStartTime()
	{
		StartTime = (int)Time.time;
	}
}
