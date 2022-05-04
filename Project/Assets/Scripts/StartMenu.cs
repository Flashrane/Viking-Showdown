using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public LevelChanger levelChanger;
    public GameObject startMenuUI;

    void Update()
    {
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
