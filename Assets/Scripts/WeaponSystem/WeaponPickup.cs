using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour {

    // External Properties
    public Transform Muzzle;
    public GameObject Bullet;
    public float BulletSpeed = 1;
    public float Range = 1;
    public float Spread = 0;
    public float RateOfFire = 1;
    public int MaxAmmo = -1;
    public int Volley = 1;
    public int Damage = 1;

    // Internal Properties
    private int currentAmmo;
    private float coolDown = 0;
    private bool held;
    private BulletPool pool;


    // Use this for initialization
    void Start()
    {
        currentAmmo = MaxAmmo;
        coolDown = 0;
        if (Muzzle == null)
        {
            Muzzle = transform;
        }
        if (BulletPool.PoolDirectory.ContainsKey(Bullet))
        {
            pool = BulletPool.PoolDirectory[Bullet];
        }
    }
	
	// Update is called once per frame
	void Update () {
		if(coolDown > 0)
        {
            coolDown -= Time.deltaTime;
        }
        Fire();
	}

    public bool Fire()
    {
        // If we are ready to fire
        if(coolDown <= 0 && HasAmmo())
        {
            // Get position and direction from the muzzle
            Vector3 pos = Muzzle.position;
            Vector3 velocity = Muzzle.forward * BulletSpeed;

            // Fire the whole volley
            for (int i = 0; i < Volley && HasAmmo(); i++)
            {
                GameObject fired = null;
                if (pool != null)
                {
                    fired = pool.GetBullet();
                }
                else
                {
                    fired = Instantiate(Bullet);
                }
                if (fired == null)
                {
                    return false;
                }
                fired.transform.position = pos;
                float angle = Random.Range(-Spread, Spread);
                BulletMovement movement = fired.GetComponent<BulletMovement>();
                if(movement != null)
                {
                    movement.SetUp(Quaternion.Euler(0, 0, angle) * velocity, Damage, Range / BulletSpeed);
                }
                currentAmmo--;
            }

            // Reset cooldown
            if(coolDown != -1)
            {
                coolDown = RateOfFire;
            }
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool HasAmmo()
    {
        return (currentAmmo > 0 || MaxAmmo == -1);
    }

    public bool TryHold()
    {
        if(held)
        {
            held = true;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Release()
    {
        held = false;
    }
}
