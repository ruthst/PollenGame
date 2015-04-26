using UnityEngine;
using System.Collections;

public class GameOverScript : MonoBehaviour {
	public GameObject scoreText;
	// Use this for initialization
	void Start () {
		int score = PlayerPrefs.GetInt ("SCORE", 0);
		int highScore = PlayerPrefs.GetInt ("HIGHSCORE", 0);
		string text = "Score : " + score + "\n" + "High Score : " + highScore;
		scoreText.GetComponent<TextMesh> ().text = text;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			Application.LoadLevel("KaivanScene");
		}
	}
}
