using UnityEngine;

public class HideAdded : MonoBehaviour
{
    private void OnEnable()
    {
        FunctionTimer.Create(hidePanel, 2f);
    }

    public void hidePanel()
    {
        gameObject.SetActive(false);
    }
}
