using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MenuOpciones : MonoBehaviour
{
    [SerializeField]
    private AudioMixer audioMixer;
    public void PantallaCompleta(bool pantallaCompleta)
    {
        Screen.fullScreen = pantallaCompleta;
    }


    public void CambiarVolumenMusica(float volumen)
    {
        this.audioMixer.SetFloat("VolumenMusica", volumen);
    }

    public void CambiarVolumenSonidos(float volumen)
    {
        this.audioMixer.SetFloat("VolumenSonido", volumen);
    }
}
