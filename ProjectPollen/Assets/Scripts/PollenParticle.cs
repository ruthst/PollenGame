using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using System;

public enum STATE {RAND, CONT};
public enum COLOR {BLUE, GREEN, ORANGE, PURPLE, RED, TEAL, YELLOW, NONE};

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
		swapVel = false;
		doVelocityChange ();

	}
	// Update is called once per frame
	void Update () {
		if (this.state == STATE.RAND) {
			if (swapVel) {
				swapVel = false;
				doVelocityChange ();
			}
			Vector2 newPoint = Vector2.Lerp (transform.position, randomPos, Time.deltaTime * 1);
			Vector2 newVelDir = newPoint - rbd.position;
			rbd.AddForce (newVelDir * 1.5f);
		} else if (this.state == STATE.CONT) {
			Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			this.transform.position = new Vector3(pos.x, pos.y, 0);
		}
	}

	void OnMouseDown() {
		if (GameObject.Find ("WorkList").GetComponent<WorkList> ().currColorList[0] == this.color) {
			this.state = STATE.CONT;
			GameObject.Find ("Main Camera").GetComponent<GameManager> ().mState = MSTATE.CHAIN;
			GameObject.Find ("Main Camera").GetComponent<GameManager> ().currentChain.Add(this.gameObject);
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
				GameObject.Find ("Main Camera").GetComponent<GameManager> ().currentChain.Add(this.gameObject);
			} else {
				foreach (GameObject pollen in currChain) {
					pollen.GetComponent<PollenParticle>().state = STATE.RAND;
				}
				GameObject.Find ("Main Camera").GetComponent<GameManager> ().currentChain.Clear();
			}
		}
	}

	void OnMouseUp() {
		//Debug.Log ("Un clicked : " + this.name);
		this.state = STATE.RAND;
		GameObject.Find ("Main Camera").GetComponent<GameManager> ().mState = MSTATE.UNCHAIN;
		List<GameObject> currChain = GameObject.Find ("Main Camera").GetComponent<GameManager> ().currentChain;
		foreach (GameObject pollen in currChain) {
			pollen.GetComponent<PollenParticle>().state = STATE.RAND;
		}
		GameObject.Find ("Main Camera").GetComponent<GameManager> ().currentChain.Clear ();
	}

	void timerElapsed(object sender, ElapsedEventArgs e){
		swapVel = true;
	}
	
	void doVelocityChange(){
		float randX = UnityEngine.Random.Range (0.0f, 10.0f) - 5.0f;
		float randY = UnityEngine.Random.Range (0.0f, 16.0f) - 8.0f;
		Vector2 pos = transform.position;
		Vector2 vel = rbd.velocity;
		if (rbd.position.x < -6f) {
			randX = 5.0f;
			pos.x = -6.0f;
			vel.x = 0;
		} else if (rbd.position.x > 6f) {
			randX = -5.0f;
			pos.x = 6.0f;
			vel.x = 0;
		}
		if (rbd.position.y < -9f) {
			randY = 8.5f;
			pos.y = -10.0f;
			vel.y = 0;
		} else if (rbd.position.y > 9f) {
			randY = -8.5f;
			pos.y = 10.0f;
			vel.y = 0;
		}
		transform.position = pos;
		rbd.velocity = vel;
		randomPos = new Vector3 (randX, randY);
	}
}
