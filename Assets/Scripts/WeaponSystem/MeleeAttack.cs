using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour {

    public float StartUp;
    public float ActiveFrames = .5f;
    public int Damage;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damagable other = collision.gameObject.GetComponent<Damagable>();
        if (other != null)
        {
            other.TakeDamage(Damage);
        }
    }
}
