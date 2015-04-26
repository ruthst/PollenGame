using UnityEngine;
using System.Collections;
using System.Diagnostics;
using System;

public class Main : MonoBehaviour {

	string elapsedTime;
	Stopwatch stopwatch;
	int score;
	int highScore;
	string highScoreKey = "HIGHSCORE";

	// Use this for initialization
	void Start () {
		stopwatch = new Stopwatch();
		stopwatch.Start();
		score = 0;
		highScore = PlayerPrefs.GetInt(highScoreKey,0);
	}
	
	// Update is called once per frame
	void Update () {
		TimeSpan ts = stopwatch.Elapsed;

		elapsedTime = String.Format("{0:00}:{1:00}",
			ts.Minutes, ts.Seconds);

		score = GameObject.Find("WorkList").GetComponent<WorkList>().score;
		GameObject.Find("Counter").GetComponent<TextMesh>().text = score.ToString();


	}

	void OnDestroy(){
		if(score > highScore){
			PlayerPrefs.SetInt(highScoreKey, score);
			highScore = score;	
			UnityEngine.Debug.Log("Highscore: "  + highScore);
		}
	}

	void OnGUI () {

	}//end onGui

}
