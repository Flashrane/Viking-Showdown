using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Objective : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI currentObjective;
    public static int sceneIndex;
    public string[] objectives;
    public static int objectiveIndex;

    void Start()
    {
        sceneIndex = SceneManager.GetActiveScene().buildIndex;

        objectiveIndex = 0;
        currentObjective.text = objectives[objectiveIndex];
    }

    void Update()
    {
        if (sceneIndex == 2)
            if (objectiveIndex == 0 && Enemy.EnemiesRemaining == 0)
                NextObjective();
    }

    public void NextObjective()
    {
        objectiveIndex++;
        if (objectiveIndex == objectives.Length)
        {
            currentObjective.text = "";
            this.enabled = false;
            return;
        }
        currentObjective.text = objectives[objectiveIndex];
    }
}
