using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public static AudioManager instance { get; private set; }

    [SerializeField]
    private AudioSource musica;
    [SerializeField]
    private AudioSource sonido, sonidoPlayer;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }

    public void ReproducirMusica(AudioClip clip)
    {
        this.musica.PlayOneShot(clip);
    }


    public void SetActiveSoundPlayer(bool active)
    {
        if (active)
        {
            this.sonidoPlayer.Play();
        }
        else
        {
            this.sonidoPlayer.Stop();
        }
    }
    public void ReproducirSonidoPlayer(AudioClip clip, bool oneShot = false)
    {
        if(oneShot)
        {
            this.sonidoPlayer.PlayOneShot(clip);
        }
        else
        {
            this.sonidoPlayer.clip = clip;
            this.sonidoPlayer.Play();
        }

    }

    public void ReproducirSonido(AudioClip clip)
    {
        this.sonido.PlayOneShot(clip);
    }

}
