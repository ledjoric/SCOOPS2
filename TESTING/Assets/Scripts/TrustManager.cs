using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TrustManager : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Image fill;
    [SerializeField] private Animator animator, fadePanel;
    [SerializeField] private GameObject article, eval, btnProceed, blackPanel, conclusionPanel, clickText, blackPanelText;

    private bool clickEnable;

    [SerializeField] private GameData gameData;

    private void OnEnable()
    {
        slider.value = gameData.currentPoints;
        fill.color = gameData.gradient.Evaluate(slider.normalizedValue);
    }

    private void Start()
    {
        slider.maxValue = gameData.maxPoints;
    }

    private void Update()
    {
        if (gameData.currentPoints != slider.value)
        {
            float smoothPoint = Mathf.SmoothDamp(slider.value, gameData.currentPoints, ref gameData.velocity, 100 * Time.deltaTime);
            slider.value = smoothPoint;
            fill.color = gameData.gradient.Evaluate(slider.normalizedValue);
        }

        if((Input.touchCount == 1 || Input.GetMouseButtonDown(0)) && clickEnable)
        {
            fadePanel.SetBool("Proceed", true);
            clickEnable = false;
        }
    }

    public void proceed()
    {
        FindObjectOfType<AudioManager>().Play("ButtonSound");
        animator.SetBool("IsConfirm", false);
        btnProceed.SetActive(false);
        StartCoroutine(showNewArticles());
        //  content.GetComponent<LoadArticles>().loadStageTwo();
    }

    private IEnumerator showNewArticles()
    {
        yield return new WaitForSeconds(0.3f);
        if(gameData.stage < 3)
        {
            article.SetActive(true);
        }else
        {
            blackPanel.SetActive(true);
            yield return new WaitForSeconds(0.3f);
            blackPanelText.GetComponent<TextMeshProUGUI>().fontSize = 120;
            blackPanelText.GetComponent<TextMeshProUGUI>().text = "Publishing...";
            fadePanel.SetBool("Proceed", true);
            yield return new WaitForSeconds(1f);
            //Debug.Log(gameData.getArticle(gameData.stageTwoArticles[0]-1).credibility);
            publishResult();
            yield return new WaitForSeconds(2f);
            fadePanel.SetBool("Proceed", false);
            yield return new WaitForSeconds(2f);
            finalText();
            yield return new WaitForSeconds(2f);
            clickText.SetActive(true);
            clickEnable = true;
        }
    }

    private void publishResult()
    {
        conclusionPanel.SetActive(true);
        if (gameData.getArticle(gameData.stageTwoArticles[0]-1).credibility == "good")
        {
            conclusionPanel.transform.GetChild(1).GetComponent<Image>().color = new Color32(37, 169, 43, 127);
            conclusionPanel.transform.GetChild(2).GetComponent<Image>().sprite = Resources.Load<Sprite>("CRA/GOOD");
            conclusionPanel.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "Success!";
            conclusionPanel.transform.GetChild(4).GetChild(0).GetComponent<TextMeshProUGUI>().text = "The article you have selected <font=\"Fredoka-Bold SDF\">checks all the marks!</font> Impressive! It is current, accurate, relevant etc etc!";
        }
        else if (gameData.getArticle(gameData.stageTwoArticles[0]-1).credibility == "bad")
        {
            conclusionPanel.transform.GetChild(1).GetComponent<Image>().color = new Color32(158, 169, 43, 127);
            conclusionPanel.transform.GetChild(2).GetComponent<Image>().sprite = Resources.Load<Sprite>("CRA/BAD");
            conclusionPanel.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "Close!";
            conclusionPanel.transform.GetChild(4).GetChild(0).GetComponent<TextMeshProUGUI>().text = "The article you have selected may be credible in one or two aspects, but it may have been wrong with some! Make sure to be wary of blablabla";
        }
        else if (gameData.getArticle(gameData.stageTwoArticles[0]-1).credibility == "worst")
        {
            conclusionPanel.transform.GetChild(1).GetComponent<Image>().color = new Color32(169, 53, 37, 127);
            conclusionPanel.transform.GetChild(2).GetComponent<Image>().sprite = Resources.Load<Sprite>("CRA/WORST");
            conclusionPanel.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "Oh no!";
            conclusionPanel.transform.GetChild(4).GetChild(0).GetComponent<TextMeshProUGUI>().text = "The article you have selected ruined your reputation even further. Published by a propagandic author, be sure to evaluate your sources before spreading information!";
        }
    }

    private void finalText()
    {
        conclusionPanel.SetActive(true);
        if (gameData.getArticle(gameData.stageTwoArticles[0] - 1).credibility == "good")
        {
            blackPanelText.GetComponent<TextMeshProUGUI>().fontSize = 60;
            blackPanelText.GetComponent<TextMeshProUGUI>().text = "Because of your outstanding, fact-checked article about the issue, you are one of the journalists who cleared up the rumors between Jay C and his soon-to-be mother-in-law. You applied in a publishing company, and in no surprise, got accepted.";
        }
        else if (gameData.getArticle(gameData.stageTwoArticles[0] - 1).credibility == "bad")
        {
            blackPanelText.GetComponent<TextMeshProUGUI>().fontSize = 60;
            blackPanelText.GetComponent<TextMeshProUGUI>().text = "You are commended for learning to perform fact-checking before publishing, but the employer wants you to do better next time for their company to admit you.";
        }
        else if (gameData.getArticle(gameData.stageTwoArticles[0] - 1).credibility == "worst")
        {
            blackPanelText.GetComponent<TextMeshProUGUI>().fontSize = 60;
            blackPanelText.GetComponent<TextMeshProUGUI>().text = "Netizens once again bombarded you with negative responses, which also caught the attention of Jay C and Buoyance’s mother. their current plan is to sue you for libel (defamation).";
        }
    }
}
