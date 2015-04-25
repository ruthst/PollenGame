using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PollenMgmt : MonoBehaviour {
	public int maxPollen;
	public GameObject pollenPrefab;
	public Sprite[] pollenSprites;
	List<GameObject> pollenList;
	// Use this for initialization
	void Start () {
		pollenList = new List<GameObject> ();
		for (int i = 0; i < maxPollen; i++) {
			float x = Random.Range(-4.9f, 4.9f);
			float y = Random.Range(-8.9f, 8.0f);
			GameObject pollen = (GameObject) Instantiate(pollenPrefab, new Vector2(x, y), Quaternion.identity);
			pollen.name = "pollen" + i;
			int spriteNo = Random.Range(0,7);
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
				int spriteNo = Random.Range(0,8);
				pollen.GetComponent<SpriteRenderer>().sprite = pollenSprites[spriteNo];
				pollen.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-3.0f, 3.1f), Random.Range(-3.0f, 3.1f));
				pollen.transform.parent = this.transform;
				pollenList.Add(pollen);
			}

		}
	}
}
