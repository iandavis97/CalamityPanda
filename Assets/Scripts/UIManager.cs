using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public GameObject livesText;
    public GameObject healthText;
    public GameObject ammoText;

    private Damagable player;
    private WeaponHolder weapon;

    // Temp variables for testing
    public int lives = 3;
    public int health = 100;
    public int ammo = 100;

    // Whatever can see player health / ammo / lives
    public GameObject character;

	void Start () {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Damagable>();
        if(player != null)
        {
            weapon = player.GetComponent<Player>().Weapon;
        }
	}
	
	void Update () {
        if (player != null)
        {
            health = player.Health;
        }
        // link up to lives from character object 
        livesText.GetComponent<Text>().text = lives.ToString();
        healthText.GetComponent<Text>().text = health.ToString();
        ammoText.GetComponent<Text>().text = ammo.ToString();
    }
}
