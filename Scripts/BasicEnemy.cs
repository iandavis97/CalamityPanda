using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    [SerializeField]
    float speed = 5;
    static protected Player playerRef;
    CharacterController controller;
	// Use this for initialization
	void Start ()
    {
        controller = GetComponent<CharacterController>();
		if (playerRef == null)
        {
            playerRef = GameObject.Find("Player").GetComponent<Player>();
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        Vector3 deltaPosition = playerRef.transform.position - transform.position;
        transform.up = deltaPosition;
        controller.Move(deltaPosition.normalized * speed * Time.deltaTime);
        
	}
}
