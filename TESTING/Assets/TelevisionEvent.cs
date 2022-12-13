using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TelevisionEvent : MonoBehaviour
{
    [SerializeField] private GameData gameData; // SCRIPTABLEOBJECT

    [SerializeField] private GameObject dialogbox, interactButton, cNotif;
    [SerializeField] private FollowPlayer followPlayer;
    [SerializeField] private Transform tv, player;
    [SerializeField] private Animator animator;

    [SerializeField] private TextAsset objectData;
    private ObjectDialog objectJson;

    public string[] dialogMessage;
    public int dialogCycle;
    public bool clickEnable;

    private void Update()
    {
        //if (gameData.isWatched) return;
        if (Input.GetMouseButtonDown(0) && clickEnable)
        {
            if (dialogCycle != dialogMessage.Length - 1)
            {
                dialogCycle++;
                tvDialog();
            }
            else
            {
                dialogbox.SetActive(false);
                dialogCycle = 0;
                Array.Clear(dialogMessage, 0, dialogMessage.Length);
                clickEnable = false;
                gameData.isWatched = true;
                gameData.dialogActive = false;

                if(gameData.playerSexuality == "Female")
                {
                    followPlayer.player = followPlayer.femalePlayer;
                }else
                {
                    followPlayer.player = followPlayer.malePlayer;
                }

                followPlayer.offset = new Vector3(-10f, 10f, -10f);

                if (!gameData.cluesList.Contains("clue#Dairy Mail is known for publishing articles that are factual."))
                {
                    animator.SetTrigger("NewNotif");
                    FindObjectOfType<AudioManager>().Play("PhoneNotification");
                    gameData.addClues("clue#Dairy Mail is known for publishing articles that are factual.");
                    cNotif.SetActive(true);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider collisionInfo)
    {
        if(gameData.isWatched) return;
        if (collisionInfo.CompareTag("Player"))
        {
            interactButton.GetComponent<Button>().onClick.RemoveListener(tvDialog);
            interactButton.GetComponent<Button>().onClick.AddListener(tvDialog);
        }
    }

    private void OnTriggerExit(Collider collisionInfo)
    {
        Debug.Log("Hello");
        if (gameData.isWatched) return;
        if (collisionInfo.CompareTag("Player"))
        {
            interactButton.GetComponent<Button>().onClick.RemoveListener(tvDialog);
        }
    }

    public void tvDialog()
    {
        if(!dialogbox.activeInHierarchy)
        {
            followPlayer.player = tv;
            followPlayer.offset = new Vector3(-10f, 7.4f, -10f);
            tv.GetComponent<AudioSource>().enabled = true;
        }
        dialogbox.SetActive(true);
        interactButton.SetActive(false);
        gameData.dialogActive = true;

        objectJson = JsonUtility.FromJson<ObjectDialog>(objectData.text);
        loadDialog();
    }

    public void loadDialog()
    {
        dialogbox.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = objectJson.name;
        dialogMessage = objectJson.object_dialog.Split('#');

        dialogbox.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = dialogMessage[dialogCycle];
        clickEnable = true;
    }
}
