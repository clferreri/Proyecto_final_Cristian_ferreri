using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Feet : MonoBehaviour
{
    public GameObject player;
    private PlayerController playerScript;

    public void Awake()
    {
        this.playerScript = player.GetComponent<PlayerController>();
    }

    private void OnTriggerStay(Collider other)
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "ground")
        {
            this.playerScript.setJumping(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ground")
        {
            Debug.Log("entre al suelo");
            GameManager.instance.setAnimationTriggerPlayer("Jump_Down");
            this.playerScript.setJumping(false);
            AudioManager.instance.ReproducirSonidoPlayer(playerScript.runClip);
        }
    }
}
