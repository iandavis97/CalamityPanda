using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthFlasherPandaBall : MonoBehaviour {

    // The damagable we are tracking
    public Damagable Track;
    private MeshRenderer rend;

	// Use this for initialization
	void Start () {
        rend = GetComponent<MeshRenderer>(); 
	}
	
	// Update is called once per frame
	void Update () {
		if(Track != null)
        {
            rend.material.color = Color.Lerp(Color.red, Color.white, Track.GetFlashingPoint());
        }
    }
}
