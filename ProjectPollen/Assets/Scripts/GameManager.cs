using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum MSTATE {UNCHAIN, CHAIN};

public class GameManager : MonoBehaviour {

	public int maxPollen;
	public GameObject pollenPrefab;
	public GameObject linePrefab;
	public Sprite[] pollenSprites;
	public Color[] colorValues;
	public MSTATE mState;
	public List<GameObject> pollenList;
	public List<GameObject> currentChain;
	public List<GameObject> lineList;
	public List<Vector3> chainPositions;

	// Use this for initialization
	void Start () {
		pollenList = new List<GameObject> ();
		currentChain = new List<GameObject> ();
		chainPositions = new List<Vector3> ();
		lineList = new List<GameObject> ();
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
		List<COLOR> nonExistent = new List<COLOR> ();
		for (int i = 0; i < GameObject.Find ("WorkList").GetComponent<WorkList> ().currColorList.Count; i++) {
			bool present = false;
			foreach(GameObject pollen in pollenList) {
				if(pollen.GetComponent<PollenParticle>().color == GameObject.Find ("WorkList").GetComponent<WorkList> ().currColorList[i]) {
					present = true;
					break;
				}
			}
			if (!present) {
				nonExistent.Add(GameObject.Find ("WorkList").GetComponent<WorkList> ().currColorList[i]);
			}
		}
		foreach (COLOR color in nonExistent) {
			float x = Random.Range(-4.9f, 4.9f);
			float y = Random.Range(-8.9f, 8.0f);
			GameObject pollen = (GameObject) Instantiate(pollenPrefab, new Vector3(x, y, 0), Quaternion.identity);
			pollen.name = "pollen" + pollenList.Count;
			int spriteNo = (int)color;
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
			List<COLOR> nonExistent = new List<COLOR> ();
			for (int i = 0; i < GameObject.Find ("WorkList").GetComponent<WorkList> ().currColorList.Count; i++) {
				bool present = false;
				foreach(GameObject pollen in pollenList) {
					if(pollen.GetComponent<PollenParticle>().color == GameObject.Find ("WorkList").GetComponent<WorkList> ().currColorList[i]) {
						present = true;
						break;
					}
				}
				if (!present) {
					nonExistent.Add(GameObject.Find ("WorkList").GetComponent<WorkList> ().currColorList[i]);
				}
			}
			foreach (COLOR color in nonExistent) {
				float x = Random.Range(-4.9f, 4.9f);
				float y = Random.Range(-8.9f, 8.0f);
				GameObject pollen = (GameObject) Instantiate(pollenPrefab, new Vector3(x, y, 0), Quaternion.identity);
				pollen.name = "pollen" + pollenList.Count;
				int spriteNo = (int)color;
				pollen.GetComponent<PollenParticle>().color = (COLOR)spriteNo;
				pollen.GetComponent<SpriteRenderer>().sprite = pollenSprites[spriteNo];
				pollen.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-3.0f, 3.1f), Random.Range(-3.0f, 3.1f));
				pollen.transform.parent = GameObject.Find("PollenList").transform;
				pollen.layer = 8;
				pollen.transform.localScale = new Vector3(2.0f, 2.0f, 1.0f);
				pollenList.Add(pollen);
			}
			for (int i = pollenList.Count; i < maxPollen; i++) {
				float x = Random.Range(-4.9f, 4.9f);
				float y = Random.Range(-8.9f, 8.0f);
				GameObject pollen = (GameObject) Instantiate(pollenPrefab, new Vector3(x, y, 0), Quaternion.identity);
				pollen.name = "pollen" + i;
				int spriteNo = Random.Range(0,7);
				pollen.GetComponent<PollenParticle>().color = (COLOR)spriteNo;
				pollen.GetComponent<SpriteRenderer>().sprite = pollenSprites[spriteNo];
				pollen.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-3.0f, 3.1f), Random.Range(-3.0f, 3.1f));
				pollen.transform.parent = this.transform;
				pollen.layer = 8;
				pollen.transform.localScale = new Vector3(2.0f, 2.0f, 1.0f);
				pollenList.Add(pollen);
			}
		}
		int end = this.lineList.Count - 1;
		int cend = this.currentChain.Count - 1;
		if (end >= 0) {
			for (int i = 0; i < this.lineList.Count - 1; i++) {
				lineList[i].GetComponent<LineRenderer>().SetPosition(0, new Vector3(currentChain[i].transform.localPosition.x, currentChain[i].transform.localPosition.y, -1.0f));
				lineList[i].GetComponent<LineRenderer>().SetPosition(1, new Vector3(currentChain[i+1].transform.localPosition.x, currentChain[i+1].transform.localPosition.y, -1.0f));
			}
			Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			pos = new Vector3(pos.x, pos.y, -1);
			lineList[end].GetComponent<LineRenderer>().SetPosition(0, new Vector3(currentChain[cend].transform.localPosition.x, currentChain[cend].transform.localPosition.y, -1.0f));
			lineList[end].GetComponent<LineRenderer>().SetPosition(1, pos);
		}
		//Debug.Log ("Size : " + this.currentChain.Count);
	}

	void addPollenToChain(GameObject pollen) {
		this.currentChain.Add(pollen);
		this.chainPositions.Add(pollen.transform.localPosition);
		this.mState = MSTATE.CHAIN;
		if (this.lineList.Count == 0) {
			GameObject line1 = (GameObject)Instantiate (linePrefab, new Vector3 (0, 0, -1), Quaternion.identity);
			line1.name = "Line0";
			//line.transform.parent = this.transform;
			Vector3 pos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			pos = new Vector3 (pos.x, pos.y, -1.0f);
			line1.GetComponent<LineRenderer> ().SetPosition (0, new Vector3 (currentChain[0].transform.localPosition.x, currentChain[0].transform.localPosition.y, -1.0f));
			line1.GetComponent<LineRenderer> ().SetPosition (1, pos);
			Color color = this.colorValues[(int)this.currentChain[0].GetComponent<PollenParticle>().color];
			line1.GetComponent<LineRenderer> ().SetColors (color, color);
			this.lineList.Add (line1);
		} else {
			int i = this.lineList.Count - 1;
			int end = this.currentChain.Count - 1;
			lineList[i].GetComponent<LineRenderer>().SetPosition(1, new Vector3(currentChain[end].transform.localPosition.x, currentChain[end].transform.localPosition.y, -1.0f));

			GameObject line1 = (GameObject)Instantiate (linePrefab, new Vector3(0, 0, -1), Quaternion.identity);
			line1.name = "Line0";
			Vector3 pos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			pos = new Vector3 (pos.x, pos.y, -1.0f);
			line1.GetComponent<LineRenderer> ().SetPosition (0, new Vector3(currentChain [end].transform.localPosition.x, currentChain [end].transform.localPosition.y, -1.0f));
			line1.GetComponent<LineRenderer> ().SetPosition (1, pos);
			Color color = this.colorValues[(int)this.currentChain[end].GetComponent<PollenParticle>().color];
			line1.GetComponent<LineRenderer> ().SetColors (color, color);
			this.lineList.Add (line1);
		}
	}

	void clearChain() {
		this.currentChain.Clear ();
		this.chainPositions.Clear();
		foreach (GameObject line in lineList) {
			Destroy(line);
		}
		this.lineList.Clear ();
		this.mState = MSTATE.UNCHAIN;
	}
	
	void centerCollided(){
		//Debug.Log ("Center Collided");
		GameObject.Find ("WorkList").GetComponent<WorkList> ().BroadcastMessage ("move", this.currentChain.Count);
		foreach (GameObject pollen in currentChain) {
			Destroy(pollen);
		}
		this.currentChain.Clear ();
		this.chainPositions.Clear ();
		this.clearChain ();
		this.mState = MSTATE.UNCHAIN;
	}
}
