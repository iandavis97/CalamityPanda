using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public enum PlayerState
    {
        Idle, Aiming, Rolling
    }

    Rigidbody2D control;
    [SerializeField] float speed;

    // Dash properties and controls
    private PlayerState state;
    public float dashSpeed = 5;
    public float dashRange = 3;
    private float dashTimer;
    private Damagable health;
    private Collider2D box;
    private GameObject lastBounce = null;

    // Public so that we can have the component on a different transform
    public WeaponHolder Weapon;

	// Use this for initialization
	void Start ()
    {
        state = PlayerState.Idle;
        control = GetComponent<Rigidbody2D>();
        if(Weapon == null)
        {
            Weapon = GetComponent<WeaponHolder>();
        }
        health = GetComponent<Damagable>();
        box = GetComponent<Collider2D>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        switch (state)
        {
            case PlayerState.Idle:
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
                    state = PlayerState.Aiming;
                }
                break;
            case PlayerState.Aiming:
                control.velocity = Vector2.zero;    
                if (!Input.GetMouseButton(1))
                {
                    dashTimer = dashRange / dashSpeed;
                    state = PlayerState.Rolling;
                    health.Immune = true;
                    control.gameObject.layer = 15;
                    lastBounce = null;
                }
                break;
            case PlayerState.Rolling:
                dashTimer -= Time.deltaTime;
                control.velocity = transform.up * dashSpeed;
                if (dashTimer <= 0)
                {
                    state = PlayerState.Idle;
                    health.Immune = false;
                    control.gameObject.layer = 13;
                }
                break;
            default:
                break;
        }
        // if (Input.GetMouseButton(1))
        // {
        //     ContactFilter2D filter = new ContactFilter2D();
        //     filter.layerMask = LayerMask.NameToLayer("Enemy");

        //     RaycastHit2D[] hit = new RaycastHit2D[2];
        //     Physics2D.Raycast(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position, filter, hit);
        //     if (hit[1].collider != null)
        //     {
        //         BasicEnemy possibleEnemy = hit[1].collider.GetComponent<BasicEnemy>();
        //         if (possibleEnemy != null)
        //         {
        //             possibleEnemy.TryParry();
        //         }
        //     }
        // }
    }
    private void LateUpdate()
    {
        if (Weapon.CurrentState == WeaponHolder.CombatState.Waiting && state != PlayerState.Rolling)
        {
            Vector3 relativeScreenPos = Input.mousePosition - Camera.main.WorldToScreenPoint(Weapon.transform.position);
            relativeScreenPos.z = 0;
            if (relativeScreenPos.sqrMagnitude > 32*32)
            {
                transform.up = relativeScreenPos;
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (state == PlayerState.Rolling && lastBounce != collision.gameObject)
        {
            transform.up = Vector3.Reflect(transform.up, collision.contacts[0].normal);
            lastBounce = collision.gameObject;
        }
    }
}
