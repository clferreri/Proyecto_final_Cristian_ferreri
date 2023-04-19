using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static GameManager instance { get; private set; }

    public bool gameStart = false;

    [SerializeField]
    private Animator playerAnimator;

    [SerializeField]
    private PlayerController playerScript;

    [SerializeField]
    private float startTimeLevel = 90;
    [SerializeField]
    private int startLives = 3;
    [SerializeField]
    private int startContinues = 2;

    [SerializeField]
    private GameObject contador, contadorBaterias;

    [SerializeField]
    private AudioClip hitClip, deathClip, takeBateryClip;

    private float timeLevel = 90;
    private int lives = 3;
    private int continues = 2;
    private int batery = 0;
    private TextMeshProUGUI contadorText;
    private TextMeshProUGUI contadorBateriasText;

    private float contadorMuerte = 2;


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

    public void Start()
    {
        this.contadorText = this.contador.GetComponent<TextMeshProUGUI>();
        this.contadorBateriasText = this.contadorBaterias.GetComponent<TextMeshProUGUI>();
    }
    public void Update()
    {
        this.Contador();
    }

    public void StartLevel()
    {
        this.timeLevel = this.startTimeLevel;

        this.lives = this.startLives;
        this.gameStart = true;
        this.playerAnimator.SetTrigger("Running");
        AudioManager.instance.SetActiveSoundPlayer(true);
        this.playerScript.setMovePlayer(true);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene("Nivel");
    }



    public void GameOver()
    {
        Time.timeScale = 0;
    }




    public void setAnimationTriggerPlayer(string animation)
    {
        this.playerAnimator.SetTrigger(animation);
    }

    public void Hit()
    {
        AudioManager.instance.ReproducirSonido(this.hitClip);
        this.setAnimationTriggerPlayer("Hit");
        this.lives -= 1;

        if (this.lives <= 0)
        {
            StartCoroutine("Morir");
        }


    }

    public void TakeBatery()
    {
        AudioManager.instance.ReproducirSonido(this.takeBateryClip);
        this.batery += 1;
        this.contadorBateriasText.text = this.batery.ToString();
    }


    private void Contador()
    {
        this.timeLevel -= Time.deltaTime;
        this.contadorText.text = Math.Round(this.timeLevel).ToString();
    }


    IEnumerator Morir()
    {
        this.setAnimationTriggerPlayer("Death");
        yield return new WaitForSeconds(0.5f);
        this.playerScript.setMovePlayer(false);
        yield return new WaitForSeconds(0.6f);
        AudioManager.instance.ReproducirSonido(deathClip);
        AudioManager.instance.SetActiveSoundPlayer(false);
        yield return new WaitForSeconds(0.8f);
        //mostrar menu de reiniciar
    }

}
