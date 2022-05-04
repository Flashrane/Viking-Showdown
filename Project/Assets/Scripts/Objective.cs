using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Objective : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI currentObjective;
    public string[] objectives;
    int objectiveIndex = 0;

    void Start()
    {
        currentObjective.text = objectives[objectiveIndex];
    }

    void Update()
    {
    }
}
