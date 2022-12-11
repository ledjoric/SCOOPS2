using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Playables;
using UnityEngine.UI;

public class Cryptogram : MonoBehaviour
{
    [SerializeField] private GameData gameData;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject letterTemplate, guideLetters, mainPanel, helpPanel, finishDialog, darkPanel, cNotif;
    [SerializeField] private ObjectInteraction newsPaper;
    private GameObject g;

    private static char[] alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
    private static char[] remainingLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
    private static char[] crypto;
    private static char[] sentence;
    private static char[] inputSentence;
    private static char cryptoLetter, inputLetter;
    private static int letterIndex, cryptoIndex, letterRemove;

    private int randomNumber;
    private List<int> RndmList;

    private void OnEnable()
    {
        gameData.minigameActive = true;
    }

    private void OnDisable()
    {
        gameData.minigameActive = false;
    }

    private void Start()
    {
        guideLetters.GetComponent<TextMeshProUGUI>().text = string.Join(" ", remainingLetters);
        sentence = "CT is a website about satire content".ToUpper().ToCharArray();

        RndmList = new List<int>(new int[alphabet.Length]);
        crypto = new char[alphabet.Length];

        // POPULATE CRYPTO ARRAY
        for (int i = 0; i < alphabet.Length; i++)
        {
            randomNumber = UnityEngine.Random.Range(0, (alphabet.Length) + 1);
            while (RndmList.Contains(randomNumber))
            {
                randomNumber = UnityEngine.Random.Range(0, (alphabet.Length) + 1);
            }
            RndmList[i] = randomNumber;
            crypto[i] = alphabet[randomNumber-1];
        }

        // CONVERT SENTENCE TO CRYPTOGRAM
        for(int i = 0; i < sentence.Length; i++)
        {
            if (sentence[i] != ' ')
            {
                letterIndex = Array.IndexOf(alphabet, sentence[i]);

                g = Instantiate(letterTemplate, transform.GetChild(0).GetChild(1));
                g.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = crypto[letterIndex].ToString();
                g.SetActive(true);
            }
            else
            {
                // HIDE GAMEOBJECT WHEN THE CHARACTER IS SPACE
                g = Instantiate(letterTemplate, transform.GetChild(0).GetChild(1));
                g.transform.GetChild(0).GetComponent<Image>().enabled = false;
                g.transform.GetChild(0).GetComponent<TMP_InputField>().enabled = false;
                g.transform.GetChild(1).GetComponent<Image>().enabled = false;
                g.transform.GetChild(2).GetComponent<TextMeshProUGUI>().color = new Color32(255, 255, 255, 0);
                g.SetActive(true);
            }
        }
    }

    public void fillOutLetters()
    {
        cryptoLetter = char.Parse(EventSystem.current.currentSelectedGameObject.transform.parent.GetChild(2).GetComponent<TextMeshProUGUI>().text);
        cryptoIndex = Array.IndexOf(crypto, cryptoLetter);
        if(EventSystem.current.currentSelectedGameObject.GetComponent<TMP_InputField>().text != "")
        {
            inputLetter = char.Parse(EventSystem.current.currentSelectedGameObject.GetComponent<TMP_InputField>().text.ToUpper());
        }else
        {
            inputLetter = ' ';
        }

        if (inputLetter == alphabet[cryptoIndex])
        {
            EventSystem.current.currentSelectedGameObject.GetComponent<Image>().color = Color.white;

            for (int i = 0; i < transform.GetChild(0).GetChild(1).childCount; i++)
            {
                if (transform.GetChild(0).GetChild(1).GetChild(i).GetChild(2).GetComponent<TextMeshProUGUI>().text == crypto[cryptoIndex].ToString())
                {
                    EventSystem.current.SetSelectedGameObject(transform.GetChild(0).GetChild(1).GetChild(i).GetChild(0).gameObject);
                    transform.GetChild(0).GetChild(1).GetChild(i).GetChild(0).GetComponent<TMP_InputField>().text = alphabet[cryptoIndex].ToString();
                    transform.GetChild(0).GetChild(1).GetChild(i).GetChild(0).GetComponent<TMP_InputField>().interactable = false;

                    remainingLetters = remainingLetters.Where(val => val != inputLetter).ToArray();
                    guideLetters.GetComponent<TextMeshProUGUI>().text = string.Join(" ", remainingLetters);
                }
            }
        }else
        {
            if (EventSystem.current.currentSelectedGameObject.GetComponent<TMP_InputField>().text == "")
            {
                EventSystem.current.currentSelectedGameObject.GetComponent<Image>().color = Color.white;
            }
            else
            {
                EventSystem.current.currentSelectedGameObject.GetComponent<Image>().color = new Color32(255, 187, 187, 255);
            }
        }

        complete();
    }

    private void complete()
    {
        // IF FINISHED
        inputSentence = new char[sentence.Length];
        for (int i = 0; i < transform.GetChild(0).GetChild(1).childCount; i++)
        {
            if (transform.GetChild(0).GetChild(1).GetChild(i).GetChild(0).GetComponent<TMP_InputField>().text != "")
            {
                inputLetter = char.Parse(transform.GetChild(0).GetChild(1).GetChild(i).GetChild(0).GetComponent<TMP_InputField>().text.ToUpper());
            }
            else
            {
                inputLetter = ' ';
            }

            inputSentence[i] = inputLetter;
        }

        if(Enumerable.SequenceEqual(inputSentence, sentence) == true)
        {
            StartCoroutine(puzzleDialog());
        }
    }

    public void help()
    {
        FindObjectOfType<AudioManager>().Play("ButtonSound");
        if (mainPanel.activeInHierarchy)
        {
            mainPanel.SetActive(false);
            helpPanel.SetActive(true);
            EventSystem.current.currentSelectedGameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Cryptogram/BACK");
        }
        else if(helpPanel.activeInHierarchy)
        {
            helpPanel.SetActive(false);
            mainPanel.SetActive(true);
            EventSystem.current.currentSelectedGameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Cryptogram/HELP");
        }
    }

    public void exit()
    {
        FindObjectOfType<AudioManager>().Play("ButtonSound");
        gameObject.SetActive(false);
    }

    private IEnumerator puzzleDialog()
    {
        yield return new WaitForSeconds(0.25f);
        darkPanel.SetActive(true);
        finishDialog.SetActive(true);
        yield return new WaitForSeconds(2f);
        darkPanel.SetActive(false);
        finishDialog.SetActive(false);

        if (!gameData.cluesList.Contains("clue#CT is a website about satire content."))
        {
            animator.SetTrigger("NewNotif");
            FindObjectOfType<AudioManager>().Play("PhoneNotification");
            gameData.addClues("clue#CT is a website about satire content.");
            cNotif.SetActive(true);
        }

        newsPaper.enabled = false;
        newsPaper.gameObject.GetComponent<BoxCollider>().enabled = false;
        newsPaper.gameObject.GetComponent<Outline>().enabled = false;
        gameData.cryptoDone = true;

        gameObject.SetActive(false);
    }
}
