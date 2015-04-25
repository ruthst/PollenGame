using UnityEngine;
using System.Collections;

public class WorkList : MonoBehaviour {

	public GameObject WorkListPrefab;
	const int WORK_LIST_MAX = 8;

	public Sprite[] sprites;

	public GameObject[] listObjects;

	public Vector3[] positions;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	/*
	Generate new list of pollen to be achieved
	*/
	void newWorkList() {
		//get 5 random numbers mod 8, use those to get sprite

		for (int i = 0; i < WORK_LIST_MAX; i++) {

			int random = Random.Range(0,8);

			listObjects[i] = (GameObject) Instantiate(WorkListPrefab, positions[i], Quaternion.identity);
			listObjects[i].GetComponent<SpriteRenderer>().sprite = sprites[random];
		}

	}

	/*
	Check if pollen has been absorbed in order, and how many
	Filter down colors 
	generate new ones at the end
	*/
	void updateWorkList() {

		refillWorkList();
	}

	/*
	Generate new workList colors in empty slots
	*/
	void refillWorkList() {
		for (int i = 0; i < WORK_LIST_MAX; i++) {
			if (listObjects[i] == null) {
				int random = Random.Range(0,8);

				listObjects[i] = (GameObject) Instantiate(WorkListPrefab, positions[i], Quaternion.identity);
				listObjects[i].GetComponent<SpriteRenderer>().sprite = sprites[random];
			}

		}
	}

}
