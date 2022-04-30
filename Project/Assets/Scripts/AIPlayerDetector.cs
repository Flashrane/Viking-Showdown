using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPlayerDetector : MonoBehaviour
{
    [SerializeField] Transform castPoint;
    Transform playerTransform;
    
    LayerMask playerLayer;
    [SerializeField] LayerMask obstructionLayer;

    Rigidbody2D rbEnemy;

    [SerializeField] float radius;
    [Range(1,360)] [SerializeField] float angle;

    void Start()
    {
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();
        playerLayer = GetComponent<EnemyCombat>().playerLayer;
        rbEnemy = GetComponent<Rigidbody2D>();
    }

    public bool CanSeePlayer()
    {
        Collider2D rangeCheck = Physics2D.OverlapCircle(castPoint.position, radius, playerLayer);
        if (rangeCheck != null)
        {
            Transform player = rangeCheck.transform;
            float degree = rbEnemy.rotation + 45f;
            Vector2 directionOfEnemy = (Vector3)(Quaternion.Euler(0, 0, degree) * Vector3.one);
            Vector2 directionToPlayer = (player.position - castPoint.position).normalized;

            if (Vector2.Angle(directionOfEnemy, directionToPlayer) < angle / 2)
            {
                float distanceToPlayer = Vector2.Distance(castPoint.position, player.position);

                if (!Physics2D.Raycast(castPoint.position, directionToPlayer, distanceToPlayer, obstructionLayer))
                    return true;
            }
        }
        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        UnityEditor.Handles.DrawWireDisc(castPoint.position, Vector3.forward, radius);

        Vector3 angle01 = DirectionFromAngle(-castPoint.eulerAngles.z, -angle / 2);
        Vector3 angle02 = DirectionFromAngle(-castPoint.eulerAngles.z, angle / 2);

        Gizmos.DrawLine(castPoint.position, castPoint.position + angle01 * radius);
        Gizmos.DrawLine(castPoint.position, castPoint.position + angle02 * radius);
    }

    private Vector2 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;

        return new Vector2(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
