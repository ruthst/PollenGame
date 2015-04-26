using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum MSTATE {UNCHAIN, CHAIN};

public class GameManager : MonoBehaviour {

	public int maxPollen;
	public GameObject pollenPrefab;
	public Sprite[] pollenSprites;
	public MSTATE mState;
	public List<GameObject> pollenList;
	public List<GameObject> currentChain;
	// Use this for initialization
	void Start () {
		pollenList = new List<GameObject> ();
		currentChain = new List<GameObject> ();
		for (int i = 0; i < maxPollen; i++) {
			float x = Random.Range(-4.9f, 4.9f);
			float y = Random.Range(-8.9f, 8.0f);
			GameObject pollen = (GameObject) Instantiate(pollenPrefab, new Vector3(x, y, 0), Quaternion.identity);
			pollen.name = "pollen" + i;
			int spriteNo = Random.Range(0,7);
			pollen.GetComponent<PollenParticle>().color = (COLOR)spriteNo;
			pollen.GetComponent<SpriteRenderer>().sprite = pollenSprites[spriteNo];
			pollen.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-3.0f, 3.1f), Random.Range(-3.0f, 3.1f));
			pollen.transform.parent = GameObject.Find("PollenList").transform;
			pollen.layer = 8;
			pollen.transform.localScale = new Vector3(2.0f, 2.0f, 1.0f);
			pollenList.Add(pollen);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (pollenList.Count < maxPollen) {
			for (int i = pollenList.Count; i < maxPollen; i++) {
				float x = Random.Range(-4.9f, 4.9f);
				float y = Random.Range(-8.9f, 8.0f);
				GameObject pollen = (GameObject) Instantiate(pollenPrefab, new Vector3(x, y, 0), Quaternion.identity);
				pollen.name = "pollen" + i;
				int spriteNo = Random.Range(0,7);
				pollen.GetComponent<PollenParticle>().color = (COLOR)spriteNo;
				pollen.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-3.0f, 3.1f), Random.Range(-3.0f, 3.1f));
				pollen.transform.parent = this.transform;
				pollen.layer = 8;
				pollen.transform.localScale = new Vector3(2.0f, 2.0f, 1.0f);
				pollenList.Add(pollen);
			}
		}
	}

	void centerCollided(GameObject gObj){
		Debug.Log ("Center Collided");
		GameObject.Find ("WorkList").GetComponent<WorkList> ().BroadcastMessage ("move", this.currentChain.Count);
		foreach (GameObject pollen in currentChain) {
			Destroy(pollen);
		}
		this.currentChain.Clear ();
		this.mState = MSTATE.UNCHAIN;
	}
}
