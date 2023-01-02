using UnityEngine;
using UnityEngine.Audio;

public class TargetFPS : MonoBehaviour
{
    public GameObject fpsCounter;
    [SerializeField] private GameData gameData;
    [SerializeField] private Animator animator;
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private GameObject tutorial;

    void Start()
    {
        // QualitySettings.vSyncCount = 1;
        Application.targetFrameRate = 300;
        StartCoroutine(FadeMixerGroup.StartFade(audioMixer, "Music", 4, 1));
        // Invoke("showFPS", 5);
    }

    private void OnEnable()
    {
        if (gameData.tutorial)
        {
            tutorial.SetActive(true);
        }
        else
        {
            tutorial.SetActive(false);
        }
    }

    // public void showFPS()
    // {
    //     fpsCounter.SetActive(true);
    // }
}
