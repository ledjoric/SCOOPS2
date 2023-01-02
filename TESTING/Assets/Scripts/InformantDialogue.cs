using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InformantDialogue : MonoBehaviour
{
    // NPC DATA
    private Informant informantJson;
    [SerializeField] private TextAsset characterData;

    // DIALOG UI
    [SerializeField] private GameObject dialogBox, article, introPanel;
    [SerializeField] private TextMeshProUGUI dialogue;

    // DIALOG CYCLE VARIABLES
    private bool clickEnable, finishDialogue, CR_running;
    private int dialogId;
    private int progress;
    //private string[] multiDialog;

    [SerializeField] private GameData gameData;

    private void OnEnable()
    {
        informantJson = JsonUtility.FromJson<Informant>(characterData.text);
        loadCharacterData();
    }

    void Start()
    {
        clickEnable = true;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && clickEnable == true)
        {
            Debug.Log(dialogId);

            if (dialogId != informantJson.informant_dialogs.Length - 1)
            {
                if (CR_running)
                {
                    finishDialogue = true;
                    delay();
                }
                else
                {
                    dialogId++;
                    loadCharacterData();
                }
            }
            else
            {
                // END OF INTRODUCTION
                transform.root.gameObject.SetActive(false);
                dialogBox.SetActive(false);
                introPanel.SetActive(false);
                clickEnable = false;
            }
        }
    }

    public void loadCharacterData()
    {
        // DIALOGUE
        if (getIDialog(dialogId).dialog_message != "")
        {
            if (!dialogBox.activeInHierarchy)
            {
                dialogBox.SetActive(true);
            }

            StopAllCoroutines();
            StartCoroutine(TypeSentence(getIDialog(dialogId).dialog_message.Replace("#name", gameData.playerName)));
            introPanel.GetComponent<Image>().sprite = Resources.Load<Sprite>(getIDialog(dialogId).panel);
        }
        else
        {
            dialogBox.SetActive(false);
            introPanel.GetComponent<Image>().sprite = Resources.Load<Sprite>(getIDialog(dialogId).panel);
            dialogId++;
        }
    }

    // INFORMANT DIALOGUE GETTER
    public InformantDialog getIDialog(int dialogId)
    {
        return informantJson.informant_dialogs[dialogId];
    }

    private IEnumerator TypeSentence(string sentence)
    {
        CR_running = true;
        finishDialogue = false;

        dialogue.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            if (finishDialogue)
            {
                dialogue.text = sentence;
                break;
            }
            dialogue.text += letter;
            //yield return null;
            yield return new WaitForSeconds(0.01f);
        }

        CR_running = false;
    }

    /*
    IEnumerator TypeSentence(string sentence)
    {
        dialogue.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            dialogue.text += letter;
            //yield return null;
            yield return new WaitForSeconds(0.01f);
        }
    }
    */
    private void delay()
    {
        StartCoroutine(clickDelay());
    }
    private IEnumerator clickDelay()
    {
        clickEnable = false;
        yield return new WaitForSeconds(3.5f);
        clickEnable = true;
    }

    private void showDialogue()
    {
        dialogBox.SetActive(true);
    }
}
