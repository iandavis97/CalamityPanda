using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damagable : MonoBehaviour {
    public int MaxHealth = 1;
    public int Health { get; set; }

    // Use this for initialization
    void Start () {
        Health = MaxHealth;
	}
	
	// Update is called once per frame
	void Update () {

    }

    public void TakeDamage(int amount)
    {
        Health -= amount;
        if(Health <= 0)
        {
            WeaponHolder weapon = GetComponent<WeaponHolder>();
            weapon = weapon ? weapon : GetComponentInChildren<WeaponHolder>();
            if (weapon)
            {
                weapon.Release();
            }
            Destroy(gameObject);
        }
    }
}
