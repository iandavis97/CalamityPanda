using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashEnemy : MonoBehaviour
{
    [SerializeField]
    [Range(0.0f, 1.0f)]
    float speed = 0.5f;
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
        if (Input.GetKeyDown(KeyCode.C))
        {
            StartCoroutine(Dash());
        }
    }

    IEnumerator Dash()
    {
        Vector3 displacementVec = playerRef.transform.position - transform.position;
        Vector3 destination = playerRef.transform.position;
        float sqrDistance = Vector3.SqrMagnitude(destination - transform.position);
        float deltaLerp = 1.0f / sqrDistance;
        for (float f = 0; f < 1.0f; f += this.speed / sqrDistance)
        {
            transform.position = Vector3.Lerp(transform.position, destination, f);
            yield return null;

        }
        //while (Vector3.SqrMagnitude(transform.position - destination) > .01f)
        //{
        //    controller.SimpleMove(Vector3.right);
        //    yield return null;
        //}
    }
}
