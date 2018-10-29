using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public bool KeyGrabbed;//checks if player has got this key
    public GameObject player;
	// Use this for initialization
	void Start ()
    {
	}
	
	// Update is called once per frame
	void Update ()
    {
        KeyCollision(player);
        if (KeyGrabbed == true)
            this.GetComponent<SpriteRenderer>().enabled = false;
	}
    private void KeyCollision(GameObject other)
    {
        if (Input.GetKeyDown(KeyCode.E)
            &&(this.GetComponent<Collider2D>().IsTouching(other.GetComponent<Collider2D>()) ==true))//picking up key
        {
            KeyGrabbed = true;
        }
        else
            KeyGrabbed = false;
    }
}
