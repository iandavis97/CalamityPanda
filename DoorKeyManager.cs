using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorKeyManager : MonoBehaviour
{
    public List<GameObject> DoorList;//list of all locked doors in scene
    public List<GameObject> KeyList;//list of all keys in scene
	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        for (int i = 0; i < DoorList.Capacity; i++)//looping through doors
        {
            Key keyScript = KeyList[i].GetComponent<Key>();
            //checking if any keys were grabbed
            if (keyScript.KeyGrabbed==true)
            {
                //destroys corresponding door when correct key grabbed
                Destroy(DoorList[i]);
            }
        }
	}
}
