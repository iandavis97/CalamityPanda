﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damagable : MonoBehaviour {
    public int MaxHealth = 1;
    public int Health { get; private set; }
    public delegate void OnDeathAction();
    public OnDeathAction MyOnDeath { get; set; }

    public float ImmunePeriod;
    private float immuneTimer;
    public bool Immune { get; set; }

    // Use this for initialization
    void Start () {
        immuneTimer = 0;
        Health = MaxHealth;
        if(MyOnDeath == null)
        {
            MyOnDeath = OnDeath;
        }
	}
	
	// Update is called once per frame
	void Update () {
        if(immuneTimer > 0)
        {
            immuneTimer -= Time.deltaTime;
        }
    }

    // Lowers health by amount 
    public void TakeDamage(int amount)
    {
        if(amount < 0 || immuneTimer > 0 || Immune)
        {
            return;
        }
        Health -= amount;
        if(Health <= 0)
        {
            MyOnDeath();
        }
        immuneTimer = ImmunePeriod;
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
        Destroy(gameObject);
    }
}
