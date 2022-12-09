using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DialogueManager_2 : MonoBehaviour
{
    [SerializeField] private GameData gameData;

    private Character characterJson;
    [SerializeField] private TextAsset characterData;

    [SerializeField] private GameObject dialogBox;
    [SerializeField] private Button btnInteract, btnC1, btnC2;
    [SerializeField] private TextMeshProUGUI npcName, txtC1, txtC2, dialogName, dialogMessage;

    private bool clickEnable;
    private string[] multiDialog;
    private int multiDialogCycle;

    private int dialogId;

    private void Start()
    {
        characterJson = JsonUtility.FromJson<Character>(characterData.text);
        dialogId = 1;
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0) && clickEnable)
        {
            if (multiDialog.Length > 1)
            {
                if (getDialog(dialogId).choices.Length == 0 && multiDialogCycle == multiDialog.Length - 1)
                {
                    // CLOSE DIALOG
                    dialogBox.SetActive(false);
                    clickEnable = false;
                    dialogId = 1;
                }
                else
                {
                    multiDialogCycle++;
                    loadDialog();
                }
            }
            else
            {
                // CLOSE DIALOG IF THERE IS ONLY ONE MESSAGE, NO CHOICES, AND LAST MESSAGE OF CONVO
                dialogBox.SetActive(false);
                clickEnable = false;
                dialogId = 1;
            }
        }
    }

    private void OnTriggerEnter(Collider collisionInfo)
    {
        if (collisionInfo.CompareTag("Player"))
        {
            btnInteract.gameObject.SetActive(true);
            btnInteract.GetComponent<Image>().sprite = Resources.Load<Sprite>("InteractionAsset/DIALOGUE");
            btnInteract.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = new Color32(75, 75, 75, 255);

            //buttonOwner = this;

            btnInteract.onClick.RemoveListener(onNameClick);
            btnInteract.onClick.AddListener(onNameClick);

            btnC1.onClick.RemoveListener(onChoiceClick);
            btnC1.onClick.AddListener(onChoiceClick);

            btnC2.onClick.RemoveListener(onChoiceClick);
            btnC2.onClick.AddListener(onChoiceClick);

            npcName.text = characterJson.name;
        }
    }

    private void OnTriggerExit(Collider collisionInfo)
    {
        if (collisionInfo.CompareTag("Player"))
        {
            btnInteract.onClick.RemoveListener(onNameClick);

            btnC1.onClick.RemoveListener(onChoiceClick);
            btnC2.onClick.RemoveListener(onChoiceClick);

            btnInteract.gameObject.SetActive(false);

            //buttonOwner = null;
        }
    }

    public void onNameClick()
    {
        FindObjectOfType<AudioManager>().Play("ButtonSound");

        dialogBox.SetActive(true);
        if (dialogBox.activeInHierarchy)
        {
            loadDialog();
        }
    }

    public void loadDialog()
    {
        Debug.Log(dialogId);
        dialogName.text = characterJson.name;

        multiDialog = getDialog(dialogId).dialog_message.Split('#');

        if (multiDialog.Length == 1)
        {
            dialogMessage.text = getDialog(dialogId).dialog_message;
        }
        else if (multiDialogCycle < multiDialog.Length)
        {
            dialogMessage.text = multiDialog[multiDialogCycle];
            btnC1.gameObject.SetActive(false);
            btnC2.gameObject.SetActive(false);
            clickEnable = true;
        }

        loadChoices();
    }

    public void loadChoices()
    {

        if (getDialog(dialogId).choices.Length != 0)
        {
            if(getChoice(dialogId, 0).choice_message == "" && getChoice(dialogId, 1).choice_message != "")
            {
                btnC1.gameObject.SetActive(false);
                btnC2.gameObject.SetActive(true);
                txtC2.text = getChoice(dialogId, 1).choice_message;
            }
            else
            {
                btnC1.gameObject.SetActive(true);
                btnC2.gameObject.SetActive(true);
                txtC1.text = getChoice(dialogId, 0).choice_message;
                txtC2.text = getChoice(dialogId, 1).choice_message;
            }
        }
        else
        {
            clickEnable = true;
            btnC1.gameObject.SetActive(false);
            btnC2.gameObject.SetActive(false);
        }
        
    }

    public void onChoiceClick()
    {
        if (EventSystem.current.currentSelectedGameObject.name == "Choice0")
        {
            dialogId = getChoice(dialogId, 0).target_id;
        }
        else if (EventSystem.current.currentSelectedGameObject.name == "Choice1")
        {
            dialogId = getChoice(dialogId, 1).target_id;
        }
        else
        {
            dialogId = 0;
        }

        if(dialogId == 0)
        {
            dialogBox.SetActive(false);
            dialogId = 1;
        }
        else
        {
            loadDialog();
        }
    }

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

    public Choice getChoice(int dialogId, int choiceId)
    {
        return characterJson.dialogs[dialogId-1].choices[choiceId];
    }
}
