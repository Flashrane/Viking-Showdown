using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowEnemy : MonoBehaviour
{
    public GameObject enemy;
    public Vector2 offset;

    void FixedUpdate()
    {
        transform.position = new Vector3(enemy.transform.position.x + offset.x, enemy.transform.position.y + offset.y, transform.position.z);
    }
}
