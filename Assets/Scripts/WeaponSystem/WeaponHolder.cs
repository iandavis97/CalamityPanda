using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHolder : MonoBehaviour {
    public GameObject StartingWeapon;
    public WeaponPickup CurrentWeapon;
    private Collider2D pickUpCollider;

    public bool Player = false;

    // Use this for initialization
    void Awake () {
        if (CurrentWeapon != null)
        {
            Grab(CurrentWeapon);
        }
        else if (StartingWeapon != null)
        {
            GameObject obj = Instantiate(StartingWeapon);
            Grab(obj.GetComponent<WeaponPickup>());
        }
        pickUpCollider = GetComponent<Collider2D>();
	}

	// Update is called once per frame
	void Update () {

	}

    public bool Holding()
    {
        return CurrentWeapon != null;
    }

    public bool TryFire()
    {
        if(Holding())
        {
            return CurrentWeapon.Fire(Player ? 8 : 12);
        }
        return false;
    }

    public bool TryPickUp()
    {
        Collider2D[] overlap = new Collider2D[10];
        ContactFilter2D filter = new ContactFilter2D();
        filter.useTriggers = true;
        int count = pickUpCollider.OverlapCollider(filter, overlap);
        for (int i = 0; i < count; i++)
        {
            WeaponPickup pickup = overlap[i].GetComponent<WeaponPickup>();
            if(pickup != null && Grab(pickup))
            {
                return true;
            }
        }
        return false;
    }

    private bool Grab(WeaponPickup weapon)
    {
        if(weapon.TryHold())
        {
            if (CurrentWeapon != null)
            {
                CurrentWeapon.Release();
                CurrentWeapon.transform.parent = null;
                CurrentWeapon.transform.position = weapon.transform.position;
                CurrentWeapon.transform.localRotation = Quaternion.identity;
            }
            CurrentWeapon = weapon;
            weapon.transform.parent = transform;
            weapon.transform.localPosition = Vector3.zero;
            weapon.transform.localRotation = Quaternion.identity;
            return true;
        }
        return false;
    }

    public void Release()
    {
        if (CurrentWeapon != null)
        {
            CurrentWeapon.Release();
            CurrentWeapon.transform.parent = null;
            CurrentWeapon.transform.position = transform.position;
            CurrentWeapon.transform.localRotation = transform.rotation;
        }
    }
}
