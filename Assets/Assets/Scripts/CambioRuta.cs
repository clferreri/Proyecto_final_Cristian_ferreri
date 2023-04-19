using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CambioRuta : MonoBehaviour
{
    public enum Giro { Derecha, Izquierda}

    [SerializeField]
    private Giro direccionGiro;
    private GameObject player;
    // Start is called before the first frame update

    private void Start()
    {
        this.player = GameObject.FindGameObjectWithTag("PlayerObject");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            switch (this.direccionGiro)
            {
                case Giro.Derecha:
                    this.player.transform.Rotate(new Vector3(0, 90, 0));
                    break;
                case Giro.Izquierda:
                    this.player.transform.Rotate(new Vector3(0, -90, 0));
                    break;
                default:
                    break;
            }
            //Giro al personaje;
        }
    }

    private void rotar(int limite)
    {
        float rotacionActual = this.player.transform.rotation.y;
        for (int i = 0; i <= limite; i++)
        {
            this.player.transform.Rotate(new Vector3(0, 90, 0));
        }
        
    }
}
