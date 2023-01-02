using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] GameData gameData;

    public Transform malePlayer, femalePlayer;
    public Transform player;
    public Vector3 offset;

    private void Start()
    {
        if (gameData.playerSexuality == "Female")
        {
            if (!gameData.isResume)
            {
                femalePlayer.gameObject.SetActive(true);
                malePlayer.gameObject.SetActive(false);
            }
            player = femalePlayer;
        }
        else
        {
            if (!gameData.isResume)
            {
                femalePlayer.gameObject.SetActive(false);
                malePlayer.gameObject.SetActive(true);
            }
            player = malePlayer;
        }
    }

    private void Update()
    {
        transform.position = player.position + offset;
    }
}
