using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public static int spawned = 0;
    public static int MaxSpawn = 30;
    public GameObject enemyOnePrefab;
    public float frequency;
    private float timer; 

    // Use this for initialization
    void Start () {
        spawned = 0;
        timer = Random.Range(0, frequency);
	}
	
	// Update is called once per frame
	void Update () {

        timer -= Time.deltaTime;

		if(timer <= 0)
        {
            if (enemyOnePrefab != null && spawned < MaxSpawn)
            {
                spawned++;
                GameObject obj = Instantiate(enemyOnePrefab, transform.position, transform.rotation);
                obj.GetComponent<BasicEnemy>().MakeAlert();
            }
            timer = frequency;
        }
	}
}
