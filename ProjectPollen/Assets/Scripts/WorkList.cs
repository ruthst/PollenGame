﻿using UnityEngine;
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

	public int timeLimit;

	float smallOffset;
	public float offSet = 1.8f;


	// Use this for initialization
	void Start () {
		
		smallOffset = offSet / (timeLimit * 50);

		score = 0;

		listObjects = new List<GameObject>();
		currColorList = new List<COLOR> ();
		newWorkList();
	}

	// Update is called once per frame
	void Update () {

	}

	void FixedUpdate() {
		

		for (int i = 0; i < listObjects.Count; i++) {
			positions[i].y += smallOffset;
			listObjects[i].transform.localPosition = positions[i];

		}

		//This is the end game state
		if ( positions[0].y >= 10.5) 
		{
			//Debug.Log("KILL KILL KILL");
			Application.LoadLevel(Application.loadedLevel);
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

		score += numDone;
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
//		Debug.Log ("sdasdsad");
//		foreach (COLOR elem in currColorList) {
//			Debug.Log(elem);
//		}
	}
}
