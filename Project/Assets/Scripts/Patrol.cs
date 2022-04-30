using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    Rigidbody2D rb;
    EnemyAI aiScript;

    [SerializeField] float speed;
    [SerializeField] float lowestWaitTime, highestWaitTime;

    [SerializeField] Transform[] moveSpots;
    int randomSpot;

    Coroutine move;
    bool isMoveCRRunning = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        aiScript = GetComponent<EnemyAI>();

        randomSpot = Random.Range(0, moveSpots.Length);
    }

    void FixedUpdate()
    {
        if (!isMoveCRRunning)
        {
            transform.position = Vector2.MoveTowards(transform.position, moveSpots[randomSpot].position, speed * Time.deltaTime);
            Vector3 moveDirection = moveSpots[randomSpot].position - transform.position;
            aiScript.Rotate(moveDirection, 720);
        }

        float distanceToSpot = Vector2.Distance(transform.position, moveSpots[randomSpot].position);
        if (distanceToSpot < .2f)
        {
            if (move == null)
                move = StartCoroutine(StopMoving());
        }
    }

    IEnumerator StopMoving()
    {
        isMoveCRRunning = true;

        yield return new WaitForSeconds(Random.Range(lowestWaitTime, highestWaitTime));
        randomSpot = Random.Range(0, moveSpots.Length);
        
        move = null;
        isMoveCRRunning = false;
    }

    // if it hits a wall, this prevent it from getting stuck there
    private void OnCollisionEnter2D(Collision2D collision)
    {
        StartCoroutine(StopMoving());
    }
}
