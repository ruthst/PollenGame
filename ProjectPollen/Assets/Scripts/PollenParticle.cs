using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Timers;
using System;

public enum STATE {RAND, CONT};
public enum COLOR {blue, green, orange, purple, red, teal, yellow};

public class PollenParticle : MonoBehaviour {

	public Vector2 position;
	public int timerTime;
	Timer changeDirectionTimer;
	public COLOR color;
	public STATE state;
	System.Random rnd;
	Rigidbody2D rbd;
	bool swapVel;
	Vector2 randomPos;
	float waitSeconds = 10.0f;

	// Use this for initialization
	void Start () {
		rnd = new System.Random ();
		changeDirectionTimer = new Timer(timerTime);
		changeDirectionTimer.Elapsed += new ElapsedEventHandler(timerElapsed);
		changeDirectionTimer.Enabled = true;
		state = STATE.RAND;
		rbd = this.GetComponent<Rigidbody2D> ();
		rbd.velocity = new Vector2 (0,0);
//		this.GetComponent<SpriteRenderer>().color = color;
		swapVel = false;

	}

	void timerElapsed(object sender, ElapsedEventArgs e){
		// Change pollen direction
		swapVel = true;
	}

	void doVelocityChange(){
		float randX = UnityEngine.Random.Range (0.0f, 16.0f) - 8.0f;
		float randY = UnityEngine.Random.Range (0.0f, 20.0f) - 10.0f;




		randomPos = new Vector3 (UnityEngine.Random.Range( 8,-8 ), UnityEngine.Random.Range( 10, -10 ));
		Debug.Log ("done waiting");
	}


	void FixedUpdate(){
		Vector2 newPoint = Vector2.Lerp (transform.position, randomPos, Time.deltaTime*1);
		Vector2 newVelDir = newPoint - rbd.position;
		rbd.AddForce (newVelDir);
	}
	// Update is called once per frame
	void Update () {
		if (swapVel) {
			swapVel = false;
			doVelocityChange();
		}
		Vector2 newPoint = Vector2.Lerp (transform.position, randomPos, Time.deltaTime*1);
		Vector2 newVelDir = newPoint - rbd.position;
		rbd.AddForce (newVelDir);
	}
}
