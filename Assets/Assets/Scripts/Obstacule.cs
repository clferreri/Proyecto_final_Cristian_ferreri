using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacule : MonoBehaviour
{
    private ParticleSystem particula;
    private bool dañoRealizado;

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
        if (!dañoRealizado)
        {
            GameManager.instance.Hit();
            this.dañoRealizado = true;
        }
    }
}
