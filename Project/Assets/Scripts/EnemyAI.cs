using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    public Transform target;
    
    public float speed = 1000f;
    public float nextWaypointDistance = 1f;

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;
    
    Seeker seeker;
    Rigidbody2D rbEnemy;

    void Start()
    {
        seeker = GetComponent<Seeker>();
        rbEnemy = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, .5f);
    }

    void UpdatePath()
    {
        if (seeker.IsDone())
            seeker.StartPath(rbEnemy.position, target.position, OnPathComplete);
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    void FixedUpdate()
    {
        if (path == null)
            return;
        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
            reachedEndOfPath = false;

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rbEnemy.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        rbEnemy.AddForce(force);

        float distance = Vector2.Distance(rbEnemy.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

        Rotate();
    }
    
    void Rotate()
    {
        Debug.Log(rbEnemy.velocity.x + " " + rbEnemy.velocity.y);

        if (rbEnemy.velocity.x >= 0.01f && rbEnemy.velocity.y >= 0.01f)
        {
            if (rbEnemy.velocity.x <= rbEnemy.velocity.y)
                rbEnemy.rotation = 0f;
            else
                rbEnemy.rotation = -90f;
        }
        else if (rbEnemy.velocity.x >= 0.01f && rbEnemy.velocity.y <= -0.01f)
        {
            if (Mathf.Abs(rbEnemy.velocity.x) <= Mathf.Abs(rbEnemy.velocity.y))
                rbEnemy.rotation = -180f;
            else
                rbEnemy.rotation = -90f;
        }
        else if (rbEnemy.velocity.x <= -0.01f && rbEnemy.velocity.y <= -0.01f)
        {
            if (rbEnemy.velocity.x <= rbEnemy.velocity.y)
                rbEnemy.rotation = -180f;
            else
                rbEnemy.rotation = 90f;
        }
        else if (rbEnemy.velocity.x <= -0.01f && rbEnemy.velocity.y >= 0.01f)
        {
            if (Mathf.Abs(rbEnemy.velocity.x) <= Mathf.Abs(rbEnemy.velocity.y))
                rbEnemy.rotation = 0f;
            else
                rbEnemy.rotation = 90f;
        }
    }
}
