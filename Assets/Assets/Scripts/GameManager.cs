using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Scene = UnityEngine.SceneManagement.Scene;

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
    private int startLives = 3;

    [SerializeField]
    private int startContinues = 2;

    [SerializeField]
    private GameObject contador, contadorBaterias;

    [SerializeField]
    private AudioClip hitClip, deathClip, takeBateryClip;

    [SerializeField]
    private Canvas canvas;

    [SerializeField]
    private GameObject PanelGameOver;

    private float timeLevel = 90;
    private int lives = 3;
    private int continues = 2;
    private int batery = 0;
    private TextMeshProUGUI contadorText;
    private TextMeshProUGUI contadorBateriasText;

    public List<GameObject> imagenesVidas;


    private Scene actualScene;

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
        this.WakeUpGame();
        if(actualScene.buildIndex != 0)
        {
            this.contadorText = this.contador.GetComponent<TextMeshProUGUI>();
            this.contadorBateriasText = this.contadorBaterias.GetComponent<TextMeshProUGUI>();
        }
    }

    private TextMeshProUGUI GetContadorText()
    {
        if(!this.contadorText)
        {
            this.contadorText = this.contador.GetComponent<TextMeshProUGUI>();
        }

        return this.contadorText;
    }

    private TextMeshProUGUI GetContadorBaterias()
    {
        if (!this.contadorBateriasText)
        {
            this.contadorBateriasText = this.contadorBaterias.GetComponent<TextMeshProUGUI>();
        }
        return this.contadorBateriasText;
    }

    private Animator GetPlayerAnimator()
    {
        if (!this.playerAnimator)
        {
            GameObject objeto = GameObject.FindGameObjectWithTag("PlayerModel");
            if (objeto != null)
            {
                this.playerAnimator = objeto.GetComponent<Animator>();
            }
            else
            {
                return null;
            }
        }

        return this.playerAnimator;
    }

    private void EjecutarAnimacion(string animacion)
    {
        Animator anim = this.GetPlayerAnimator();
        if (anim)
        {
            anim.SetTrigger(animacion);
        }
    }

    private PlayerController GetPlayerScript()
    {
        if (!playerScript)
        {
            GameObject player = GameObject.FindGameObjectWithTag("PlayerObject");
            if(player)
            {
                this.playerScript = player.GetComponent<PlayerController>();
            }
            else
            {
                return null;
            }
        }

        return playerScript;
    }


    public void Update()
    {
        if(this.actualScene.buildIndex != 0)
        {
            this.Contador();
        }
    }

    public void WakeUpGame()
    {
        this.actualScene = SceneManager.GetActiveScene();
        AudioManager.instance.EjecutarPistaMusical(this.actualScene.buildIndex);
    }

    public void StartLevel()
    {
        this.PanelGameOver.SetActive(false);
        this.WakeUpGame();
        this.canvas.gameObject.SetActive(true);
        this.timeLevel = this.startTimeLevel;
        this.lives = this.startLives;
        this.gameStart = true;
        this.EjecutarAnimacion("Running");
        AudioManager.instance.SetActiveSoundPlayer(true);
        this.GetPlayerScript();
        this.CargarVidas();

    }

    private void CargarVidas()
    {
        foreach (GameObject vida in this.imagenesVidas)
        {
            vida.SetActive(true);
        }
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    public void cambiarEscena()
    {
        SceneManager.LoadScene(1);
    }


    



    public void GameOver()
    {
        Time.timeScale = 0;
    }




    public void setAnimationTriggerPlayer(string animation)
    {
        this.EjecutarAnimacion(animation);
    }

    public void Hit()
    {
        AudioManager.instance.ReproducirSonido(this.hitClip);
        this.EjecutarAnimacion("Hit");
        this.imagenesVidas[this.lives - 1].SetActive(false);
        this.lives -= 1;
        if (this.lives <= 0)
        {
            StartCoroutine("Morir");
        }


    }

    public void TakeBatery()
    {
        if(this.lives >= 0)
        {
            AudioManager.instance.ReproducirSonido(this.takeBateryClip);
            this.batery += 1;
            this.GetContadorBaterias().text = this.batery.ToString();
        }

    }


    private void Contador()
    {
        if(this.lives > 0)
        {
            this.timeLevel -= Time.deltaTime;
            this.GetContadorText().text = Math.Round(this.timeLevel).ToString();
        }
    }


    public void VolverAlMenu()
    {
        AudioManager.instance.EjecutarPistaMusical(0);
        SceneManager.LoadScene(0);
    }


    IEnumerator Morir()
    {
        this.setAnimationTriggerPlayer("Death");
        yield return new WaitForSeconds(0.5f);
        this.GetPlayerScript().setMovePlayer(false);
        yield return new WaitForSeconds(0.6f);
        AudioManager.instance.ReproducirSonido(deathClip);
        AudioManager.instance.SetActiveSoundPlayer(false);
        yield return new WaitForSeconds(1.5f);
        this.PanelGameOver.SetActive(true);
        //mostrar menu de reiniciar
    }

}
