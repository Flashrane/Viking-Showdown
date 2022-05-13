using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public LevelChanger levelChanger;
    new AudioManager audio;
    public GameObject startMenuUI;

    void Awake()
    {
        audio = GameObject.Find("AudioManager").GetComponent<AudioManager>();

        audio.Play("StartMenuMusic");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
