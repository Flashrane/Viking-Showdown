using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Objective : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI currentObjective;
    public string[] objectives;
    public static int objectiveIndex = 0;

    void Start()
    {
        currentObjective.text = objectives[objectiveIndex];
    }

    void Update()
    {
        if (objectiveIndex == 0 && Enemy.EnemiesRemaining == 0)
            NextObjective();
    }

    public void NextObjective()
    {
        if (objectiveIndex + 1 == objectives.Length)
        {
            currentObjective.text = "";
            this.enabled = false;
            return;
        }
        objectiveIndex++;
        currentObjective.text = objectives[objectiveIndex];
    }
}
