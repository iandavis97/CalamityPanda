using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
		StartCoroutine(DestroySelf());
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damagable otherDamagable = collision.gameObject.GetComponent<Damagable>();
        if (otherDamagable != null)
        {
            otherDamagable.TakeDamage(50);
            print("That's a lotta damage!");
        }
    }

    IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
