using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashEnemy : MonoBehaviour
{
    [SerializeField]
    [Range(0.0f, 1.0f)]
    float speed = 0.5f;
    static protected Player playerRef;
    Rigidbody2D rb;
    // Use this for initialization
    void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
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
        //float sqrDistance = Vector3.SqrMagnitude(destination - transform.position);
        //float deltaLerp = 1.0f / sqrDistance;
        //for (float f = 0; f < 1.0f; f += this.speed / sqrDistance)
        //{
        //    transform.position = Vector3.Lerp(transform.position, destination, f);
        //    yield return null;

        //}
        rb.velocity = displacementVec.normalized * speed;
        
        while (Vector3.Dot(displacementVec, destination - transform.position) > 0)
        {
            yield return null;
        }
        rb.velocity = Vector3.zero;
    }
}
