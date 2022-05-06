using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat : MonoBehaviour
{
    [SerializeField] LevelChanger levelChanger;
    [SerializeField] Transform boat;
    [SerializeField] Transform player;

    void Update()
    {
        MoveBoatWithPlayerUpwards();
    }

    void MoveBoatWithPlayerUpwards()
    {
        Vector2 destination = new Vector2(boat.position.x, boat.position.y + 7f);
        boat.position = Vector2.MoveTowards(boat.position, destination, Time.deltaTime);
        player.position = new Vector3(boat.position.x, boat.position.y - 0.7f, boat.position.z); ;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "seaArea")
        {
            levelChanger.FadeToNextLevel();
        }
    }
}
