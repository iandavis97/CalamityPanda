using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactDamage : MonoBehaviour {

    public float MinSpeedForDamage = 10;
    private Rigidbody2D rigid;

	// Use this for initialization
	void Start () {
        rigid = transform.root.GetComponentInChildren<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (rigid.velocity.sqrMagnitude >= MinSpeedForDamage * MinSpeedForDamage)
        {
            Damagable damage = collision.GetComponent<Damagable>();
            if(damage != null)
            {
                damage.TakeDamage(4);
            }
        }
    }
}
