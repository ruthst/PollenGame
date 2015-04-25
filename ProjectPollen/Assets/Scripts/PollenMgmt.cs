using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PollenMgmt : MonoBehaviour {
	public int maxPollen;
	public GameObject pollenPrefab;
	public Sprite[] pollenSprites;
	public List<GameObject> pollenList;
	// Use this for initialization
	void Start () {
		pollenList = new List<GameObject> ();
		for (int i = 0; i < maxPollen; i++) {
			float x = Random.Range(-4.9f, 4.9f);
			float y = Random.Range(-8.9f, 8.0f);
			GameObject pollen = (GameObject) Instantiate(pollenPrefab, new Vector2(x, y), Quaternion.identity);
			pollen.name = "pollen" + i;
			int spriteNo = Random.Range(0,7);
			pollen.GetComponent<PollenParticle>().color = (COLOR)spriteNo;
			pollen.GetComponent<SpriteRenderer>().sprite = pollenSprites[spriteNo];
			pollen.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-3.0f, 3.1f), Random.Range(-3.0f, 3.1f));
			pollen.transform.parent = this.transform;
			pollenList.Add(pollen);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (pollenList.Count < maxPollen) {
			for (int i = pollenList.Count; i < maxPollen; i++) {
				float x = Random.Range(-4.9f, 4.9f);
				float y = Random.Range(-8.9f, 8.0f);
				GameObject pollen = (GameObject) Instantiate(pollenPrefab, new Vector2(x, y), Quaternion.identity);
				pollen.name = "pollen" + i;
				int spriteNo = Random.Range(0,7);
				pollen.GetComponent<PollenParticle>().color = (COLOR)spriteNo;
				pollen.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-3.0f, 3.1f), Random.Range(-3.0f, 3.1f));
				pollen.transform.parent = this.transform;
				pollenList.Add(pollen);
			}

		}
	}

	static COLOR intToCOLOR(int colorNumber){
		switch (colorNumber) {
			case 0:
				return COLOR.BLUE;
			case 1:
				return COLOR.GREEN;
			case 2:
				return COLOR.ORANGE;
			case 3:
				return COLOR.PURPLE;
			case 4:
				return COLOR.RED;
			case 5:
				return COLOR.TEAL;
			case 6:
				return COLOR.YELLOW;
			default:
				Debug.LogError("No such color");
				return COLOR.NONE;
		}
	}

	static int COLORtoInt(COLOR color){
		switch (color) {
			case COLOR.BLUE:
				return 0;
			case COLOR.GREEN:
				return 1;
			case COLOR.ORANGE:
				return 2;
			case COLOR.PURPLE:
				return 3;
			case COLOR.RED:
				return 4;
			case COLOR.TEAL:
				return 5;
			case COLOR.YELLOW:
				return 6;
			default:
				Debug.LogError("No such color");
				return 25;
		}
	}
}
