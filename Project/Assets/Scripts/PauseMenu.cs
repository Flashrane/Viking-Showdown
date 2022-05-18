using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{
    GameManager gameManager;
    new AudioManager audio;
    [SerializeField] GameObject pauseMenuUI;
    [SerializeField] GameObject controlsMenuUI;
    [SerializeField] GameObject audioMenuUI;
    [SerializeField] GameObject gameplay;
    [SerializeField] GameObject shadow;

    void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        audio = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameManager.GameIsPaused)
            {
                if (pauseMenuUI.activeSelf)
                    Resume();
                else
                {
                    audio.Play("MenuBackward");
                    controlsMenuUI.SetActive(false);
                    audioMenuUI.SetActive(false);
                    pauseMenuUI.SetActive(true);
                }
            }
            else
                Pause();
        }
    }

    public void Resume()
    {
        audio.Play("MenuBackward");
        gameManager.EnableGameplay();
        pauseMenuUI.SetActive(false);
        shadow.SetActive(false);
        gameplay.SetActive(true);
        Time.timeScale = 1f;
        GameManager.GameIsPaused = false;
    }

    void Pause()
    {
        audio.Play("ButtonSelect");
        gameManager.DisableGameplay();
        pauseMenuUI.SetActive(true);
        shadow.SetActive(true);
        gameplay.SetActive(false);
        Time.timeScale = 0f;
        GameManager.GameIsPaused = true;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        audio.Stop("GameMusic");
        Objective.sceneIndex = 0;
        GameManager.GameIsPaused = false;
        SceneManager.LoadScene("StartMenu");

        Cheat.toggled = false;
        GameManager.isPlayerAlive = true;
        PlayerController.movementSpeed = PlayerController.normalMovementSpeed;
        PlayerCombat.attackPower = PlayerCombat.normalAttackPower;
        Enemy.EnemiesRemaining = 0;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void Select()
    {
        audio.Play("ButtonSelect");
    }
}
