using UnityEngine;

public class RadioProximity : MonoBehaviour
{
    [SerializeField] AudioSource radio;

    private void OnTriggerEnter(Collider collisionInfo)
    {
        radio.enabled = true;
    }

    private void OnTriggerExit(Collider collisionInfo)
    {
        radio.enabled = false;
    }
}
