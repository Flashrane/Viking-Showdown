using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager : MonoBehaviour
{
    public Objective objectiveManager;

    public static event Action FoundBoatEvent;

    void Update()
    {
    }

    public static void StartFoundBoatEvent()
    {
        if (FoundBoatEvent != null)
            FoundBoatEvent();
    }
}
