using UnityEngine;
using Photon.Pun;

public class isWalking : MonoBehaviourPunCallbacks
{
    private Animator animator;
    private Vector3 lastPosition;

    void Start()
{
    // Get the Animator component from the child GameObject
    animator = GetComponentInChildren<Animator>();
    Debug.Log("Animator: " + animator);
    lastPosition = transform.position;
} 

    void Update()
{
    if (animator == null)
    {
        animator = GetComponentInChildren<Animator>();
    }

    Vector3 currentPosition = transform.position;
    if (currentPosition != lastPosition)
    {
        // The avatar is moving, so set the isWalking parameter to true
        photonView.RPC("SetisWalking", RpcTarget.All, true);
    }
    else
    {
        // The avatar is not moving, so set the isWalking parameter to false
        photonView.RPC("SetisWalking", RpcTarget.All, false);
    }
    lastPosition = currentPosition;
}

    [PunRPC]
    public void SetisWalking(bool isWalking)
    {
        animator.SetBool("isWalking", isWalking);
    }
}