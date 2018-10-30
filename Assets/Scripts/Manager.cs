using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour 
{
	Queue<BasicEnemy> inactiveEnemies;
	[SerializeField]
	float enemyActivationInterval;
	// Use this for initialization
	void Start () 
	{
		BasicEnemy[] enemies = GameObject.FindObjectsOfType<BasicEnemy>();
		for	(int i = 0; i < enemies.Length; ++i)
		{
			enemies[i].gameObject.SetActive(false);
		}
		inactiveEnemies = new Queue<BasicEnemy>(enemies);
		StartCoroutine(ActivateEnemies());
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	IEnumerator ActivateEnemies()
	{
		yield return new WaitForSeconds(enemyActivationInterval);
		inactiveEnemies.Dequeue().gameObject.SetActive(true);
		StartCoroutine(ActivateEnemies());
	}
}
