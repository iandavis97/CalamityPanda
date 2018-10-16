using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    public GameObject Track;

    public int DepthAway = 1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        // If the player dies, he will be null according to Unity
        if (Track != null)
        {
            transform.position = Track.transform.position + new Vector3(0, 0, -DepthAway);
        }
    }
}
