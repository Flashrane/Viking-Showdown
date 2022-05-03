using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject endMenu;

    bool gameHasEnded = false;
    float restartDelay = 2f;

    void FreezeGameplay()
    {
        Time.timeScale = 0;

        // 
    }

    void Pause()
    {
        Time.timeScale = 0;
        
        // show ui
    }

    void Resume()
    {

    }

    public IEnumerator EndGame()
    {
        if (gameHasEnded == false)
        {
            gameHasEnded = true;
            yield return new WaitForSeconds(restartDelay);
            endMenu.SetActive(true);
        }
    }
}
