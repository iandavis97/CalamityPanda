using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUp : MonoBehaviour {

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(TryUse(collision.gameObject))
        {
            Destroy(gameObject);
        }
    }

    protected abstract bool TryUse(GameObject other);

}
