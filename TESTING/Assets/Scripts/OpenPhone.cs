using System.Collections;
using UnityEngine;

public class OpenPhone : MonoBehaviour
{
    [SerializeField] private GameObject invPanel, notif, aNotif, cNotif, ffNotif;
    [SerializeField] private Animator animator, notifAnim;
    [SerializeField] private GameData gameData;

    private void OnEnable()
    {
        StartCoroutine(showNotif());
        Debug.Log("Start");
    }

    public void openPhone()
    {
        animator.SetBool("ShowPhone", true);
        invPanel.SetActive(true);
    }

    public void closePhone()
    {
        animator.SetBool("ShowPhone", false);
        invPanel.SetActive(false);
    }

    IEnumerator showNotif()
    {
        yield return new WaitForSeconds(0.5f);
        if ((gameData.cluesList.Count != gameData.viewedClues.Count) || (gameData.articlesList.Count != gameData.viewedArticles.Count) || (gameData.articlesMinigame != gameData.viewedFF))
        {
            //notif.SetActive(true);
            notifAnim.SetTrigger("NewNotif");
        }

        if (gameData.articlesList.Count != gameData.viewedArticles.Count)
        {
            aNotif.SetActive(true);
        }

        if (gameData.cluesList.Count != gameData.viewedClues.Count)
        {
            cNotif.SetActive(true);
        }

        if (gameData.articlesMinigame != gameData.viewedFF)
        {
            ffNotif.SetActive(true);
        }
    }
}
