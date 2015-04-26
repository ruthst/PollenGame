using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WorkList : MonoBehaviour {

	public GameObject WorkListPrefab;
	public int WORK_LIST_MAX;

	public Sprite[] sprites;

	public List<GameObject> listObjects;

	public Vector3[] positions;


	// Use this for initialization
	void Start () {
		listObjects = new List<GameObject>();
		newWorkList();
	}
	
	/*
	Check if pollen has been absorbed in order, and how many
	Filter down colors 
	generate new ones at the end
	*/

	// Update is called once per frame
	void Update () {
	}


	/*
	Generate new list of pollen to be achieved
	*/
	void newWorkList() {
		//get 5 random numbers mod 8, use those to get sprite

		for (int i = 0; i < WORK_LIST_MAX; i++) {

			int random = Random.Range(0,7);

			//Debug.Log("The random is " + random);
			listObjects.Add((GameObject) Instantiate(WorkListPrefab, positions[i], Quaternion.identity));
			listObjects[i].GetComponent<SpriteRenderer>().sprite = sprites[random];
		}

	}

	/*
	Remove the pollen that was successfully pulled into the node
	*/
	void removeOldWork(int numDone){
		for (int i = 0; i < numDone; i++) {
			listObjects.RemoveAt(i);
		}
	}


	/*
	Shift worklist colors to the left
	*/
	void shiftWorkList(int numDone) {

		foreach (GameObject elem in listObjects) {
				elem.transform.localPosition = new Vector3(positions[listObjects.IndexOf(elem) - numDone].x, positions[listObjects.IndexOf(elem) - numDone].y, positions[listObjects.IndexOf(elem) - numDone].z);
		}
	}


	/*
	Generate new workList colors in empty slots
	*/
	void refillWorkList(int numDone) {
		for (int i = (WORK_LIST_MAX - numDone); i < WORK_LIST_MAX; i++) {
			int random = Random.Range(0,7);

			listObjects.Add((GameObject) Instantiate(WorkListPrefab, positions[i], Quaternion.identity));
			listObjects[i].GetComponent<SpriteRenderer>().sprite = sprites[random];

		}
	}

	void move(int numDone) {
		removeOldWork(numDone);
		shiftWorkList(numDone);
		refillWorkList(numDone);
	}

}
