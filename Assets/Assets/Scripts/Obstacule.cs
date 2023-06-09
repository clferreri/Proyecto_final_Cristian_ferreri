using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacule : MonoBehaviour
{
    private ParticleSystem particula;
    private bool daņoRealizado;

    private void Start()
    {
        
        this.particula = this.gameObject.GetComponentInChildren<ParticleSystem>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (this.particula)
        {
            this.particula.Play();
        }
        if (!daņoRealizado)
        {
            GameManager.instance.Hit();
            this.daņoRealizado = true;
        }
    }
}
