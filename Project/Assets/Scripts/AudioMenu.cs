using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioMenu : MonoBehaviour
{
    [SerializeField] Slider gameplaySlider;
    [SerializeField] Slider musicSlider;

    void Awake()
    {
        gameplaySlider.value = AudioManager.gameVolume;
        musicSlider.value = AudioManager.musicVolume;
    }

    void Update()
    {
        AudioManager.gameVolume = gameplaySlider.value;
        AudioManager.musicVolume = musicSlider.value;
    }
}
