using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingBullet : BulletMovement {
    static GameObject playerRef;
    Vector2 homingVec;
	// Use this for initialization
	new void Start ()
    {
        base.Start();
		if (playerRef == null)
        {
            playerRef = FindObjectOfType<Player>().gameObject;
        }
	}
	
	// Update is called once per frame
	new void Update ()
    {
        base.Update();
        homingVec = (playerRef.transform.position - transform.position).normalized;
        rigid.velocity = rigid.velocity + homingVec;
	}
}
