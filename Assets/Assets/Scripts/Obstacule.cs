using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacule : MonoBehaviour
{
    private ParticleSystem particula;
    private bool da�oRealizado;

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
        if (!da�oRealizado)
        {
            GameManager.instance.Hit();
            this.da�oRealizado = true;
        }
    }
}
