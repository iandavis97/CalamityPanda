using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : PowerUp {
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    protected override bool TryUse(GameObject other)
    {
        if (other.tag.Equals("Player"))
        {
            Damagable tar = other.GetComponent<Damagable>();
            if(tar != null)
            {
                tar.Heal(1);
                return true;
            }
        }
        return false;
    }
}
