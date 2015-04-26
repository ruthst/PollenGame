using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum MSTATE {UNCHAIN, CHAIN};

public class GameManager : MonoBehaviour {

	public int maxPollen;
	public GameObject pollenPrefab;
	public GameObject linePrefab;
	public GameObject particlePrefab;
	public Sprite[] pollenSprites;
	public Color[] colorValues;
	public MSTATE mState;
	public List<GameObject> pollenList;
	public List<GameObject> currentChain;
	public List<GameObject> lineList;
	public List<Vector3> chainPositions;
	WorkList workList;

	int chainSize;

	AudioSource audio;
	AudioSource backGroundMusic;

	// Use this for initialization
	void Start () {
		workList = GameObject.Find ("WorkList").GetComponent<WorkList>();
		pollenList = new List<GameObject> ();
		currentChain = new List<GameObject> ();
		chainPositions = new List<Vector3> ();
		lineList = new List<GameObject> ();
		for (int i = 0; i < maxPollen; i++) {
			float x = Random.Range(-4.9f, 4.9f);
			float y = Random.Range(-8.9f, 8.0f);
			GameObject pollen = (GameObject) Instantiate(pollenPrefab, new Vector3(x, y, 0), Quaternion.identity);
			pollen.name = "pollen" + i;
			int spriteNo = Random.Range(0,5);
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
		// ensure that there is enough to chain
		Dictionary<COLOR, int> colorCountList = new Dictionary<COLOR, int>();
		Dictionary<COLOR, int> colorCountScreen = new Dictionary<COLOR, int>();

		for(int i = 0; i < workList.currColorList.Count; i++){
			if(colorCountList.ContainsKey(workList.currColorList[i]))
			{
				colorCountList[workList.currColorList[i]] += 1;
			}else {
				colorCountList.Add (workList.currColorList[i], 1);
			}
		}
		foreach(GameObject pollen in pollenList){
			if(colorCountScreen.ContainsKey(pollen.GetComponent<PollenParticle>().color))
			{
				colorCountScreen[pollen.GetComponent<PollenParticle>().color] += 1;
			}else {
				colorCountScreen.Add (pollen.GetComponent<PollenParticle>().color, 1);
			}
		}

		foreach (COLOR col in colorCountList.Keys) {
			if(colorCountList[col] > colorCountScreen[col]){
				int diff = colorCountList[col] - colorCountScreen[col];
				for(int j = 0; j < diff; j++){
					nonExistent.Add(col);
				}
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

		audio = GameObject.Find ("AudioObject").GetComponent<AudioSource>();
		//backGroundMusic = this.gameObject.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {

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
			
			Dictionary<COLOR, int> colorCountList = new Dictionary<COLOR, int>();
			Dictionary<COLOR, int> colorCountScreen = new Dictionary<COLOR, int>();
			
			for(int i = 0; i < workList.currColorList.Count; i++){
				if(colorCountList.ContainsKey(workList.currColorList[i]))
				{
					colorCountList[workList.currColorList[i]] += 1;
				}else {
					colorCountList.Add (workList.currColorList[i], 1);
				}
			}
			foreach(GameObject pollen in pollenList){
				if(colorCountScreen.ContainsKey(pollen.GetComponent<PollenParticle>().color))
				{
					colorCountScreen[pollen.GetComponent<PollenParticle>().color] += 1;
				}else {
					colorCountScreen.Add (pollen.GetComponent<PollenParticle>().color, 1);
				}
			}
			
			foreach (COLOR col in colorCountList.Keys) {
				if(colorCountList[col] > colorCountScreen[col]){
					int diff = colorCountList[col] - colorCountScreen[col];
					for(int j = 0; j < diff; j++){
						nonExistent.Add(col);
					}
				}
			}

			foreach (COLOR color in nonExistent) {
				

				//Random Placement of new pollen
				int randomDir = Random.Range(0,3) % 4;
				float x;
				float y;

				if (randomDir == 0) {
					x = Random.Range(-4.9f, 4.9f);
					y = Random.Range( 9.0f , 10.0f);
				}
				else if (randomDir % 4 == 1) {
					x = Random.Range( 5.0f, 6.0f);
					y = Random.Range( -10.0f , 10.0f);
				}
				else if (randomDir % 4 == 2) {
					x = Random.Range(-4.9f, 4.9f);
					y = Random.Range( -9.0f , -10.0f);
				}
				else {
					x = Random.Range( -5.0f, -6.0f);
					y = Random.Range( -10.0f , 10.0f);
				}

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
		if (pollenList.Count < maxPollen) {
			for (int i = pollenList.Count; i < maxPollen; i++) {
				
				//random placement of new
				int randomDir = Random.Range(0,3) % 4;

				float x;
				float y;

				if (randomDir == 0) {
					x = Random.Range(-4.9f, 4.9f);
					y = Random.Range( 9.0f , 10.0f);
				}
				else if (randomDir % 4 == 1) {
					x = Random.Range( 5.0f, 6.0f);
					y = Random.Range( -10.0f , 10.0f);
				}
				else if (randomDir % 4 == 2) {
					x = Random.Range(-4.9f, 4.9f);
					y = Random.Range( -9.0f , -10.0f);
				}
				else {
					x = Random.Range( -5.0f, -6.0f);
					y = Random.Range( -10.0f , 10.0f);
				}

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
			//(int) this.currentChain[this.currentChain.Count - 1].GetComponent<PollenParticle>().color

			AudioClip sound = GameObject.Find ("AudioObject").GetComponent<AudioMaster> ().clips[Random.Range(0,7)];
			audio.PlayOneShot(sound ,0.7f);
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
		Vector3 pos;

		//Debug.Log ("Center Collided");
		GameObject.Find ("WorkList").GetComponent<WorkList> ().BroadcastMessage ("move", this.currentChain.Count);
		
		chainSize = currentChain.Count;

		foreach (GameObject pollen in currentChain) {
			iTween.ScaleTo(pollen, new Vector3(0.1f,0.1f,0.0f), 0.40f);
			iTween.ScaleTo(pollen, iTween.Hash("scale" ,new Vector3(4.0f,4.0f,0.0f),"x", pollen.transform.position.x, "y", pollen.transform.position.y,"time", 0.20f, "delay", 0.20f));
			pos = pollen.transform.localPosition;
			pos = new Vector3 (pos.x, pos.y, -1.0f);
			particlePrefab.GetComponent<ParticleSystem>().startColor = this.colorValues[(int)pollen.GetComponent<PollenParticle>().color];
			Instantiate(particlePrefab, pos, Quaternion.identity);
			this.pollenList.Remove(pollen);
			Destroy(pollen, 0.4f);
		}

		AudioClip pop = GameObject.Find ("AudioObject").GetComponent<AudioMaster> ().pop;
		audio.clip = pop;

		audio.volume = (0.3f * chainSize);
		audio.PlayDelayed(0.3f);
		//audio.PlayOneShot(pop, 0.5f);

		this.currentChain.Clear ();
		this.chainPositions.Clear ();
		this.clearChain ();
		this.mState = MSTATE.UNCHAIN;
	}
}
