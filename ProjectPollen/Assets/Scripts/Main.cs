using UnityEngine;
using System.Collections;
using System.Diagnostics;
using System.Timers;
using System;

public class Main : MonoBehaviour {

	string elapsedTime;
	Stopwatch stopwatch;

	// Use this for initialization
	void Start () {
		stopwatch = new Stopwatch();
		stopwatch.Start();

	}
	
	// Update is called once per frame
	void Update () {
		TimeSpan ts = stopwatch.Elapsed;

		elapsedTime = String.Format("{0:00}:{1:00}",
			ts.Minutes, ts.Seconds);
	}

	/*
	Generate new list of pollen to be achieved
	*/
	void newPollenList() {
		
	}

	void OnGUI () {
	
	}//end onGui

}
