using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour {

    private Vector3 velocity;
    private int damage;
    private float life;
    private BulletPool pool;

    private Rigidbody2D rigid;

	// Use this for initialization
	void Start () {
        rigid = GetComponent<Rigidbody2D>();
        rigid.velocity = velocity;
	}
	
	// Update is called once per frame
	void Update () {
        life -= Time.deltaTime;
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
        if(rigid != null)
        {
            rigid.velocity = velocity;
        }
    }

    public void SetPool(BulletPool pool)
    {
        this.pool = pool;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        life /= 2;
        // Do Damage
    }
}
