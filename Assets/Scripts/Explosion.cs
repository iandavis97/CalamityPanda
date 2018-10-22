﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damagable otherDamagable = collision.gameObject.GetComponent<Damagable>();
        if (otherDamagable != null)
        {
            otherDamagable.TakeDamage(10);
            print("That's a lotta damage!");
        }
    }
}