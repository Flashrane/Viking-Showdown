using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    new AudioManager audio;
    [SerializeField] GameObject player;
    [SerializeField] GameObject shadow;
    [SerializeField] GameObject gameWinScreen;
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] GameObject gameplay;
    [SerializeField] Collider2D bossFightCollider;
    [SerializeField] GameObject menuButtonUI;

    public static bool GameIsPaused = false;
    public static bool GameHasEnded = false;
    public static bool isPlayerAlive = true;

    void Start()
    {
        audio = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    void Update()
    {
        if (!GameHasEnded)
            return;
        
        gameplay.SetActive(false);
        shadow.SetActive(true);
        Invoke("ShowMenuButton", 3f);
        if (isPlayerAlive)
        {
            GameHasEnded = false;
            bossFightCollider.enabled = false;
            gameWinScreen.SetActive(true);
            audio.Stop("GameMusic");
            Invoke("PlayWinSound", 0.3f);
        }
        else
        {
            GameHasEnded = false;
            gameOverScreen.SetActive(true);
            audio.Stop("GameMusic");
            audio.Play("GameOver");
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

    void ShowMenuButton()
    {
        menuButtonUI.SetActive(true);
    }

    void PlayWinSound()
    {
        audio.Play("GameWin");
    }
}
