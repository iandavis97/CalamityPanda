using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    CharacterController control;
    [SerializeField] float speed;

	// Use this for initialization
	void Start ()
    {
        control = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (Input.GetKey(KeyCode.W))
        {
            control.Move(Vector3.up * Time.deltaTime * speed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            control.Move(-Vector3.right * Time.deltaTime * speed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            control.Move(-Vector3.up * Time.deltaTime * speed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            control.Move(Vector3.right * Time.deltaTime * speed);
        }
    }
}
