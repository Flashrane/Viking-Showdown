using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    Transform target;
    Rigidbody2D rbEnemy;
    EnemyCombat combat;
    AIPlayerDetector playerDetector;
    [SerializeField] float viewRange;

    public float speed;
    [SerializeField] float nextWaypointDistance;

    Path path;
    int currentWaypoint = 0;
    Seeker seeker;

    bool canSeePlayer = false;

    void Start()
    {
        target = GameObject.Find("Player").GetComponent<Transform>();
        combat = GetComponent<EnemyCombat>();
        seeker = GetComponent<Seeker>();
        rbEnemy = GetComponent<Rigidbody2D>();
        playerDetector = GetComponent<AIPlayerDetector>();

        seeker.enabled = false;
    }

    void FixedUpdate()
    {
        // the canSeePlayer variable prevents further callings of the CanSeePlayer function after the 
        // player is seenand enables the seeker script only after the enemy has started chasing the player
        if (!canSeePlayer)
        {
            canSeePlayer = playerDetector.CanSeePlayer(viewRange);
            if (canSeePlayer)
            {
                seeker.enabled = true;
                InvokeRepeating("UpdatePath", 0f, .1f);
            }
        }
        if (canSeePlayer)
        {
            ChasePlayer();
            if (IsPlayerInAttackRange())
                combat.Attack();
        }
    }

    void ChasePlayer()
    {
        if (path == null)
            return;
        if (currentWaypoint >= path.vectorPath.Count) // if reached the end of the path, a.k.a. the player
            return;

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

    void Rotate()
    {
        Vector2 dir = rbEnemy.velocity;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;
        rbEnemy.rotation = angle;
    }

    bool IsPlayerInAttackRange()
    {
        float distance = Vector2.Distance(transform.position, target.position);
        if (distance <= combat.attackRange)
            if (!combat.isAttacking)
                return true;
        return false;
    }
}
