using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    new AudioManager audio;

    [SerializeField] GameObject startMenuUI;
    [SerializeField] GameObject controlsMenuUI;
    [SerializeField] GameObject audioMenuUI;

    void Awake()
    {
        audio = GameObject.Find("AudioManager").GetComponent<AudioManager>();

        audio.Play("StartMenuMusic");
    }

    void Start()
    {
        Invoke("EnableHoverSound", 2f);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!startMenuUI.activeSelf)
            {
                audio.Play("MenuBackward");
                controlsMenuUI.SetActive(false);
                audioMenuUI.SetActive(false);
                startMenuUI.SetActive(true);
            }
        }
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

    private void OnDestroy()
    {
        audio.Stop("StartMenuMusic");
    }
}
