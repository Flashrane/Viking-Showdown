using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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

    void Start()
    {
        Invoke("EnableHoverSound", 2f);
    }

    void EnableHoverSound()
    {
        AudioSource[] sources = audio.GetComponents<AudioSource>();
        foreach (AudioSource source in sources)
        {
            if (source.clip.name == "Hover")
            {
                source.volume = 1;
                return;
            }
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void Hover()
    {
        audio.Play("ButtonHover");
    }

    public void Select()
    {
        audio.Play("ButtonSelect");
    }
}
