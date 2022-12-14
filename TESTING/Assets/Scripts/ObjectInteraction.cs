using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ObjectInteraction : MonoBehaviour
{
    [SerializeField] private GameData gameData;

    [SerializeField] private GameObject interactButton, cafeArrow, aptArrow, konbiniArrow, tv, cNotif, magazine, darkPanel;
    [SerializeField] private GameObject cryptogram;
    [SerializeField] private GameObject dialogbox;
    [SerializeField] private GameObject nameParent;
    [SerializeField] private Animator animator;

    private ObjectDialog objectJson;
    private TextAsset objectData;
    private string[] dialogMessage;
    private int dialogCycle;
    private bool clickEnable;

    private int[] apartment = { 3, 8, 15, 16 }; // APARTMENT
    private int[] cafe = Enumerable.Range(0, 17).ToArray(); // CAFE
    private int[] konbini = { 77, 78, 82, 83 }; // CONVENIENT STORE

    //static bool isInside, isOpen;

    private void OnEnable()
    {

    }

    private void Start()
    {
        gameData.isInside = false;
        gameData.isOpen = false;
        //Debug.Log(dialogCycle);
    }

    private void Update()
    {
        //if (instance != this) return;

        if (Input.GetMouseButtonDown(0) && clickEnable)
        {
            if (gameObject.name == "car2")
            {
                magazine.SetActive(false);
                darkPanel.SetActive(false);
                gameData.minigameActive = false;
                clickEnable = false;

                if (!gameData.cluesList.Contains("clue#RealCeleb News is a health-centered lifestyle magazine."))
                {
                    animator.SetTrigger("NewNotif");
                    FindObjectOfType<AudioManager>().Play("PhoneNotification");
                    gameData.addClues("clue#RealCeleb News is a health-centered lifestyle magazine.");
                    cNotif.SetActive(true);
                }
            }
            else
            {
                if (dialogCycle != dialogMessage.Length - 1)
                {
                    dialogCycle++;
                    loadDialog();
                }
                else
                {
                    dialogbox.SetActive(false);
                    dialogCycle = 0;
                    Array.Clear(dialogMessage, 0, dialogMessage.Length);
                    clickEnable = false;
                    gameData.dialogActive = false;

                    if (gameObject.name == "Radio")
                    {
                        if (!gameData.cluesList.Contains("clue#BBZ News won multiple awards in Excellence in Online Journalism!"))
                        {
                            animator.SetTrigger("NewNotif");
                            FindObjectOfType<AudioManager>().Play("PhoneNotification");
                            gameData.addClues("clue#BBZ News won multiple awards in Excellence in Online Journalism!");
                            cNotif.SetActive(true);
                        }
                    }
                }
            }

        }
    }

    private void OnTriggerEnter(Collider collisionInfo)
    {
        if (collisionInfo.CompareTag("Player"))
        {
            if (gameObject.CompareTag("Door"))
            {
                interactButton.SetActive(true);
                interactButton.GetComponent<Image>().sprite = Resources.Load<Sprite>("InteractionAsset/OBJECT");
                interactButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = " Open";
                interactButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Left;

                interactButton.GetComponent<Button>().onClick.RemoveListener(enter);
                interactButton.GetComponent<Button>().onClick.AddListener(enter);
            }
            else if (gameObject.CompareTag("Newspaper"))
            {
                if (!gameData.cryptoDone)
                {
                    interactButton.SetActive(true);
                    interactButton.GetComponent<Image>().sprite = Resources.Load<Sprite>("InteractionAsset/OBJECT");
                    interactButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = " View";
                    interactButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Left;
                    gameObject.GetComponent<Outline>().enabled = true;

                    // ADD ONCLICK FOR NEWSPAPER
                    interactButton.GetComponent<Button>().onClick.RemoveAllListeners();
                    interactButton.GetComponent<Button>().onClick.AddListener(showCryptogram);
                }
            }
            else if (gameObject.CompareTag("ObjectDialog"))
            {
                interactButton.SetActive(true);
                interactButton.GetComponent<Image>().sprite = Resources.Load<Sprite>("InteractionAsset/OBJECT");
                if (gameObject.name == "Radio")
                {
                    interactButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = " Listen";
                }
                else if (gameObject.name == "Television")
                {
                    interactButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = " Watch";
                }
                interactButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Left;
                gameObject.GetComponent<Outline>().enabled = true;

                interactButton.GetComponent<Button>().onClick.RemoveAllListeners();
                interactButton.GetComponent<Button>().onClick.AddListener(objectDialog);
            }
            else if (gameObject.name == "car2")
            {
                interactButton.SetActive(true);
                interactButton.GetComponent<Image>().sprite = Resources.Load<Sprite>("InteractionAsset/OBJECT");
                interactButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = " View";
                interactButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Left;
                gameObject.GetComponent<Outline>().OutlineWidth = 4;

                interactButton.GetComponent<Button>().onClick.RemoveAllListeners();
                interactButton.GetComponent<Button>().onClick.AddListener(showMagazine);
            }

            if (gameObject.name == "apt_floor")
            {
                openApartment();
                gameData.isInside = true;
                aptArrow.SetActive(true);
            }
            else if (gameObject.name == "cafe_floor")
            {
                openCafe();
                gameData.isInside = true;
                cafeArrow.SetActive(true);

                foreach (Transform text in nameParent.transform)
                {
                    if (text.name == "NameText(Clone)")
                    {
                        if (text.GetComponent<TextMeshProUGUI>().text == "Vivian" || text.GetComponent<TextMeshProUGUI>().text == "Noah")
                        {
                            text.gameObject.SetActive(true);
                        }
                    }
                }
            }
            else if (gameObject.name == "konbini_floor")
            {
                openKonbini();
                gameData.isInside = true;
                konbiniArrow.SetActive(true);
                tv.GetComponent<AudioSource>().enabled = true;
                foreach (Transform text in nameParent.transform)
                {
                    if (text.name == "NameText(Clone)")
                    {
                        if (text.GetComponent<TextMeshProUGUI>().text == "Stacy")
                        {
                            text.gameObject.SetActive(true);
                        }
                    }
                }
            }
        }
    }

    private void OnTriggerExit(Collider collisionInfo)
    {
        interactButton.SetActive(false);
        interactButton.GetComponent<Button>().onClick.RemoveAllListeners();

        if (collisionInfo.CompareTag("Player"))
        {
            // TURN OFF OUTLINE / HIGHLIGHT 
            if (gameObject.CompareTag("Newspaper"))
            {
                gameObject.GetComponent<Outline>().enabled = false;
            }
            else if (gameObject.CompareTag("ObjectDialog"))
            {
                gameObject.GetComponent<Outline>().enabled = false;
            }
            else if (gameObject.name == "car2")
            {
                gameObject.GetComponent<Outline>().OutlineWidth = 0;
            }

            // WHEN LEAVING ROOMS
            if (gameObject.name == "apt_floor" || (gameObject.name == "apt_wall" && !gameData.isInside && gameData.isOpen))
            {
                closeApartment();
                gameData.isInside = false;
                gameData.isOpen = false;
            }
            else if (gameObject.name == "cafe_floor" || (gameObject.name == "cafe_wall" && !gameData.isInside && gameData.isOpen))
            {
                closeCafe();
                gameData.isInside = false;
                gameData.isOpen = false;
            }
            else if (gameObject.name == "konbini_floor" || (gameObject.name == "konbini_wall" && !gameData.isInside && gameData.isOpen))
            {
                closeKonbini();
                gameData.isInside = false;
                gameData.isOpen = false;
            }
        }
    }

    // ENTER ROOM
    public void enter()
    {
        FindObjectOfType<AudioManager>().Play("DoorSqueak");
        if (gameObject.name == "apt_door")
        {
            openApartment();
        }
        else if (gameObject.name == "cafe_door")
        {
            openCafe();
        }
        else if (gameObject.name == "konbini_door1")
        {
            openKonbini();
        }
        gameData.isOpen = true;
        interactButton.SetActive(false);
        interactButton.GetComponent<Button>().onClick.RemoveListener(enter);

    }

    // SHOW CRYPTOGRAM MINIGAME IN OBJECT
    public void showCryptogram()
    {
        FindObjectOfType<AudioManager>().Play("ButtonSound");
        cryptogram.SetActive(true);
    }

    public void objectDialog()
    {
        FindObjectOfType<AudioManager>().Play("ButtonSound");
        dialogbox.SetActive(true);
        interactButton.SetActive(false);
        gameData.dialogActive = true;
        dialogCycle = 0;

        loadJson();
        loadDialog();
    }

    public void loadJson()
    {
        if (gameObject.name == "Radio")
        {
            objectData = Resources.Load<TextAsset>("JSON/Objects/Radio");
        }
        else if (gameObject.name == "Television")
        {
            objectData = Resources.Load<TextAsset>("JSON/Objects/TV");
        }
        objectJson = JsonUtility.FromJson<ObjectDialog>(objectData.text);
    }

    public void loadDialog()
    {
        dialogbox.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = objectJson.name;
        dialogMessage = objectJson.object_dialog.Split('#');

        if (dialogMessage.Length == 1)
        {
            dialogbox.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = objectJson.object_dialog;
        }
        else if (dialogCycle < dialogMessage.Length)
        {
            dialogbox.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = dialogMessage[dialogCycle];
            clickEnable = true;
        }
    }

    public void showMagazine()
    {
        FindObjectOfType<AudioManager>().Play("ButtonSound");
        magazine.SetActive(true);
        darkPanel.SetActive(true);
        gameData.minigameActive = true;
        clickEnable = true;
    }

    // OPEN ROOMS
    public void openApartment()
    {
        for (int i = 0; i < apartment.Length; i++)
        {
            transform.root.GetChild(apartment[i]).GetComponent<MeshRenderer>().enabled = false;
        }
        transform.root.GetChild(3).gameObject.SetActive(false);
        gameData.isOpen = true;
    }

    public void openCafe()
    {
        gameData.isOpen = true;
        for (int i = 0; i < cafe.Length; i++)
        {
            if (i != 13)
            {
                transform.root.GetChild(cafe[i]).gameObject.SetActive(false);
            }
        }
        transform.root.GetChild(63).GetComponent<MeshRenderer>().enabled = false;
        transform.root.GetChild(67).GetComponent<MeshRenderer>().enabled = false;
        transform.root.GetChild(68).GetComponent<MeshRenderer>().enabled = false;
        transform.root.GetChild(59).gameObject.SetActive(false);
    }

    public void openKonbini()
    {
        gameData.isOpen = true;
        for (int i = 0; i < konbini.Length; i++)
        {
            transform.root.GetChild(konbini[i]).GetComponent<MeshRenderer>().enabled = false;
        }
        transform.root.GetChild(73).gameObject.SetActive(false);
        transform.root.GetChild(74).gameObject.SetActive(false);
    }

    // CLOSE ROOMS
    public void closeApartment()
    {
        FindObjectOfType<AudioManager>().Play("DoorSqueak");
        for (int i = 0; i < apartment.Length; i++)
        {
            transform.root.GetChild(apartment[i]).GetComponent<MeshRenderer>().enabled = true;
            transform.root.GetChild(apartment[i]).gameObject.SetActive(true);
        }
        aptArrow.SetActive(false);
        gameData.isInside = false;
        gameData.isOpen = false;
    }

    public void closeCafe()
    {
        FindObjectOfType<AudioManager>().Play("DoorSqueak");
        for (int i = 0; i < cafe.Length; i++)
        {
            if (i != 13)
            {
                transform.root.GetChild(cafe[i]).gameObject.SetActive(true);
            }
        }
        transform.root.GetChild(63).GetComponent<MeshRenderer>().enabled = true;
        transform.root.GetChild(67).GetComponent<MeshRenderer>().enabled = true;
        transform.root.GetChild(68).GetComponent<MeshRenderer>().enabled = true;
        transform.root.GetChild(59).gameObject.SetActive(true);
        cafeArrow.SetActive(false);

        foreach (Transform text in nameParent.transform)
        {
            if (text.name == "NameText(Clone)")
            {
                if (text.GetComponent<TextMeshProUGUI>().text == "Vivian" || text.GetComponent<TextMeshProUGUI>().text == "Noah")
                {
                    text.gameObject.SetActive(false);
                }
            }
        }
        gameData.isInside = false;
        gameData.isOpen = false;
    }

    public void closeKonbini()
    {
        FindObjectOfType<AudioManager>().Play("DoorSqueak");
        tv.GetComponent<AudioSource>().enabled = false;
        for (int i = 0; i < konbini.Length; i++)
        {
            transform.root.GetChild(konbini[i]).GetComponent<MeshRenderer>().enabled = true;
        }
        transform.root.GetChild(73).gameObject.SetActive(true);
        transform.root.GetChild(74).gameObject.SetActive(true);
        konbiniArrow.SetActive(false);

        foreach (Transform text in nameParent.transform)
        {
            if (text.name == "NameText(Clone)")
            {
                if (text.GetComponent<TextMeshProUGUI>().text == "Stacy")
                {
                    text.gameObject.SetActive(false);
                }
            }
        }
        gameData.isInside = false;
        gameData.isOpen = false;
    }
}
