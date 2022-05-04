using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearWithDelay : MonoBehaviour
{
    public GameObject button;
    public int seconds;

    void Start()
    {
        StartCoroutine(Appear());    
    }

    IEnumerator Appear()
    {
        yield return new WaitForSeconds(seconds);
        button.SetActive(true);
    }
}
