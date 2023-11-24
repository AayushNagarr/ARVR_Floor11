using UnityEngine;
using Photon.Pun;

public class dooropen : MonoBehaviourPunCallbacks
{
    public float interactionDistance = 5f;
    private Animation doorAnimation;
    private bool doorOpened = false;
    private bool playerInZone = false;

    void Start()
    {
        doorAnimation = GetComponent<Animation>();
        if (doorAnimation == null)
        {
            Debug.LogError("Animation component not found on the door GameObject.");
        }
    }

    void Update()
    {
        if (playerInZone && Input.GetKeyDown(KeyCode.F))
        {
            if (!doorOpened)
            {
                photonView.RPC("PlayDoorAnimation", RpcTarget.All, "Open");
                doorOpened = true;
            }
            else
            {
                photonView.RPC("PlayDoorAnimation", RpcTarget.All, "Close");
                doorOpened = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerInZone = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerInZone = false;
        }
    }

    [PunRPC]
    void PlayDoorAnimation(string animationName)
    {
        if (doorAnimation != null)
        {
            if (!doorAnimation.isPlaying)
            {
                doorAnimation.Stop();
                doorAnimation.Play(animationName);
            }
        }
    }
}