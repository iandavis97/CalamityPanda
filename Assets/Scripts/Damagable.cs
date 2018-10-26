using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damagable : MonoBehaviour {
    public int MaxHealth = 1;
    public int Health { get; private set; }
    public delegate void OnDeathAction();
    public OnDeathAction MyOnDeath { get; set; }
    public GameObject bloodSplat;

    // Use this for initialization
    void Start () {
        Health = MaxHealth;
        if(MyOnDeath == null)
        {
            MyOnDeath = OnDeath;
        }
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
            MyOnDeath();
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

    public void OnDeath()
    {
        WeaponHolder weapon = GetComponent<WeaponHolder>();
        weapon = weapon ? weapon : GetComponentInChildren<WeaponHolder>();
        if (weapon)
        {
            weapon.Release();
        }
        // if (GetComponent<BasicEnemy>() != null)
        // {
        //     GetComponent<BasicEnemy>().DestroyParrySprite();
        // }

        if(bloodSplat != null)
        {
            Instantiate(bloodSplat, transform.position, transform.rotation);
        }

        Destroy(gameObject);
    }
}
