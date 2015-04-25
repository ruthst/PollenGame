using UnityEngine;
using System.Collections;
using System.Timers;
using System;

public enum STATE {RAND, CONT};

public class PollenParticle : MonoBehaviour {

	public Vector2 position;
	public int timerTime;
	Timer changeDirectionTimer;
	public Color color;
	public STATE state;
	System.Random rnd;
	Rigidbody2D rbd;
	bool swapVel;

	// Use this for initialization
	void Start () {
		rnd = new System.Random ();
		changeDirectionTimer = new Timer(timerTime);
		changeDirectionTimer.Elapsed += new ElapsedEventHandler(timerElapsed);
		changeDirectionTimer.Enabled = true;
		state = STATE.RAND;
		rbd = this.GetComponent<Rigidbody2D> ();
		rbd.velocity = new Vector2 (1, 1);
//		this.GetComponent<SpriteRenderer>().color = color;
		swapVel = false;

	}

	void timerElapsed(object sender, ElapsedEventArgs e){
		// Change pollen direction
		swapVel = true;
	}

	void doVelocityChange(){
		int randomSwap = rnd.Next(0,4);
		Vector2 tmpVel = new Vector2 (0, 0);
		switch (randomSwap) {
		case 0:
			tmpVel.x += -5;
			tmpVel.y = -rbd.velocity.y;
			break;
		case 1:
			tmpVel.x = -rbd.velocity.x;
			tmpVel.y += -5;
			break;
		case 2:
			tmpVel.y = -rbd.velocity.y;
			tmpVel.x += 5;
			break;
		case 3:
			tmpVel.x = - rbd.velocity.x;
			tmpVel.y += 5;
			break;
		}


		rbd.AddForce (tmpVel);
		
		Debug.Log ("Do Velocity Change ran swap val:" + randomSwap);
	}

	void FixedUpdate(){
		if (swapVel) {
			doVelocityChange ();
			swapVel = false;
		}
	}
	// Update is called once per frame
	void Update () {
		if (swapVel) {
			doVelocityChange ();
			swapVel = false;
		}
	}
}
