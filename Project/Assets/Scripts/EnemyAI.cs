using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    public Transform target;

    public float speed = 1000f;
    public float nextWaypointDistance = 2f;

    Path path;
    int currentWaypoint = 0;
    
    Seeker seeker;
    Rigidbody2D rbEnemy;

    public Transform attackPoint;

    [SerializeField] float attackRange = 1.6f;
    [SerializeField] float attackSpeed = 1f;
    public int attackPower = 20;
    float nextAttackTime = 0f;

    public bool isAttacking = false;

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
        if (currentWaypoint >= path.vectorPath.Count) // if reached the end of the path, that is the player
        {
            Debug.Log("Reached player");
            Attack();
            return;
        }

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
        Vector2 dir = rbEnemy.velocity;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;
        rbEnemy.rotation = angle;
    }

    void Attack()
    {

    }
}
