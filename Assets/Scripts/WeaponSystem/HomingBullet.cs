using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingBullet : BulletMovement {
    static GameObject playerRef;
    GameObject nearestEnemy;
    GameObject homingTarget;

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
        if (gameObject.layer == LayerMask.NameToLayer("PlayerBullet"))
        {
            homingTarget = FindNearestEnemy();
        }
        else if (gameObject.layer == LayerMask.NameToLayer("EnemyBullet"))
        {
            homingTarget = playerRef;            
        }
        if (homingTarget != null)
        {
            homingVec = (homingTarget.transform.position - transform.position).normalized;
            rigid.velocity = rigid.velocity + homingVec;
        }
        else
        {
            homingVec = Vector2.zero;
        }
	}

    GameObject FindNearestEnemy()
    {
        BasicEnemy[] enemies = GameObject.FindObjectsOfType<BasicEnemy>();
        float distanceFromTarget;
        float lowestDistance = float.MaxValue;
        GameObject nearestEnemy = null;
        foreach (BasicEnemy bE in enemies)
        {
            distanceFromTarget = Vector3.SqrMagnitude(bE.transform.position - transform.position);
            if (distanceFromTarget < lowestDistance)
            {
                lowestDistance = distanceFromTarget;
                nearestEnemy = bE.gameObject;
            }
        }
        return nearestEnemy;        
    }
}
