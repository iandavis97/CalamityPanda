using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBarrel : MonoBehaviour
{
    Damagable damage;
    ExplosionEmitter emitter;
	// Use this for initialization
	void Start ()
    {
        damage = GetComponent<Damagable>();
        //damage.MyOnDeath = null;
        damage.MyOnDeath -= damage.OnDeath;
        damage.MyOnDeath += Explode;
        emitter = GetComponent<ExplosionEmitter>();
	}
	
	// Update is called once per frame
	// void Update ()
    // {
	// 	if (damage.Health <= 0)
    //     {
    //         StartCoroutine(emitter.Explode());
    //         Destroy(gameObject);
    //     }
	// }
    void Explode()
    {
        ContactFilter2D enemyFilter = new ContactFilter2D();
        enemyFilter.SetLayerMask(LayerMask.NameToLayer("Player"));
        GetComponent<SpriteRenderer>().enabled = false;
        StartCoroutine(GetComponent<ExplosionEmitter>().Explode());
        // RaycastHit2D[] results = new RaycastHit2D[10];
        // Physics2D.CircleCast(transform.position, 5.0f, Vector2.zero, enemyFilter, results);
        // foreach (RaycastHit2D hit in results)
        // {
        //     if (hit.collider != null)
        //     {
        //         Damagable damage = hit.collider.GetComponent<Damagable>();
        //         if ( damage != null)
        //         {
        //             damage.TakeDamage(1000);
        //         }
        //     }
        // }
    }
}
