using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class FanRotate : MonoBehaviourPunCallbacks
{
    public GameObject fan;
    public GameObject txtToDisplay;
    private bool PlayerInZone;
    public float spinSpeed = 1000.0f;
    private Animation switchAnimation;

    private void Start()
    {
        switchAnimation = GetComponent<Animation>();
        PlayerInZone = false;
        txtToDisplay.SetActive(false);
    }

    private void Update()
    {
        if (PlayerInZone && Input.GetKeyDown(KeyCode.F))
        {
            photonView.RPC("ToggleFanRotation", RpcTarget.All);
        }
            fan.transform.Rotate(0, spinSpeed * Time.deltaTime, 0);
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            txtToDisplay.SetActive(true);
            PlayerInZone = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerInZone = false;
            txtToDisplay.SetActive(false);
        }
    }

    [PunRPC]
    private void ToggleFanRotation()
    {
        if (spinSpeed == 0.0f)
        {
            spinSpeed = 1000.0f;
            switchAnimation.Play("on");
        }
        else
        {
            spinSpeed = 0.0f;
            switchAnimation.Play("off");
        }
    }
}