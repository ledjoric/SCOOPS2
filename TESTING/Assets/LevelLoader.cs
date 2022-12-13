using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private Animator transition;
    [SerializeField] private TextMeshProUGUI tipsText;
    [SerializeField] private AudioMixer audioMixer;

    [SerializeField] private TextAsset tipsData;
    private Tips tipsJson;

    private int random;

    private void Start()
    {
        tipsJson = JsonUtility.FromJson<Tips>(tipsData.text);
    }

    public void levelLoader()
    {
        //transition.SetTrigger("Start");
        randomTips();
        StartCoroutine(loadLevel());
    }

    public void mainMenuLoader()
    {
        randomTips();
        StartCoroutine(loadMainMenu());
    }

    IEnumerator loadLevel()
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(1);
    }

    IEnumerator loadMainMenu()
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(0);
    }

    public void randomTips()
    {
        random = Random.Range(0, tipsJson.tips.Length);
        tipsText.text = tipsJson.tips[random].tip;
    }
}
