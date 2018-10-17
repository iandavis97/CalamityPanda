using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParrySprite : MonoBehaviour
{
    [SerializeField] float heightAboveEnemy = 1;
    SpriteRenderer parryRenderer;
	// Use this for initialization
	void Start ()
    {
        parryRenderer = GetComponentsInChildren<SpriteRenderer>()[1];
	}
	
	// Update is called once per frame
	void Update ()
    {
        parryRenderer.gameObject.transform.position = this.transform.position + Vector3.up * heightAboveEnemy;
        parryRenderer.gameObject.transform.rotation = Quaternion.identity;

    }
}
