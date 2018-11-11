using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeList : MonoBehaviour {

	public List<GameObject> content = new List<GameObject>();
	List<GameObject> pObstructions = new List<GameObject> ();
	List<GameObject> tObstructions = new List<GameObject> ();

	public GameObject nodePrefab;

	public int minX;
	public int minY;
	public int maxX;
	public int maxY;

	// Use this for initialization
	void Start () {

		pObstructions = new List<GameObject>(GameObject.FindGameObjectsWithTag ("pObstruction"));
		tObstructions =  new List<GameObject>(GameObject.FindGameObjectsWithTag ("tObstruction"));
		content = new List<GameObject> (GameObject.FindGameObjectsWithTag("node"));

		for(int i=0;i<content.Count;i++){
		
			for (int j = 0; j < content.Count; j++) 
			{
				if (j == i)
					continue;

				content [i].GetComponent<Node> ().Connect (content [j].GetComponent<Node>());

			}
		
		}

	}

	public List<GameObject> PObstructions {
		get {
			return pObstructions;
		}
	}

	public List<GameObject> TObstructions {
		get {
			return tObstructions;
		}
	}

	// Update is called once per frame
	void Update () {
		
	}
}
