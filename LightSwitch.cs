using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class LightSwitch : MonoBehaviourPunCallbacks
{
    public GameObject[] lightsToToggle;
    public GameObject txtToDisplay;
    private bool PlayerInZone;
    private Animation switchAnimation;
    private bool lightsOn;

    private void Start()
    {
        switchAnimation = GetComponent<Animation>();
        PlayerInZone = false;
        txtToDisplay.SetActive(false);
        lightsOn = true;
    }

    private void Update()
    {
        if (PlayerInZone && Input.GetKeyDown(KeyCode.F))
        {
            photonView.RPC("ToggleLights", RpcTarget.All);
        }
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
    private void ToggleLights()
    {
        foreach (GameObject lightObj in lightsToToggle)
        {
            lightObj.SetActive(!lightObj.activeSelf);
        }
        if (lightsOn)
        {
            switchAnimation.Play("off");
            lightsOn = false;
        }
        else
        {
            switchAnimation.Play("on");
            lightsOn = true;
        }
    }
}