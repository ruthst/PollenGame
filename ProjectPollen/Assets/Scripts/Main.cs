using UnityEngine;
using System.Collections;
using System.Timers;

public class Main : MonoBehaviour {

	Timer timer;
	private float sec = 0f;
	private float min = 0f;

	// Use this for initialization
	void Start () {
	
		timer = new Timer(0);
		sec = 0f;
		min = 0f;

	}
	
	// Update is called once per frame
	void Update () {
		CountUp();
	}

	/*
	Generate new list of pollen
	*/
	void newPollenList() {

		
	}

	/*
	Tweaked from 
	http://forum.unity3d.com/threads/count-up-down-timer.77382/
	*/

	void CountUp() {
		timer += Time.deltaTime;
		
		if(timer >= 1f) {
			sec++;
			timer = 0f;
		}//end if
		
		if(sec >= 60) {
			min++;
			sec = 0f;
		}//end if
		
		if(min >= 60) {
			hrs++;
			min = 0f;
		}//end if
		
		if(sec >= seconds && min >= minutes && hrs >= hours) {
			sec = seconds;
			min = minutes;
		}//end if
	}//end countUp

	void FormatTimer () {
		if(sec < 10) {
			strSec = "0" + sec.ToString();
		} else {
			strSec = sec.ToString();
		}//end if
		
		if(min < 10) {
			strMin = "0" + min.ToString();
		} else {
			strMin = min.ToString();
		}//end if
		
		if(seconds < 10) {
			strSeconds = "0" + seconds.ToString();
		} else {
			strSeconds = seconds.ToString();
		}//end if
		
		if(minutes < 10) {
			strMinutes = "0" + minutes.ToString();
		} else {
			strMinutes = minutes.ToString();
		}//end if
		
	}//end formatTimer

		/* DISPLAY TIMER */
	void OnGUI () {
		FormatTimer();
		GUI.Label(new Rect(Screen.width/2-150,Screen.height/2-45,300,90), strMin + ":" + strSec + " / " + strMinutes + ":" + strSeconds + "\n");
	}//end onGui

}
