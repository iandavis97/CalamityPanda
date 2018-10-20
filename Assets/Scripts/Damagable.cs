using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damagable : MonoBehaviour {
    public int MaxHealth = 1;
    public int Health { get; private set; }

    // Use this for initialization
    void Start () {
        Health = MaxHealth;
	}
	
	// Update is called once per frame
	void Update () {

    }

    // Lowers health by amount 
    public void TakeDamage(int amount)
    {
        if(amount < 0)
        {
            return;
        }
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

    // Increases health by amount
    public void Heal(int amount)
    {
        if (amount < 0)
        {
            return;
        }
        Health += amount;
        if (Health > MaxHealth)
        {
            Health = MaxHealth;
        }
    }
}
