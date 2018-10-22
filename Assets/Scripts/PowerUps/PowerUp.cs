using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUp : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(TryUse(collision.gameObject))
        {
            Destroy(gameObject);
        }
    }

    protected abstract bool TryUse(GameObject other);

}
