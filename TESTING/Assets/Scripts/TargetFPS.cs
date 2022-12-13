using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;

public class TargetFPS : MonoBehaviour
{
    public GameObject fpsCounter;
    [SerializeField] private GameData gameData;
    [SerializeField] private Animator animator;
    [SerializeField] private AudioMixer audioMixer;

    void Start()
    {
        // QualitySettings.vSyncCount = 1;
        Application.targetFrameRate = 300;
        StartCoroutine(FadeMixerGroup.StartFade(audioMixer, "Music", 4, 1));
        // Invoke("showFPS", 5);
    }

    private void Update()
    {
        //gameData.smoothSlide();
    }

    // public void showFPS()
    // {
    //     fpsCounter.SetActive(true);
    // }
}
