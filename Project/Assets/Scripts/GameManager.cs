using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject shadow;
    [SerializeField] GameObject gameWinScreen;
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] GameObject gameplay;
    [SerializeField] Collider2D bossFightCollider;    

    public static bool GameIsPaused = false;
    public static bool GameHasEnded = false;
    public static bool isPlayerAlive = true;

    void Update()
    {
        if (!GameHasEnded)
            return;
        
        gameplay.SetActive(false);
        shadow.SetActive(true);
        if (isPlayerAlive)
        {
            bossFightCollider.enabled = false;
            gameWinScreen.SetActive(true);
        }
        else
        {
            gameOverScreen.SetActive(true);    
        }
    }

    public void DisableGameplay()
    {
        player.GetComponent<AnimationManager>().ChangeAnimationState(AnimationManager.PLAYER_IDLE);
        player.GetComponent<PlayerController>().enabled = false;
        player.GetComponent<PlayerCombat>().enabled = false;
        player.GetComponent<CapsuleCollider2D>().enabled = false;
    }

    public void EnableGameplay()
    {
        player.GetComponent<PlayerController>().enabled = true;
        player.GetComponent<PlayerCombat>().enabled = true;
        player.GetComponent<CapsuleCollider2D>().enabled = true;
    }
}
