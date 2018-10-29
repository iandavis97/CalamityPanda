using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public GameObject enemyOnePrefab;
    public int frequency;
    private int timer; 

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        timer++;

		if(timer > frequency)
        {
            if (enemyOnePrefab != null)
            {
                Instantiate(enemyOnePrefab, transform.position, transform.rotation);
            }
            timer = 0;
        }
	}
}
