using UnityEngine;
using System.Collections;

public class CenterCircleScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	} 

	void OnTriggerEnter2D (Collider2D other){
		//Debug.Log ("Collided");
		if (GameObject.Find ("Main Camera").GetComponent<GameManager> ().currentChain.Contains (other.gameObject)) {
			GameObject.Find ("Main Camera").GetComponent<GameManager> ().BroadcastMessage ("centerCollided", other.gameObject);
		}
	}
}
