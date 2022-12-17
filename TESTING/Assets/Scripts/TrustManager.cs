using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TrustManager : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Image fill;
    [SerializeField] private Animator animator, fadePanel;
    [SerializeField] private GameObject article, eval, btnProceed, blackPanel, conclusionPanel, clickText, blackPanelText, background, btnEnd, blackPanelLast;

    private bool clickEnable;

    [SerializeField] private GameData gameData;

    private Conclusions conclusionsJson;
    [SerializeField] private TextAsset conclusionsData;

    private string type;
    private string relConclusion;
    private string articleConclusion;
    private string finalConclusion;
    private int lastArticleType;
    private List<int> zeroOne;
    private List<int> zeros;
    private List<int> ones;

    private void OnEnable()
    {
        //zeros = new List<int>(sample.FindAll(isZero));
        //Debug.Log(zeros.Count);
        
        slider.value = gameData.currentPoints;
        fill.color = gameData.gradient.Evaluate(slider.normalizedValue);

        conclusionsJson = JsonUtility.FromJson<Conclusions>(conclusionsData.text);
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
            clickText.SetActive(false);
            clickEnable = false;
            thankYou();
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
            setConclusion();

            // CROSSFADE TRANSITION
            blackPanel.SetActive(true);
            yield return new WaitForSeconds(0.3f);
            transform.root.GetChild(1).gameObject.SetActive(false);
            transform.root.GetChild(3).gameObject.SetActive(false);
            blackPanelText.GetComponent<TextMeshProUGUI>().fontSize = 120;
            blackPanelText.GetComponent<TextMeshProUGUI>().text = "Publishing...";
            background.SetActive(true);
            fadePanel.SetBool("Proceed", true);
            yield return new WaitForSeconds(1f);

            // ENDING RESULT OR SENTENCES
            publishResult();
            yield return new WaitForSeconds(2f);
            fadePanel.SetBool("Proceed", false);
            yield return new WaitForSeconds(2f);

            // BLACK BACKGROUND
            finalText();
            yield return new WaitForSeconds(2f);
            clickText.SetActive(true);
            clickEnable = true;
        }
    }

    private void thankYou()
    {
        StartCoroutine(thanks());
    }
    private IEnumerator thanks()
    {
        yield return new WaitForSeconds(1f);
        blackPanelLast.SetActive(true);
        yield return new WaitForSeconds(10f);
        fadePanel.SetBool("Proceed", false);
        yield return new WaitForSeconds(1f);
        blackPanelText.GetComponent<TextMeshProUGUI>().fontSize = 120;
        blackPanelText.GetComponent<TextMeshProUGUI>().text = "Thank you for playing our demo!";
        fadePanel.SetBool("Proceed", true);
        yield return new WaitForSeconds(4f);
        btnEnd.SetActive(true);
    }

    private void publishResult()
    {
        conclusionPanel.SetActive(true);

        if((type == "000" && lastArticleType == 0) || (type == "001" && lastArticleType == 0) || (type == "011" && lastArticleType == 0) || (type == "111" && lastArticleType == 0)) // WORST ENDING
        {
            conclusionPanel.transform.GetChild(1).GetComponent<Image>().color = new Color32(169, 53, 37, 127);
            conclusionPanel.transform.GetChild(2).GetComponent<Image>().sprite = Resources.Load<Sprite>("CRA/WORST");
            conclusionPanel.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "Oh no!";
            conclusionPanel.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = relConclusion + " " + articleConclusion;
        }
        else if((type == "001" && lastArticleType == 1) || (type == "011" && lastArticleType == 1) || (type == "111" && lastArticleType == 2) || (type == "011" && lastArticleType == 2) || (type == "001" && lastArticleType == 2))
        {
            conclusionPanel.transform.GetChild(1).GetComponent<Image>().color = new Color32(158, 169, 43, 127);
            conclusionPanel.transform.GetChild(2).GetComponent<Image>().sprite = Resources.Load<Sprite>("CRA/BAD");
            conclusionPanel.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "Close!";
            conclusionPanel.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = relConclusion + " " + articleConclusion;
        }
        else if(type == "111" && lastArticleType == 1)
        {
            conclusionPanel.transform.GetChild(1).GetComponent<Image>().color = new Color32(37, 169, 43, 127);
            conclusionPanel.transform.GetChild(2).GetComponent<Image>().sprite = Resources.Load<Sprite>("CRA/GOOD");
            conclusionPanel.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "Success!";
            conclusionPanel.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = relConclusion + " " + articleConclusion;
        }
    }

    private void finalText()
    {
        conclusionPanel.SetActive(true);

        if ((type == "000" && lastArticleType == 0) || (type == "001" && lastArticleType == 0) || (type == "011" && lastArticleType == 0) || (type == "111" && lastArticleType == 0)) // WORST ENDING
        {
            blackPanelText.GetComponent<TextMeshProUGUI>().fontSize = 60;
            blackPanelText.GetComponent<TextMeshProUGUI>().text = "After causing another commotion online, netizens once again bombarded you with negative responses and backlashes, which also caught the attention of the rapper Jay C as well as Buoyancee's mother. Their current plan is to sue you in court.";
        }
        else if ((type == "001" && lastArticleType == 1) || (type == "011" && lastArticleType == 1) || (type == "111" && lastArticleType == 2) || (type == "011" && lastArticleType == 2) || (type == "001" && lastArticleType == 2))
        {
            blackPanelText.GetComponent<TextMeshProUGUI>().fontSize = 60;
            blackPanelText.GetComponent<TextMeshProUGUI>().text = "You are commended for learning the basics of the CRAAP fact-checking method before publishing an article. However, after applying for a job, the employer wants you to do better next time in order for their company to admit you as their journalist.";
        }
        else if (type == "111" && lastArticleType == 1)
        {
            blackPanelText.GetComponent<TextMeshProUGUI>().fontSize = 60;
            blackPanelText.GetComponent<TextMeshProUGUI>().text = "Because of your outstanding fact-checked article about the issue, you are one of the respected journalists who cleared up the rumors between Jay C and his soon-to-be mother-in-law. You applied in a publishing company and, in no surprise, got accepted.";
        }
    }

    private void setConclusion()
    {
        // CCONVERT GOOD AND BAD TO 0 AND 1
        foreach(int i in gameData.selectedArticles)
        {
            if(gameData.getArticle(i-1).credibility == "good")
            {
                addZeroOne(1);
            }else if(gameData.getArticle(i-1).credibility == "bad")
            {
                addZeroOne(0);
            }
        }

        // COUNT HOW MANY ZEROS AND ONES
        zeros = new List<int>(zeroOne.FindAll(isZero));
        ones = new List<int>(zeroOne.FindAll(isOne));

        // SET THE TYPE AND FIRST SENTENCE
        if(zeros.Count == 3 && ones.Count == 0)
        {
            type = "000";
            relConclusion = getConclusion(0).sentence;
        }
        else if(zeros.Count == 2 && ones.Count == 1)
        {
            type = "001";
            relConclusion = getConclusion(1).sentence;
        }
        else if (zeros.Count == 1 && ones.Count == 2)
        {
            type = "011";
            relConclusion = getConclusion(2).sentence;
        }
        else if (zeros.Count == 0 && ones.Count == 3)
        {
            type = "111";
            relConclusion = getConclusion(3).sentence;
        }

        // SET ARTICLE CONCLUSION
        if (gameData.getArticle(gameData.stageTwoArticles[0] - 1).credibility == "good")
        {
            if(gameData.getArticle(gameData.stageTwoArticles[0] - 1).id == 5)
            {
                lastArticleType = 0;
            }else if((gameData.getArticle(gameData.stageTwoArticles[0] - 1).id == 3))
            {
                lastArticleType = 2;
            }else
            {
                lastArticleType = 1;
            }
        }else if (gameData.getArticle(gameData.stageTwoArticles[0] - 1).credibility == "bad")
        {
            lastArticleType = 0;
        }

        articleConclusion = gameData.getArticle(gameData.stageTwoArticles[0] - 1).article_conclusion;

    }



    private void addZeroOne(int i)
    {
        if (zeroOne == null)
        {
            zeroOne = new List<int> { i };
        }
        else
        {
            zeroOne.Add(i);
        }
    }

    private static bool isZero(int i)
    {
        return ((i * 1) == 0);
    }

    private static bool isOne(int i)
    {
        return ((i * 1) == 1);
    }

    private Relevance_Conclusions getConclusion(int index)
    {
        return conclusionsJson.relevance_conclusion[index];
    }
}
