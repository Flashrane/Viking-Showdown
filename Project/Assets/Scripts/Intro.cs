using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intro : MonoBehaviour
{
    new AudioManager audio;

    void Awake()
    {
        audio = GameObject.Find("AudioManager").GetComponent<AudioManager>();

        audio.Play("GameMusic");
    }

    public void Select()
    {
        audio.Play("ButtonSelect");
    }
}
