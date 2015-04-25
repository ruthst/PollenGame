using UnityEngine;
using System.Collections;

using System.Diagnostics;

public class Main : MonoBehaviour {

	String elapsedTime;

	// Use this for initialization
	void Start () {
		Stopwatch stopwatch = new Stopwatch();
		stopwatch.Start();

	}
	
	// Update is called once per frame
	void Update () {
		Timespan ts = stopwatch.Elapsed;

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
