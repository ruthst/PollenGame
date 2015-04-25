using UnityEngine;
using System.Collections;
using System.Timers;

public enum STATE {RAND, CONT};

public class PollenParticle : MonoBehaviour {

	public Vector2 position;
	public Vector2 velocity;
	public int timerTime;
	Timer changeDirectionTimer;
	public Vector3 color;
	public STATE state;

	// Use this for initialization
	void Start () {
		changeDirectionTimer = new Timer(3000);
		changeDirectionTimer.Elapsed += new ElapsedEventHandler(timerElapsed);
		changeDirectionTimer.Enabled = true;
		state = STATE.RAND;
	}

	void timerElapsed(object sender, ElapsedEventArgs e)
	{
		// Change pollen direction
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
