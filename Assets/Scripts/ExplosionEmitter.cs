using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionEmitter : MonoBehaviour
{
    static GameObject explosion;
	// Use this for initialization
	void Start ()
    {
		if (explosion == null)
        {
            explosion = Resources.Load<GameObject>("Explosion");
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public IEnumerator Explode()
    {
        GameObject explosionInstance = Instantiate(explosion, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(1);
        Destroy(explosionInstance);
    }
}
