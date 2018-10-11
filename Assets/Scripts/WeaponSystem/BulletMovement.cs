using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour {

    private Vector3 velocity;
    private int damage;
    private float life;
    private BulletPool pool;

    private CharacterController controller;

	// Use this for initialization
	void Start () {
        controller = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
        life -= Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
        if (life <= 0)
        {
            if (pool != null)
            {
                pool.Free(gameObject);
            }
            else
            {
                Destroy(this);
            }
        }
    }

    // Initializes it with direction, damage and lifespan
    public void SetUp(Vector3 velocity, int damage, float lifespan)
    {
        this.velocity = velocity;
        this.damage = damage;
        life = lifespan;
    }

    public void SetPool(BulletPool pool)
    {
        this.pool = pool;
    }
}
