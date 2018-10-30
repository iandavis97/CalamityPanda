using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletNoBounce : BulletMovement {

    public override void OnCollisionEnter2D(Collision2D collision)
    {
        Damagable other = collision.gameObject.GetComponent<Damagable>();
        if (other != null)
        {
            other.TakeDamage(damage);
        }
        Release();
    }
}
