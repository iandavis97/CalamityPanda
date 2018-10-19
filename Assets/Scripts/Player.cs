﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D control;
    [SerializeField] float speed;

    // Public so that we can have the component on a different transform
    public WeaponHolder Weapon;

	// Use this for initialization
	void Start ()
    {
        control = GetComponent<Rigidbody2D>();
        if(Weapon == null)
        {
            Weapon = GetComponent<WeaponHolder>();
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        control.velocity = Vector2.zero;
		if (Input.GetKey(KeyCode.W))
        {
            control.velocity += Vector2.up;
        }
        if (Input.GetKey(KeyCode.A))
        {
            control.velocity -= Vector2.right;
        }
        if (Input.GetKey(KeyCode.S))
        {
            control.velocity -= Vector2.up;
        }
        if (Input.GetKey(KeyCode.D))
        {
            control.velocity += Vector2.right;
        }
        control.velocity = Vector3.ClampMagnitude(control.velocity, 1) * speed;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Weapon.TryPickUp();
        }
        if(Input.GetMouseButton(0))
        {
            Vector3 mouse = (Camera.main.ScreenToWorldPoint(Input.mousePosition));
            mouse.z = 0;
            Weapon.transform.LookAt(mouse);
            Weapon.TryFire();
        }
    }
}
