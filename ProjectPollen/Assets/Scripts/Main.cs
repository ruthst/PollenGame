using UnityEngine;
using System.Collections;
using System.Diagnostics;
using System;

public class Main : MonoBehaviour {

	string elapsedTime;
	Stopwatch stopwatch;
	int score;

	// Use this for initialization
	void Start () {
		stopwatch = new Stopwatch();
		stopwatch.Start();
		score = 0;
	}
	
	// Update is called once per frame
	void Update () {
		TimeSpan ts = stopwatch.Elapsed;

		elapsedTime = String.Format("{0:00}:{1:00}",
			ts.Minutes, ts.Seconds);

		score = GameObject.Find("WorkList").GetComponent<WorkList>().score;
		GameObject.Find("Counter").GetComponent<TextMesh>().text = score.ToString();
	}

	void OnGUI () {
	
	}//end onGui

}
