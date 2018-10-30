using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKillVictory : MonoBehaviour {

    public static int Count;

    static EnemyKillVictory()
    {
        Count = -1;
    }

	// Use this for initialization
	void Start () {
        Count = -1;
	}
	
	// Update is called once per frame
	void Update () {
        Count = EnemySpawner.MaxSpawn - EnemySpawner.spawned + GameObject.FindGameObjectsWithTag("Enemy").Length;
        if(Count <= 0)
        {
            LevelManager.Instance.NextLevel();
            Count = -1;
        }
    }
}
