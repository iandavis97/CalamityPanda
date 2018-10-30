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
    [SerializeField] float acceleration = 5;

    // Dash properties and controls
    private PlayerState state;
    public float dashSpeed = 5;
    public float maxDashRange = 3;
    public float maxChargeTime = 1;
    public float minChargeTime = .3f;
    private float chargeTime;
    private float dashStartTime;
    private float dashTimer;
    private GameObject lastBounce = null;

    // Internal connections
    private Damagable health;
    private Collider2D box;
    private SpriteRenderer sprite;
    private MeshRenderer dashSphere;

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
        sprite = GetComponent<SpriteRenderer>();
        dashSphere = GetComponentInChildren<MeshRenderer>();
        dashSphere.enabled = false;
    }

    // Update is called once per frame
    void FixedUpdate ()
    {
        switch (state)
        {
            case PlayerState.Idle:
                // Firing logic
                if (Input.GetMouseButton(0))
                {
                    if (Weapon.CurrentState == WeaponHolder.CombatState.Waiting)
                    {
                        Weapon.TryFire();
                    }
                }

                // Movement logic
                Vector2 target = new Vector3();
                if (Input.GetKey(KeyCode.W))
                {
                    target += Vector2.up;
                }
                if (Input.GetKey(KeyCode.A))
                {
                    target -= Vector2.right;
                }
                if (Input.GetKey(KeyCode.S))
                {
                    target -= Vector2.up;
                }
                if (Input.GetKey(KeyCode.D))
                {
                    target += Vector2.right;
                }
                target = Vector2.ClampMagnitude(target, 1) * speed;
                float mod = Vector2.Dot(target, control.velocity) > 0 ? 1 : 2;
                control.velocity = Vector2.MoveTowards(control.velocity, target, acceleration * mod * Time.fixedDeltaTime);


            
                // Spindash logic
                if (Input.GetMouseButton(1))
                {
                    state = PlayerState.Aiming;
                    chargeTime = 0;
                    sprite.enabled = false;
                    dashSphere.enabled = true;
                    Weapon.gameObject.SetActive(false);
                }
                break;
            case PlayerState.Aiming:
                control.velocity = Vector2.MoveTowards(control.velocity, Vector2.zero, acceleration * Time.fixedDeltaTime);
                chargeTime += Time.fixedDeltaTime;
                if (chargeTime > maxChargeTime)
                {
                    chargeTime = maxChargeTime;
                }
                if (!Input.GetMouseButton(1))
                {
                    if(chargeTime < minChargeTime)
                    {
                        state = PlayerState.Idle;
                        sprite.enabled = true;
                        dashSphere.enabled = false;
                        Weapon.gameObject.SetActive(true);
                        break;
                    }
                    dashTimer = maxDashRange / dashSpeed * Mathf.Pow(chargeTime / maxChargeTime, .5f);
                    dashStartTime = dashTimer;
                    state = PlayerState.Rolling;
                    health.Immune = true;
                    control.gameObject.layer = 15;
                    lastBounce = null;
                }
                break;
            case PlayerState.Rolling:
                dashTimer -= Time.fixedDeltaTime;
                control.velocity = transform.up * dashSpeed * dashTimer / dashStartTime * Mathf.Pow(chargeTime / maxChargeTime, .5f);
                if (dashTimer <= 0)
                {
                    state = PlayerState.Idle;
                    health.Immune = false;
                    control.gameObject.layer = 13;
                    sprite.enabled = true;
                    dashSphere.enabled = false;
                    Weapon.gameObject.SetActive(true);
                }
                break;
            default:
                break;
        }
        dashSphere.transform.localRotation *= Quaternion.Euler(chargeTime * 10, 0, 0);
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

    private void Update()
    {
        // Weapon pickup logic
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Weapon.TryPickUp();
        }
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
        if (state == PlayerState.Rolling && Vector3.Dot(transform.up, collision.contacts[0].normal) < 0)
        {
            transform.up = Vector3.Reflect(transform.up, collision.contacts[0].normal);
            lastBounce = collision.gameObject;
        }
    }
}
