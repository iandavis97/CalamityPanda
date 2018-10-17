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
    [SerializeField]
    float fireInterval = 2;
    [SerializeField]
    float parrySpriteDeltaHeight = 1;
    bool isParried = false;
	// Use this for initialization
	void Start ()
    {
        if (!isParried)
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
            if (parrySpriteRef == null)
            {
                parrySpriteRef = Resources.Load<GameObject>("ParrySprite");
            }
            parrySpriteClone = Instantiate(parrySpriteRef);
        }
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
                transform.up = deltaPosition;
                controller.velocity = deltaPosition.normalized * speed;
                fireTimer += Time.deltaTime;
                Weapon.transform.forward = transform.up;
                if (fireTimer >= fireInterval)
                {
                    fireTimer = 0;
                    Weapon.TryFire();
                }
            }
            parrySpriteClone.transform.position = this.transform.position + Vector3.up * parrySpriteDeltaHeight;
        }
        else
        {
            controller.velocity = Vector2.zero;
        }
	}

    public void TryParry()
    {
        isParried = true;
        StartCoroutine(ResetParry());
    }

    IEnumerator ResetParry()
    {
        yield return new WaitForSeconds(3);
        isParried = false;
    }
}
