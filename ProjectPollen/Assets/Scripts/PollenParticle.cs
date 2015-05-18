using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using System;

public enum STATE {RAND, CONT};
public enum COLOR {BLUE, GREEN, ORANGE, PURPLE, RED, NONE};

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

	AudioSource audio;
	AudioClip beep;

	// Use this for initialization
	void Start () {
		rnd = new System.Random ();
		changeDirectionTimer = new Timer(timerTime);
		changeDirectionTimer.Elapsed += new ElapsedEventHandler(timerElapsed);
		changeDirectionTimer.Enabled = true;
		state = STATE.RAND;
		rbd = this.GetComponent<Rigidbody2D> ();
		//rbd.velocity = new Vector2 (0,0);
		swapVel = false;
		doVelocityChange ();

		audio = GameObject.Find ("AudioObject").GetComponent<AudioSource>();
		beep = GameObject.Find ("AudioObject").GetComponent<AudioMaster> ().beep;
	}
	// Update is called once per frame
	void Update () {
		if (swapVel) {
			swapVel = false;
			doVelocityChange ();
		}
		Vector2 newPoint = Vector2.Lerp (transform.position, randomPos, Time.deltaTime);
		Vector2 newVelDir = newPoint - rbd.position;
		rbd.AddForce (newVelDir * 1.5f);
		if (this.state == STATE.RAND) {
		} else if (this.state == STATE.CONT) {
		}
	}

	void OnMouseDown() {
		if (GameObject.Find ("WorkList").GetComponent<WorkList> ().currColorList[0] == this.color) {
			this.state = STATE.CONT;
			GameObject.Find ("Main Camera").GetComponent<GameManager> ().BroadcastMessage("addPollenToChain", this.gameObject);
		} else {
			Debug.Log("Wrong Click de-grow circle in center");
		}
	}

	void OnMouseOver() {
		if (GameObject.Find ("Main Camera").GetComponent<GameManager> ().mState == MSTATE.CHAIN) {
			List<GameObject> currChain = GameObject.Find ("Main Camera").GetComponent<GameManager> ().currentChain;
			List<COLOR> colorList = GameObject.Find ("WorkList").GetComponent<WorkList> ().currColorList;
			if (currChain.Contains(this.gameObject)) {
				this.state = STATE.CONT;
				return;
			} else if (colorList[currChain.Count] == this.color) {
				this.state = STATE.CONT;
				GameObject.Find ("Main Camera").GetComponent<GameManager> ().BroadcastMessage("addPollenToChain", this.gameObject);
			} else {
				foreach (GameObject pollen in currChain) {
					pollen.GetComponent<PollenParticle>().state = STATE.RAND;
				}
				GameObject.Find ("Main Camera").GetComponent<GameManager> ().BroadcastMessage("clearChain");

				audio.PlayOneShot(beep, 0.2f);

			}
		}
	}

	void OnMouseUp() {
		this.state = STATE.RAND;
		List<GameObject> currChain = GameObject.Find ("Main Camera").GetComponent<GameManager> ().currentChain;
		foreach (GameObject pollen in currChain) {
			pollen.GetComponent<PollenParticle>().state = STATE.RAND;
		}
		GameObject.Find ("Main Camera").GetComponent<GameManager> ().BroadcastMessage("clearChain");
	}

	void timerElapsed(object sender, ElapsedEventArgs e){
		swapVel = true;
	}

	void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.name == "WallCollider") {
			List<GameObject> currChain = GameObject.Find ("Main Camera").GetComponent<GameManager>().currentChain;
			if (GameObject.Find ("Main Camera").GetComponent<GameManager>().currentChain.Contains(this.gameObject)) {
				GameObject.Find ("Main Camera").GetComponent<GameManager> ().BroadcastMessage("clearChain");
				audio.PlayOneShot(beep, 0.2f);
			}
		}
	}
	
	void doVelocityChange(){
		float randX = UnityEngine.Random.Range (-4.5f, 4.51f);
		float randY = UnityEngine.Random.Range (-8.7f, 7.25f);
		Vector2 pos = transform.position;
		Vector2 vel = rbd.velocity;
//		if (rbd.position.x < -4.65f) {
//			randX = 4.5f;
//			pos.x = -4.6f;
//			vel.x = 0;
//		} else if (rbd.position.x > 4.62f) {
//			randX = -4.5f;
//			pos.x = 4.5f;
//			vel.x = 0;
//		}
//		if (rbd.position.y < -8.7f) {
//			randY = 7.2f;
//			pos.y = -8.7f;
//			vel.y = 0;
//		} else if (rbd.position.y > 7.2f) {
//			randY = -8.7f;
//			pos.y = 7.2f;
//			vel.y = 0;
//		}
		transform.position = pos;
		rbd.velocity = vel;
		randomPos = new Vector2 (randX, randY);
	}
}
