using System.Collections;
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
        control.velocity = Vector3.ClampMagnitude(control.velocity, 1.2f) * speed;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Weapon.TryPickUp();
        }
        if (Input.GetMouseButton(0))
        {
            if (Weapon.CurrentState == WeaponHolder.CombatState.Waiting)
            {
                Weapon.TryFire();
            }
        }
        if (Input.GetMouseButton(1))
        {
            ContactFilter2D filter = new ContactFilter2D();
            filter.layerMask = LayerMask.NameToLayer("Enemy");

            RaycastHit2D[] hit = new RaycastHit2D[2];
            Physics2D.Raycast(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position, filter, hit);
            if (hit[1].collider != null)
            {
                BasicEnemy possibleEnemy = hit[1].collider.GetComponent<BasicEnemy>();
                if (possibleEnemy != null)
                {
                    possibleEnemy.TryParry();
                }
            }
        }
    }
    private void LateUpdate()
    {
        if (Weapon.CurrentState == WeaponHolder.CombatState.Waiting)
        {
            Vector3 relativeScreenPos = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
            relativeScreenPos = new Vector3(relativeScreenPos.y, -relativeScreenPos.x, 0);
            if (relativeScreenPos.sqrMagnitude > 64)
            {
                transform.right = relativeScreenPos;
            }
        }
    }
}
