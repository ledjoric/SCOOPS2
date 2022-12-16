using System;
using System.Collections;
using System.Drawing;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[System.Serializable]
public class DialogueManager : MonoBehaviour
{
    /*
    // NPC INTERACTION
    private static DialogueManager buttonOwner;
    private Camera mainCamera;
    */

    // NPC DATA
    private Character characterJson;

    // FOR DIALOG FLOW
    private int multiDialogCycle;
    public static int dialogId;
    // GAME OBJECTS REFERENCE
    [SerializeField] private GameObject dialogBox, addPanel, m1, id, darkPanel, notif, cNotif, aNotif, ffNotif;
    [SerializeField] private Button nameBtn, choiceOneBtn, choiceTwoBtn;
    [SerializeField] private TextMeshProUGUI npcName, choiceOne, choiceTwo, dialogName, dialogMessage, addedText, objective;
    [SerializeField] private TextAsset characterData;
    [SerializeField] private Animator animator;

    // FOR MULTIPLE DIALOGUES WITHOUT CHOICES
    private bool clickEnable;
    private string[] multiDialog;

    // CHOICES STORAGE
    private static Choice[] choices = new Choice[2];

    //private TryInstantiate tryInstantiate = new TryInstantiate();
    private GameData gameData;

    private void Start()
    {

        // LOAD NPC DATA
        characterJson = JsonUtility.FromJson<Character>(characterData.text);
        
        // SET DEFAULT VALUE
        //dialogId = 0;
        multiDialogCycle = 0;
        clickEnable = false;

        // SET SCRIPTABLE OBJECT FOR DATA STORAGE
        gameData = Resources.Load<GameData>("GameData/GameData");
    }

    private void Update()
    {
        if(nameBtn.gameObject.activeInHierarchy)
        {
            if(Input.GetKey(KeyCode.Space))
            {
                EventSystem.current.SetSelectedGameObject(nameBtn.gameObject);
            }
        }

        // FOR ENDING DIALOGUES WITHOUT CHOICES
        if (Input.GetMouseButtonDown(0) && clickEnable == true)
        {
            if (multiDialog.Length > 1 )
            {
                if(getDialog(dialogId).choices.Length == 0 && multiDialogCycle == multiDialog.Length - 1)
                {
                    addArticles();
                    addClues();
                    closeDialog();
                }
                else
                {
                    if(getDialog(dialogId).minigame != "" && multiDialogCycle < multiDialog.Length - 1)
                    {
                        if(!gameData.idShown)
                        {
                            darkPanel.SetActive(true);
                            id.SetActive(true);
                        }
                    }else
                    {
                        multiDialogCycle++;
                        loadCharacterData();
                    }
                }
            }
            else
            {
                if (getDialog(dialogId).minigame != "")
                {
                    if(getDialog(dialogId).minigame == "spot_object")
                    {
                        m1.SetActive(true);
                    }
                    dialogBox.SetActive(false);
                    gameData.dialogActive  = false;
                }
                else
                {
                    addArticles();
                    addClues();
                    closeDialog();
                }
            }
        }

        if(gameData.idShown)
        {
            multiDialogCycle++;
            loadCharacterData();
            gameData.idShown = false;
        }

        if (gameData.dialogActive && dialogId != 0 && getDialog(dialogId).minigame != "" && !gameData.loadedData)
        {
            updateID();
            loadCharacterData();
        }
        
        // FOR NPC NAMES
        /*
        if (buttonOwner != this) return;

        if (!mainCamera) mainCamera = Camera.main;
        var position = mainCamera.WorldToScreenPoint(head.position + offset);

        //uiUse.transform.position = position;
        nameBtn.transform.position = position;
        */
    }

    private void OnTriggerEnter(Collider collisionInfo)
    {
        if (collisionInfo.CompareTag("Player"))
        {
            nameBtn.gameObject.SetActive(true);
            nameBtn.GetComponent<Image>().sprite = Resources.Load<Sprite>("InteractionAsset/DIALOGUE");
            nameBtn.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = new Color32(75,75,75,255);

            gameObject.GetComponent<Outline>().enabled = true;

            //buttonOwner = this;
            nameBtn.onClick.RemoveAllListeners();

            nameBtn.onClick.RemoveListener(onNameClick);
            nameBtn.onClick.AddListener(onNameClick);

            choiceOneBtn.onClick.RemoveListener(onChoiceClick);
            choiceOneBtn.onClick.AddListener(onChoiceClick);

            choiceTwoBtn.onClick.RemoveListener(onChoiceClick);
            choiceTwoBtn.onClick.AddListener(onChoiceClick);

            npcName.text = "Talk";
            npcName.alignment = TextAlignmentOptions.Center;
        }
    }

    private void OnTriggerExit(Collider collisionInfo)
    {
        if (collisionInfo.CompareTag("Player"))
        {
            gameObject.GetComponent<Outline>().enabled = false;

            nameBtn.onClick.RemoveListener(onNameClick);

            choiceOneBtn.onClick.RemoveListener(onChoiceClick);
            choiceTwoBtn.onClick.RemoveListener(onChoiceClick);

            nameBtn.gameObject.SetActive(false);

            //buttonOwner = null;
        }
    }

    // DIALOGUE SYSTEM
    public void onNameClick()
    {
        nameBtn.gameObject.SetActive(false);
        dialogBox.SetActive(true);
        gameData.dialogActive  = true;
        FindObjectOfType<AudioManager>().Play("ButtonSound");
        if (dialogBox.activeInHierarchy)
        {
            if (dialogMessage != null && dialogName != null)
            {
                loadCharacterData();
                interactedNPC();
            }
            else
            {
                // Debug.Log("null dialog message");
            }
        }
    }
    public void updateID()
    {
        if (gameData.win && !gameData.loadedData)
        {
            dialogId = gameData.targetId_1;
            gameData.loadedData = true;
        }
        else if (!gameData.win && !gameData.loadedData)
        {
            dialogId = gameData.targetId_2;
            gameData.loadedData = true;
        }
    }
    public void loadCharacterData()
    {
        

        choices = new Choice[2];
        for (int i = 0; i < getDialog(dialogId).choices.Length; i++)
        {
            choices[i] = getDialog(dialogId).choices[i];
        }
        Debug.Log(dialogId);
        // CHARACTER NAME
        dialogName.text = characterJson.name;

        // DIALOGUE
        multiDialog = getDialog(dialogId).dialog_message.Split('#');

        if (multiDialog.Length == 1)
        {
            dialogMessage.text = getDialog(dialogId).dialog_message;
        }
        else if (multiDialogCycle < multiDialog.Length)
        {
            dialogMessage.text = multiDialog[multiDialogCycle];
            clickEnable = true;
            choiceOneBtn.gameObject.SetActive(false);
            choiceTwoBtn.gameObject.SetActive(false);
        }

        // dialogMessage.text = getDialog(dialogId).dialog_message;
        // FOR CYCLING MULTIPLE DIALOGUES
        if ((getDialog(dialogId).choices.Length == 0 && multiDialog.Length == 1))
        {
            if(getDialog(dialogId).minigame == "")
            {
                addArticles();
                addClues();

                choiceOneBtn.gameObject.SetActive(false);
                choiceTwoBtn.gameObject.SetActive(false);
                //dialogId = 0;
                multiDialogCycle = 0;
                clickEnable = true;
            }
        }
        else if ((getDialog(dialogId).choices.Length != 0 && multiDialog.Length == 1) || (multiDialogCycle == multiDialog.Length - 1 && getDialog(dialogId).choices.Length != 0))
        {
            if(getDialog(dialogId).minigame != "")
            {
                if((getChoice(0).choice_message == "" && getChoice(1).choice_message == ""))
                {
                    clickEnable = true;
                    choiceOneBtn.gameObject.SetActive(false);
                    choiceTwoBtn.gameObject.SetActive(false);
                }
                else
                {
                    multiDialogCycle = 0;
                    clickEnable = false;
                    Array.Clear(multiDialog, 0, multiDialog.Length);
                    choiceOneBtn.gameObject.SetActive(true);
                    choiceTwoBtn.gameObject.SetActive(true);
                    loadChoices();

                    StartCoroutine(refreshChoices());
                }
            }
            else
            {
                multiDialogCycle = 0;
                clickEnable = false;
                Array.Clear(multiDialog, 0, multiDialog.Length);
                choiceOneBtn.gameObject.SetActive(true);
                choiceTwoBtn.gameObject.SetActive(true);
                loadChoices();

                StartCoroutine(refreshChoices());
            }
        }
    }

    // CHOICES
    public void loadChoices()
    {
        Choice choice1 = getChoice(0);
        Choice choice2 = getChoice(1);

        choiceOne.text = choice1.choice_message;
        choiceTwo.text = choice2.choice_message;

        if(choice1.choice_message == "")
        {
            choiceOneBtn.gameObject.SetActive(false);
        }

        if (choice2.choice_message == "")
        {
            choiceTwoBtn.gameObject.SetActive(false);
        }
    }

    // CLICK CHOICE
    public void onChoiceClick()
    {
        int newDialogId = 0;

        if (EventSystem.current.currentSelectedGameObject.name == "Choice0")
        {
            addArticles();
            addClues();
            newDialogId = getChoice(0).target_id;
        }
        else if (EventSystem.current.currentSelectedGameObject.name == "Choice1")
        {
            if(getDialog(dialogId).article == 0)
            {
                addArticles();
                addClues();
            }
            newDialogId = getChoice(1).target_id;
        }
        else
        {
            newDialogId = 0;
        }

        foreach (Choice choice in choices)
        {
            if (choice.target_id == newDialogId)
            {
                dialogId = newDialogId;
            }
        }

        if (newDialogId == 0)
        {
            closeDialog();
        }
        else
        {
            loadCharacterData();
            if(getDialog(dialogId).minigame != "")
            {
                gameData.targetId_1 = getChoice(0).target_id;
                gameData.targetId_2 = getChoice(1).target_id;
            }
        }
    }

    // DIALOGUE GETTER
    public Dialog getDialog(int dialogId)
    {
        foreach (Dialog dialog in characterJson.dialogs)
        {
            if (dialog.id == dialogId)
            {
                return dialog;
            }
        }
        return characterJson.dialogs[0];
    }

    // CHOICES GETTER
    public Choice getChoice(int index)
    {
        Choice result = choices[index];
        return result;
    }

    // ADD CLUES
    public void addArticles()
    {
        if(getDialog(dialogId).article != 0)
        {
            if(!gameData.articlesList.Contains(getDialog(dialogId).article - 1))
            {
                animator.SetTrigger("NewNotif");
                FindObjectOfType<AudioManager>().Play("PhoneNotification");
                gameData.addArticles(getDialog(dialogId).article - 1);
                aNotif.SetActive(true);
                if(gameData.articlesList.Count != 6)
                {
                    objective.text = "Collect Articles (" + gameData.articlesList.Count + "/6)";
                }else
                {
                    objective.text = "Check your laptop!";
                }
                
            }
        }
    }

    // ADD clues
    public void addClues()
    {
        if (getDialog(dialogId).clues != "")
        {
            if(!gameData.cluesList.Contains(getDialog(dialogId).clues))
            {
                animator.SetTrigger("NewNotif");
                FindObjectOfType<AudioManager>().Play("PhoneNotification");
                gameData.addClues(getDialog(dialogId).clues);
                cNotif.SetActive(true);
                
            }
        }
    }

    private void closeDialog()
    {
        changeDialogue();

        dialogBox.SetActive(false);
        gameData.dialogActive  = false;
        clickEnable = false;
        multiDialogCycle = 0;
        dialogId = 0;
    }

    private void interactedNPC()
    {
        // IF THE NPC IS INTERACTED ALREADY TURN NPC BOOL TO TRUE
        if (characterJson.name == "Barry" && gameData.guardNPC == false)
        {
            gameData.mg_ArticleInterval++;
            gameData.guardNPC = true;
            addMG_Article();
        }
        else if(characterJson.name == "Vivian" && gameData.baristaNPC == false)
        {
            gameData.mg_ArticleInterval++;
            gameData.baristaNPC = true;
            addMG_Article();
        }
        else if (characterJson.name == "Noah" && gameData.customerNPC == false)
        {
            gameData.mg_ArticleInterval++;
            gameData.customerNPC = true;
            addMG_Article();
        }
        else if (characterJson.name == "Alex" && gameData.roadNPC == false)
        {
            gameData.mg_ArticleInterval++;
            gameData.roadNPC = true;
            addMG_Article();
        }
        else if (characterJson.name == "Denise" && gameData.parkNPC == false)
        {
            gameData.mg_ArticleInterval++;
            gameData.parkNPC = true;
            addMG_Article();
        }
        else if (characterJson.name == "Ryan" && gameData.vendorNPC == false)
        {
            gameData.mg_ArticleInterval++;
            gameData.vendorNPC = true;
            addMG_Article();
        }
        else if (characterJson.name == "Cleo" && gameData.storeOwnerNPC == false)
        {
            gameData.mg_ArticleInterval++;
            gameData.storeOwnerNPC = true;
            addMG_Article();
        }
        else if (characterJson.name == "Stacy" && gameData.cashierNPC == false)
        {
            gameData.mg_ArticleInterval++;
            gameData.cashierNPC = true;
            addMG_Article();
        }
    }

    private void addMG_Article()
    {
        // ADD ARTICLE
        if (gameData.mg_ArticleInterval % 2 == 0)
        {
            animator.SetTrigger("NewNotif");
            ffNotif.SetActive(true);
            FindObjectOfType<AudioManager>().Play("PhoneNotification");
            gameData.addMG_Article(gameData.articlesMinigame);
            gameData.articlesMinigame++;
            
        }
    }

    private void changeDialogue()
    {
        if(getDialog(dialogId).path != "")
        {
            if (gameObject.name == "Denise")
            {
                characterData = Resources.Load<TextAsset>(getDialog(dialogId).path);
            }else if(gameObject.name == "Noah")
            {
                characterData = Resources.Load<TextAsset>(getDialog(dialogId).path);
            }
            else if (gameObject.name == "Ryan")
            {
                characterData = Resources.Load<TextAsset>(getDialog(dialogId).path);
            }
            else if (gameObject.name == "Vivian")
            {
                characterData = Resources.Load<TextAsset>(getDialog(dialogId).path);
            }
            else if (gameObject.name == "Barry")
            {
                characterData = Resources.Load<TextAsset>(getDialog(dialogId).path);
            }
            else if (gameObject.name == "Stacy")
            {
                characterData = Resources.Load<TextAsset>(getDialog(dialogId).path);
            }
            else if (gameObject.name == "Cleo")
            {
                characterData = Resources.Load<TextAsset>(getDialog(dialogId).path);
            }
            else if (gameObject.name == "Alex")
            {
                characterData = Resources.Load<TextAsset>(getDialog(dialogId).path);
            }
            characterJson = JsonUtility.FromJson<Character>(characterData.text);
        }
    }

    private IEnumerator refreshChoices()
    {
        choiceOneBtn.transform.parent.GetComponent<VerticalLayoutGroup>().enabled = false;
        yield return new WaitForSeconds(0.0000001f);
        choiceOneBtn.transform.parent.GetComponent<VerticalLayoutGroup>().enabled = true;
    }
}
