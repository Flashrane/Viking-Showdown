using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyGFX : MonoBehaviour
{
    public AIPath aiPath;

    Rigidbody2D rbEnemy;

    private void Start()
    {
        rbEnemy = this.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (aiPath.desiredVelocity.x >= 0.01f && aiPath.desiredVelocity.y >= 0.01f)
        {
            if (aiPath.desiredVelocity.x <= aiPath.desiredVelocity.y)
                rbEnemy.rotation = 0f;
            else
                rbEnemy.rotation = -90f;
        }
        else if (aiPath.desiredVelocity.x >= 0.01f && aiPath.desiredVelocity.y <= -0.01f)
        {
            if (Mathf.Abs(aiPath.desiredVelocity.x) <= Mathf.Abs(aiPath.desiredVelocity.y))
                rbEnemy.rotation = -180f;
            else
                rbEnemy.rotation = -90f;
        }
        else if (aiPath.desiredVelocity.x <= -0.01f && aiPath.desiredVelocity.y <= -0.01f)
        {
            if (aiPath.desiredVelocity.x <= aiPath.desiredVelocity.y)
                rbEnemy.rotation = -180f;
            else
                rbEnemy.rotation = 90f;
        }
        else if (aiPath.desiredVelocity.x <= -0.01f && aiPath.desiredVelocity.y >= 0.01f)
        {
            if (Mathf.Abs(aiPath.desiredVelocity.x) <= Mathf.Abs(aiPath.desiredVelocity.y))
                rbEnemy.rotation = 0f;
            else
                rbEnemy.rotation = 90f;
        }
    }
}
