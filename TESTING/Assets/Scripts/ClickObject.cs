using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using TMPro;
using System.Collections;

public class ClickObject : MonoBehaviour
{
    [SerializeField] private GameData gameData;
    [SerializeField] private GameObject items, objects, instruction, finishDialog, darkPanel, timerText, dialogBox, hud, upperHud, phone;

    // RANDOM NUMBER HOLDER FOR CHECKING
    private int randomNumber, foundCount;

    private float timer;

    // RANDOMIZED NUMBER LIST HOLDER
    private List<int> RndmList;

    private bool clickEnable, timerStop;

    private void OnEnable()
    {
        hud.SetActive(false);
        upperHud.SetActive(false);
        phone.SetActive(false);
        resetGame();
    }

    private void OnDisable()
    {
        hud.SetActive(true);
        upperHud.SetActive(true);
        phone.SetActive(true);
    }

    private void Update()
    {
        if((Input.touchCount == 1 || Input.GetMouseButtonDown(0)) && clickEnable)
        {
            instruction.SetActive(false);

            for (int i = 0; i < objects.transform.childCount; i++)
            {
                randomNumber = UnityEngine.Random.Range(0, (objects.transform.childCount) + 1);
                while (RndmList.Contains(randomNumber))
                {
                    randomNumber = UnityEngine.Random.Range(0, (objects.transform.childCount) + 1);
                }
                RndmList[i] = randomNumber;
                // Debug.Log(objects.transform.GetChild(randomNumber-1).name);
                items.transform.GetChild(i).GetComponentInChildren<TextMeshProUGUI>().text = objects.transform.GetChild(randomNumber - 1).name;
            }
            timerStop = false;
            clickEnable = false;
        }

        if(!timerStop)
        {
            timer -= Time.deltaTime;
            timerText.GetComponent<TextMeshProUGUI>().text = Mathf.FloorToInt(timer % 60).ToString();

            if (Mathf.FloorToInt(timer % 60) == 0)
            {
                timerStop = true;
                // IF TIME IS UP PAG NATALO
                transform.parent.parent.gameObject.SetActive(false);

                dialogBox.gameObject.SetActive(true);
                gameData.dialogActive = true;
                gameData.loadedData = false;
                gameData.win = false;
            }
        }
    }

    public void objectFound()
    {
        FindObjectOfType<AudioManager>().Play("ButtonSound");
        string objectName = EventSystem.current.currentSelectedGameObject.name;

        for (int i = 0; i < items.transform.childCount; i++)
        {
            if (objectName == items.transform.GetChild(i).GetComponentInChildren<TextMeshProUGUI>().text)
            {
                if (RndmList.Contains(i + 1))
                {
                    items.transform.GetChild(i).GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
                    items.transform.GetChild(i).GetComponent<Image>().color = new Color32(118, 209, 127, 255);
                    objects.transform.GetChild(RndmList[i] - 1).GetComponent<Button>().enabled = false;
                    foundCount++;
                }
            }
        }

        if (foundCount == objects.transform.childCount)
        {
            // PAG PANALO
            Debug.Log("Spotted all the objects");
            StartCoroutine(puzzleDialog());
            timerStop = true;

            gameData.minigameProgress++;
            gameData.win = true;
        }
    }

    public void resetGame()
    {
        timerStop = true;
        foundCount = 0;
        clickEnable = true;
        instruction.SetActive(true);
        timer = 30.0f;
        RndmList = new List<int>(new int[objects.transform.childCount]);
        timerText.GetComponent<TextMeshProUGUI>().text = Mathf.FloorToInt(timer % 60).ToString();

        foreach (Transform child in items.transform)
        {
            child.GetComponentInChildren<TextMeshProUGUI>().color = new Color32(75, 75, 75, 255);
            child.GetComponentInChildren<TextMeshProUGUI>().text = "???";
            child.GetComponent<Image>().color = Color.white;
        }

        foreach (Button b in transform)
        {
            b.enabled = true;
        }
    }

    private IEnumerator puzzleDialog()
    {
        darkPanel.SetActive(true);
        finishDialog.SetActive(true);
        yield return new WaitForSeconds(2f);
        darkPanel.SetActive(false);
        finishDialog.SetActive(false);

        transform.parent.parent.gameObject.SetActive(false);
        dialogBox.gameObject.SetActive(true);
        gameData.dialogActive = true;
        gameData.loadedData = false;
    }
}
