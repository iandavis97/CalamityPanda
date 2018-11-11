using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour {

	Vector2 pos;

	List<GameObject> neighbors=new List<GameObject>();

	// Use this for initialization
	void Start(){

		pos = new Vector2 (gameObject.transform.position.x,gameObject.transform.position.y);

	}

	public void Connect(Node n, bool mutual=false)
	{

		if (mutual) {
			neighbors.Add (n.gameObject);
			return;
		}

		if (n.DistanceTo(pos)<=1) 
		{
			neighbors.Add (n.gameObject);
			n.Connect (this,true);
		}


	}

	public Vector2 Pos {
		get {
			return pos;
		}
	}

	bool LineObstructed(Node n, NodeList list)
	{
		
		if (Physics2D.Linecast (pos, n.Pos)) 
		{
			return true;
		}

		return false;

	}

	bool LinePermObstructed(Node n, NodeList list)
	{

		//Collider2D

		if (Physics2D.Linecast (pos, n.Pos)) 
		{
			return true;
		}

		return false;

	}

	float DistanceTo(Vector2 dPos)
	{
		return Vector2.Distance(dPos,pos);
	}

	void FixedUpdate(){

		foreach (GameObject n in neighbors)
			Debug.DrawLine(pos,n.GetComponent<Node>().Pos);

	}
}
