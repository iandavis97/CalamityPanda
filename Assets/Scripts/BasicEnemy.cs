using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    [SerializeField]
    float speed = 5;
    static protected Player playerRef;
    Rigidbody2D controller;
    public WeaponHolder Weapon;
    float fireTimer = 0;
    [SerializeField]
    float fireInterval = 2;
	// Use this for initialization
	void Start ()
    {
        controller = GetComponent<Rigidbody2D>();
		if (playerRef == null)
        {
            playerRef = GameObject.Find("Player").GetComponent<Player>();
        }
        if (Weapon == null)
        {
            Weapon = GetComponent<WeaponHolder>();
        }
    }

    // Update is called once per frame
    void Update ()
    {
        // If the player dies, he will be null according to Unity
        if (playerRef != null)
        {
            Vector3 deltaPosition = playerRef.transform.position - transform.position;
            transform.up = deltaPosition;
            controller.velocity = deltaPosition.normalized * speed;
            fireTimer += Time.deltaTime;
            Weapon.transform.right = transform.up;
            if (fireTimer >= fireInterval)
            {
                fireTimer = 0;
                Weapon.TryFire();
            }
        }
	}
}
