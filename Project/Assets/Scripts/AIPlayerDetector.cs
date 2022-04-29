using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPlayerDetector : MonoBehaviour
{
    [SerializeField] Transform castPoint;
    Transform playerTransform;
    LayerMask playerLayer;
    Rigidbody2D rbEnemy;

    void Start()
    {
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();
        playerLayer = GetComponent<EnemyCombat>().playerLayer;
        rbEnemy = GetComponent<Rigidbody2D>();
    }

    public bool CanSeePlayer(float distance)
    {
        float castDist = distance;

        float degree = rbEnemy.rotation + 45f;
        Vector3 direction = (Vector3)(Quaternion.Euler(0, 0, degree) * Vector3.one);
        Vector3 endPos = castPoint.position + direction * distance;
        RaycastHit2D hit = Physics2D.Linecast(castPoint.position, endPos, playerLayer);
        if (hit.collider != null)
            return true;

        Debug.DrawLine(castPoint.position, endPos, Color.red);

        return false;
    }
}
