using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollowPlayer : MonoBehaviour
{
    GameObject player;

    Vector2 offset;

    void Awake()
    {
        player = GameObject.Find("Player");

        offset.x = 0;
        offset.y = 2.5f;
    }

    void FixedUpdate()
    {
        transform.position = new Vector3(player.transform.position.x + offset.x, player.transform.position.y + offset.y, transform.position.z);
    }
}
