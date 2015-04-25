using UnityEngine;
using System.Collections;
using System.Timers;

public enum STATE {RAND, CONT};

public class PollenParticle : MonoBehaviour {

	public Vector2 position;
	public int timerTime;
	Timer changeDirectionTimer;
	public Color color;
	public STATE state;

	// Use this for initialization
	void Start () {
		changeDirectionTimer = new Timer(timerTime);
		changeDirectionTimer.Elapsed += new ElapsedEventHandler(timerElapsed);
		changeDirectionTimer.Enabled = true;
		state = STATE.RAND;
		this.GetComponent<Rigidbody2D> ().velocity = new Vector2 (1, 1);
		this.GetComponent<SpriteRenderer>().color = color;
	}

	void timerElapsed(object sender, ElapsedEventArgs e){
		// Change pollen direction


	}
	void FixedUpdate(){

	}
	// Update is called once per frame
	void Update () {

	}
}
