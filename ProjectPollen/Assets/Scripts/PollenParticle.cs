using UnityEngine;
using System.Collections;
using System.Timers;

enum STATE {RAND, CONT};

public class PollenParticle : MonoBehaviour {

	Vector2 position;
	Vector2 velocity;
	Timer changeDirectionTimer;
	Vector3 color;
	STATE state;


	PollenParticle(Vector2 _pos, Vector2 _vel)
	{
		position = _pos;
		velocity = _vel;
		changeDirectionTimer = new Timer(3000);
		changeDirectionTimer.Elapsed += new ElapsedEventHandler(timerElapsed);
		changeDirectionTimer.Enabled = true;
		state = STATE.RAND;
	}

	// Use this for initialization
	void Start () {
	
	}

	void timerElapsed(object sender, ElapsedEventArgs e)
	{
		// Change pollen direction
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
