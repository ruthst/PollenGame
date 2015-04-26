using UnityEngine;
using System.Collections;

public class IntroMain : MonoBehaviour {
	public bool readyToStart;

	// Use this for initialization
	void Start () {
		this.readyToStart = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			if (this.readyToStart) {
				Debug.Log("Here");
				GameObject.Find("titleFlowerCenter").GetComponent<Animator>().SetBool("mouseClicked", true);
				GameObject.Find("Title").GetComponent<Animator>().SetBool("mouseClicked", true);
				GameObject.Find("beginText").GetComponent<Animator>().SetBool("mouseClicked", true);
			}
		}
	}
}
