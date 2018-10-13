using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashEnemy : MonoBehaviour
{
    [SerializeField]
    [Range(0.0f, 0.3f)]
    float speed = 0.3f;
    static protected Player playerRef;
    CharacterController controller;
    // Use this for initialization
    void Start ()
    {
        controller = GetComponent<CharacterController>();
        if (playerRef == null)
        {
            playerRef = GameObject.Find("Player").GetComponent<Player>();
        }
    }

    // Update is called once per frame
    void Update ()
    {
        Vector3 deltaPosition = playerRef.transform.position - transform.position;
        transform.up = deltaPosition;
        StartCoroutine(Dash());
            }

    IEnumerator Dash()
    {
        Vector3 displacementVec = playerRef.transform.position - transform.position;
        Vector3 destination = playerRef.transform.position;        
        while (Vector3.Dot(displacementVec, destination - transform.position) > 0)
        {
            controller.Move(displacementVec.normalized * speed);
            yield return null;
        }
        yield return new WaitForSeconds(3);
        StartCoroutine(Dash());
    }
}
