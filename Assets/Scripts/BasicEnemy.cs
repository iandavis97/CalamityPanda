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
    static GameObject parrySpriteRef;
    GameObject parrySpriteClone;
    SpriteRenderer parrySpriteRenderer;
    [SerializeField]
    float fireInterval = 2;
    [SerializeField]
    float parrySpriteDeltaHeight = 1;
    [SerializeField]
    // the time between the start of the parry "windup" and the chance for you to parry
    float parryWarmUpTime = 3;
    [SerializeField]
    // the time interval in which an attack can be parried
    float parryWindow = 2;
    [SerializeField]
    // the time an enemy is stunned for if a parry is successful
    float stunInterval = 3;
    // if the player's parry was successful
    bool isParried = false;
    // if this is performing an attack that can pe parried in the right moment
    bool isPerformingParryableAttack = false;
    // can the attack be parried right now?
    bool parryWindowOpen = false;

    // Is this unit aware of the player
    bool isAlert = false;
    // A layer mask that allows the inclusion and exclusion of collision layers as applicable to enemy sight
    public LayerMask sightRayMask;

	// Use this for initialization
	void Start ()
    {
        controller = GetComponent<Rigidbody2D>();
        if (playerRef == null)
        {
            playerRef = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        }
        if (Weapon == null)
        {
            Weapon = GetComponent<WeaponHolder>();
        }
        if (parrySpriteRef == null)
        {
            parrySpriteRef = Resources.Load<GameObject>("ParrySprite");
        }
        parrySpriteClone = Instantiate(parrySpriteRef);
        parrySpriteRenderer = parrySpriteClone.GetComponent<SpriteRenderer>();
        parrySpriteRenderer.color = new Color(0, 0, 0, 0);
    }

    // Update is called once per frame
    void Update ()
    {
        if (!isParried)
        {
            // If the player dies, he will be null according to Unity
            if (playerRef != null)
            {
                Vector3 deltaPosition = playerRef.transform.position - transform.position;
                deltaPosition.z = 0;
                if (isAlert)
                {
                    controller.velocity = deltaPosition.normalized * speed;
                    transform.up = deltaPosition;
                    fireTimer += Time.deltaTime;
                    if (fireTimer >= fireInterval)
                    {
                        fireTimer = 0;
                        Weapon.TryFire();
                    }
                }
                else
                {
                    isAlert = !Physics2D.Raycast(transform.position, deltaPosition, deltaPosition.magnitude, sightRayMask);
                }
            }
            // set the parry sprite to always be above this
            parrySpriteClone.transform.position = this.transform.position + Vector3.up * parrySpriteDeltaHeight;
            if (!isPerformingParryableAttack)
            {
                isPerformingParryableAttack = true;
                StartCoroutine(StartParryableAttack());
            }
        }
        else
        {
            controller.velocity = Vector2.zero;
        }
	}

    // called by player script if it tries to parry this enemy's attack
    public void TryParry()
    {
        if (parryWindowOpen)
        {
            isParried = true;
            StartCoroutine(ResetParry());
        }
   
    }

    // after a parry, wait an appropriate amount and resume attacking
    IEnumerator ResetParry()
    {
        yield return new WaitForSeconds(stunInterval);
        isParried = false;

    }

    // start an attack that can be parried at the right moment
    IEnumerator StartParryableAttack()
    {
        // Visually indicate state of attack with parry sprite
        while (parrySpriteRenderer.color.a < .95f)
        {
            parrySpriteRenderer.color = Color.Lerp(parrySpriteRenderer.color, Color.black, Time.deltaTime / parryWarmUpTime);
            yield return null;
        }
        parrySpriteRenderer.color = Color.red;
        parryWindowOpen = true;
        yield return new WaitForSeconds(parryWindow);
        parryWindowOpen = false;
        isPerformingParryableAttack = false;
        parrySpriteRenderer.color = new Color(0, 0, 0, 0);
    }

    public void DestroyParrySprite()
    {
        Destroy(parrySpriteClone);
    }
}
