using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour {

    private Vector3 velocity;
    protected int damage;
    private float life;
    private BulletPool pool;

    protected Rigidbody2D rigid;

	// Use this for initialization
	protected void Start () {
        rigid = GetComponent<Rigidbody2D>();
        rigid.velocity = velocity;
	}
	
	// Update is called once per frame
	protected void Update () {
        life -= Time.deltaTime;
        if (life <= 0)
        {
            Release();
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

    public virtual void OnCollisionEnter2D(Collision2D collision)
    {
        Damagable other = collision.gameObject.GetComponent<Damagable>();
        if (other != null)
        {
            other.TakeDamage(damage);
            Release();
        }
        else
        {
            life /= 2;
        }
    }

    public void Release()
    {
        if (pool != null)
        {
            pool.Free(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
