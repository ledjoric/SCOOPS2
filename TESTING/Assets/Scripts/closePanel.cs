using UnityEngine;

public class closePanel : MonoBehaviour
{
    //[SerializeField] GameObject phoneLayout;
    public void exitButton()
    {
        gameObject.SetActive(false);
        FindObjectOfType<AudioManager>().Play("ButtonSound");
    }
}
