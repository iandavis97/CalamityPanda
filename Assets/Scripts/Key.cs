using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : PowerUp
{
	// Use this for initialization
	void Start ()
    {
	}
	
	// Update is called once per frame
	void Update ()
    {

	}

    protected override bool TryUse(GameObject other)
    {
        return other.tag.Equals("Player");
    }
}
