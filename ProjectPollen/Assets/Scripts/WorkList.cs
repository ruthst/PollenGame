using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WorkList : MonoBehaviour {

	public GameObject WorkListPrefab;
	public int WORK_LIST_MAX;

	public Sprite[] sprites;

	public List<GameObject> listObjects;
	public List<COLOR> currColorList;

	public Vector3[] positions;

	public int score;

	public float timeLimit;
	float timeLeft;
	public GameObject lifeBar;

	float smallOffset;
	public float offSet = 1.8f;


	// Use this for initialization
	void Start () {
		
		smallOffset = offSet / (timeLimit * 50);

		score = 0;

		listObjects = new List<GameObject>();
		currColorList = new List<COLOR> ();
		newWorkList();
		timeLeft = timeLimit;

	}

	// Update is called once per frame
	void Update () {
		float currPercent = 1.0f - (timeLeft / timeLimit);
		lifeBar.GetComponent<Renderer>().material.SetFloat("_Cutoff", currPercent); 
	}

	void FixedUpdate() {
		timeLeft = timeLeft - Time.deltaTime;

		if (timeLeft < 0) {
			Debug.Log("KILL");
			Application.LoadLevel("GameOver");
		}

	}


	/*
	Generate new list of pollen to be achieved
	*/
	void newWorkList() {
		for (int i = 0; i < WORK_LIST_MAX; i++) {
			int random = Random.Range(0,5);
			currColorList.Add((COLOR)random);
			listObjects.Add((GameObject) Instantiate(WorkListPrefab, positions[i], Quaternion.identity));
			listObjects[i].GetComponent<SpriteRenderer>().sprite = sprites[random];
		}
		listObjects [0].transform.localScale += new Vector3 (0.01f, 0f, 0f);
	}

	/*
	Remove the pollen that was successfully pulled into the node
	*/
	void removeOldWork(int numDone){
		for (int i = 0; i < numDone; i++) {
			Destroy(listObjects[0]);
			listObjects.RemoveAt(0);
			currColorList.RemoveAt(0);
		}


	}


	/*
	Shift worklist colors to the left
	*/
	void shiftWorkList(int numDone) {
		for (int i = 0; i < listObjects.Count; i++) {
			iTween.MoveTo(listObjects[i], positions[i], 0.25f);
			//listObjects[i].transform.localPosition = positions[i];
		}
		//foreach (GameObject elem in listObjects) {
		//		elem.transform.localPosition = new Vector3(positions[listObjects.IndexOf(elem) - numDone].x, positions[listObjects.IndexOf(elem) - numDone].y, positions[listObjects.IndexOf(elem) - numDone].z);
		//}
	}

	/*
	Generate new workList colors in empty slots
	*/
	void refillWorkList(int numDone) {
		for (int i = listObjects.Count; i < WORK_LIST_MAX; i++) {
			int random = Random.Range(0,5);
			currColorList.Add((COLOR)random);
			listObjects.Add((GameObject) Instantiate(WorkListPrefab, positions[i], Quaternion.identity));
			listObjects[i].GetComponent<SpriteRenderer>().sprite = sprites[random];
		}
	}

	void move(int numDone) {
//		foreach (COLOR elem in currColorList) {
//			Debug.Log(elem);
//		}
//		Debug.Log ("dsajdsad");
		removeOldWork(numDone);
		shiftWorkList(numDone);
		refillWorkList(numDone);

		listObjects [0].transform.localScale += new Vector3 (0.01f, 0f, 0f);

		score += numDone + numDone - 1;

//		Debug.Log ("sdasdsad");
//		foreach (COLOR elem in currColorList) {
//			Debug.Log(elem);
//		}
	}
}
