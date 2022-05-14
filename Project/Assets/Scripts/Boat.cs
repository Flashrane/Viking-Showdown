using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat : MonoBehaviour
{
    new AudioManager audio;
    [SerializeField] GameManager gameManager;
    [SerializeField] LevelChanger levelChanger;
    [SerializeField] Transform boat;
    [SerializeField] GameObject player;
    [SerializeField] PolygonCollider2D seaCollider;

    void Start()
    {
        audio = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        audio.Play("Boat");
    }

    void Update()
    {
        MoveBoatWithPlayerUpwards();
    }

    void MoveBoatWithPlayerUpwards()
    {
        Vector2 destination = new Vector2(boat.position.x, boat.position.y + 7f);
        boat.position = Vector2.MoveTowards(boat.position, destination, Time.deltaTime);
        player.transform.position = new Vector3(boat.position.x, boat.position.y - 0.7f, boat.position.z); ;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Objective.sceneIndex == 2)
        {
            if (collision.name == "seaArea")
            {
                levelChanger.FadeToNextLevel();
            }
        }
        else if (Objective.sceneIndex == 3)
        {
            if (collision.name == "shore")
            {
                audio.Stop("Boat");
                seaCollider.enabled = false;
                gameManager.EnableGameplay();
                player.GetComponent<PlayerController>().hasReachedShore = true;
                this.enabled = false;
            }
        }
    }
}
