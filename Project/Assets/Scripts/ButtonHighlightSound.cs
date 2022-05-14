using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHighlightSound : MonoBehaviour, IPointerEnterHandler
{
    new AudioManager audio;

    void Awake()
    {
        audio = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        audio.Play("ButtonHover");
    }
}