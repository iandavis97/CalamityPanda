using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public List<GameObject> HealthObjects;
    public GameObject enemy;
    public Text enemyText;
    public GameObject restartText;
    
    private Damagable player;
    private WeaponHolder weapon;

    // Temp variables for testing
    public int health = 100;

	void Start () {
        GameObject obj = GameObject.FindGameObjectWithTag("Player");
        if (obj)
        {
            player = obj.GetComponent<Damagable>();
            if (player != null)
            {
                weapon = player.GetComponent<Player>().Weapon;
            }
        }
        restartText.SetActive(false);
	}
	
	void Update () {
        if (player != null)
        {
            health = player.Health;
        }
        else
        {
            health = 0;
            // If we can't find the player, we aren't in a scene with action
            GameObject obj = GameObject.FindGameObjectWithTag("Player");
            if (obj)
            {
                player = obj.GetComponent<Damagable>();
                if (player != null)
                {
                    weapon = player.GetComponent<Player>().Weapon;
                }
            }
        }
        restartText.SetActive(player == null);
        // link up to lives from character object 
        for (int i = 0; i < HealthObjects.Count; i++)
        {
            HealthObjects[i].SetActive(i < health);
        }

        enemyText.enabled = (EnemyKillVictory.Count != -1);
        enemy.SetActive(EnemyKillVictory.Count != -1);
        enemyText.GetComponent<Text>().text = EnemyKillVictory.Count.ToString();
    }

    public void LevelStart()
    {
        restartText.SetActive(false);
    }
}
