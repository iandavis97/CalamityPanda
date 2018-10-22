using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public bool KeyGrabbed;//checks if player has got this key
	// Use this for initialization
	void Start ()
    {
        
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
    private void OnTriggerEnter2D(Collider2D other)
    {
        //if key collides with player, counts as being grabbed
        if (other.gameObject.tag == "Player")
            KeyGrabbed = true;
        else
            KeyGrabbed = false;
    }
}
