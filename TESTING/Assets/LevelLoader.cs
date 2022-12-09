using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private Animator transition;

    public void levelLoader()
    {
        //transition.SetTrigger("Start");
        StartCoroutine(loadLevel());
    }

    IEnumerator loadLevel()
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(1);
    }
}
