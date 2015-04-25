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

		GameObject.Find("Counter").GetComponent<TextMesh>().text = elapsedTime;
	}

	/*
	Generate new list of pollen to be achieved
	*/
	void newWorkList() {
		//get 5 random numbers mod 8, use those to get sprite
	}

	/*
	Check if pollen has been absorbed in order, and how many
	Filter down colors and generate new ones at the end
	*/
	void updateWorkList() {

		
	}

	void OnGUI () {
	
	}//end onGui

}
