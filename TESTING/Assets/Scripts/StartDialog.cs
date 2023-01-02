using UnityEngine;

public class StartDialog : MonoBehaviour
{
    [SerializeField] private GameObject dialogBox;

    void Start()
    {
        //FunctionTimer.Create(showDialogue, 3f);
    }

    private void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            if (!dialogBox.activeInHierarchy)
            {
                dialogBox.SetActive(true);
            }
        }

    }

    public void showDialogue()
    {
        dialogBox.SetActive(true);
    }
}
