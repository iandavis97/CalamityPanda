using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionEmitter : MonoBehaviour
{
    static GameObject explosion;
    [SerializeField]
    float blastRadius;
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

    public void Explode()
    {
        GameObject explosionInstance = Instantiate(explosion, transform.position, Quaternion.identity);
        explosionInstance.transform.localScale = new Vector3(blastRadius, blastRadius, 1);
        //yield return new WaitForSeconds(1);
        //Destroy(explosionInstance);
        Destroy(gameObject);
    }
}
