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
        emitter = GetComponent<ExplosionEmitter>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (damage.Health <= 0)
        {
            StartCoroutine(emitter.Explode());
            Destroy(gameObject);
        }
	}
}
