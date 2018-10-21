using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    CharacterController control;
    [SerializeField] float speed;

    // Public so that we can have the component on a different transform
    public WeaponHolder Weapon;

	// Use this for initialization
	void Start ()
    {
        control = GetComponent<CharacterController>();
        if(Weapon == null)
        {
            Weapon = GetComponent<WeaponHolder>();
        }
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
        if(Input.GetKeyDown(KeyCode.Space))
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
