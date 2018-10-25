using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHolder : MonoBehaviour {
    public enum CombatState
    {
        Waiting = 0,
        Striking = 1,
        StartUp = 2,
    }

    private Rigidbody2D physicsObject;

    public GameObject StartingWeapon;
    public WeaponPickup CurrentWeapon;
    private Collider2D pickUpCollider;

    public MeleeAttack MeleeWeapon;

    public bool Player = false;
    public CombatState CurrentState { get; set; }
    private float timer;

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
        if(MeleeWeapon == null)
        {
            MeleeWeapon = GetComponentInChildren<MeleeAttack>();
        }
        CurrentState = CombatState.Waiting;
        pickUpCollider = GetComponent<Collider2D>();
        physicsObject = GetComponentInParent<Rigidbody2D>();
	}

	// Update is called once per frame
	void Update () {
        if (CurrentState != CombatState.Waiting)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                CurrentState--;
                timer = MeleeWeapon.ActiveFrames;
                MeleeWeapon.gameObject.SetActive(CurrentState == CombatState.Striking);
            }
        }
	}

    public bool Holding()
    {
        return CurrentWeapon != null;
    }

    public bool TryFire()
    {
        if(Holding())
        {
            return CurrentWeapon.Fire(Player ? 8 : 12, physicsObject.velocity);
        }
        else if (MeleeWeapon != null && CurrentState == CombatState.Waiting)
        {
            CurrentState = CombatState.StartUp;
            timer = MeleeWeapon.StartUp;
            return true;
        }
        return false;
    }

    public bool TryPickUp()
    {
        Collider2D[] overlap = new Collider2D[10];
        ContactFilter2D filter = new ContactFilter2D();
        filter.useTriggers = true;
        int count = pickUpCollider.OverlapCollider(filter, overlap);
        WeaponPickup old = CurrentWeapon;
        Release();
        for (int i = 0; i < count; i++)
        {
            WeaponPickup pickup = overlap[i].GetComponent<WeaponPickup>();
            if(pickup != null && pickup != old && Grab(pickup))
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
            CurrentWeapon = weapon;
            weapon.transform.parent = transform;
            weapon.transform.localPosition = Vector3.zero;
            weapon.transform.localRotation = Quaternion.identity;
            weapon.transform.localScale = new Vector3(1, 1, 1);
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
            CurrentWeapon = null;
        }
    }
}
