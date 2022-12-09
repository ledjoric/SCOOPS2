using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class ConfirmSelection : MonoBehaviour
{
    [SerializeField] private GameObject darkPanel, content, article, btnProceed, articleChoosing, articleEval;
    [SerializeField] private GameData gameData;
    [SerializeField] private Animator animator;

    public void selectYes()
    {
        // STAGE VARIABLE PROGRESS
        FindObjectOfType<AudioManager>().Play("ButtonSound");
        if (gameData.stage == 1)
        {
            // DESTROY RELEVANCE ARTICLES
            for (int i = 0; i < content.transform.childCount; i++)
            {
                Destroy(content.transform.GetChild(i).gameObject);
            }
            transform.root.GetChild(1).gameObject.SetActive(false);
            gameData.stage++;
            gameData.selectedLimit = 1;
            //content.GetComponent<LoadArticles>().loadStageTwo();
            gameData.selectedArticlesIndex.Clear();

            // TRUST METER CHANGE / TRANSITION
            animator.SetBool("IsConfirm", true);
            btnProceed.SetActive(true);
            article.SetActive(false);

            // POINTS TO BE ADDED OR DEDUCTED
            gameData.trustChange(gameData.totalPoints);
        }
        else if(gameData.stage == 2)
        {
            // KAPAG NAG-CONFIRM NA SA CURRENCY PART
            transform.root.GetChild(1).gameObject.SetActive(false);
            for (int i = 0; i < content.transform.childCount; i++)
            {
                Destroy(content.transform.GetChild(i).gameObject);
            }
            gameData.stage++;

            article.SetActive(false);
            articleEval.SetActive(true);
        }else if(gameData.stage == 3)
        {
            articleEval.SetActive(false);

            // TRUST METER CHANGE / TRANSITION
            animator.SetBool("IsConfirm", true);
            btnProceed.SetActive(true);

            // POINTS TO BE ADDED OR DEDUCTED
            gameData.trustChange(gameData.totalPoints);
        }

        

        // HIDE CONFIRM DIALOG
        gameObject.SetActive(false);
        darkPanel.SetActive(false);
    }

    public void selectNo()
    {
        FindObjectOfType<AudioManager>().Play("ButtonSound");
        gameObject.SetActive(false);
        darkPanel.SetActive(false);
    }
}
