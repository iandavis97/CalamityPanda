using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public GameObject livesText;
    public GameObject healthText;
    public GameObject ammoText;
    public GameObject restartText;
    
    private Damagable player;
    private WeaponHolder weapon;

    // Temp variables for testing
    public int lives = 3;
    public int health = 100;
    public int ammo = 100;

    // Whatever can see player health / ammo / lives
    public GameObject character;

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
        livesText.GetComponent<Text>().text = lives.ToString();
        healthText.GetComponent<Text>().text = health.ToString();
        ammoText.GetComponent<Text>().text = ammo.ToString();
    }

    public void LevelStart()
    {
        restartText.SetActive(false);
    }
}
