using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float actualSpeed;
    //private float x;
    public bool move = true;

    [SerializeField]
    private GameObject player;
    private CapsuleCollider playerColider;
    [SerializeField]
    private Camera cam;
    [SerializeField]
    private float runSpeed = 6;
    [SerializeField]
    private float multiplayerSpeed = 2;
    [SerializeField]
    private float spinSpeed = 5; 
    [SerializeField]
    private bool inGrounded = true;
    [SerializeField]
    private bool sliding = false;
    [SerializeField]
    private Rigidbody rb;
    [SerializeField]
    private float jumpForce = 10f;
    [SerializeField]
    private List<ParticleSystem> particulas;



    public float time = 0;

    public AudioClip runClip, slideClip, jumpClip, flashClip;

    private float slideCronometer = 0;
    private float dashCronometer = 0;
    private float maxAngleRotation = 16;
    private float minAngleRotation = -16;
    private bool jump = false;
    private bool dashing = false;


    public void setMovePlayer(bool move)
    {
        this.move = move;
    }


    public bool IsJumping()
    {
        if (!this.inGrounded)
        {
            Debug.Log("ahora falle");
        }
        return !this.inGrounded || this.jump;
    }
    public void setJumping(bool jump)
    {
        this.inGrounded = !jump;
    }

    // Start is called before the first frame update
    void Start()
    {
        this.playerColider = this.player.GetComponent<CapsuleCollider>();
        this.actualSpeed = this.runSpeed;
        GameManager.instance.StartLevel();
        this.setMovePlayer(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (this.move)
        {
            this.DashLogic();
            this.Movimiento();
            this.JumpLogic();
            this.SlideLogic();
        }

    }

    private void DashLogic()
    {
        this.dashCronometer += Time.deltaTime;

        if(this.dashCronometer > 0 && this.dashing){
            
            GameManager.instance.setAnimationTriggerPlayer("Finish_Dash");
            Debug.Log("fin Velocidad");
            this.dashing = false;
            this.actualSpeed = this.runSpeed;
            return;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            this.dashCronometer = -1.8f;
            this.actualSpeed = this.runSpeed * this.multiplayerSpeed;
            this.dashing = true;
            AudioManager.instance.ReproducirSonidoPlayer(this.flashClip, true);
            GameManager.instance.setAnimationTriggerPlayer("Dash");
        }
    }


    private void Movimiento()
    {
        //float left = Input.GetAxis("Left");
        int giro = this.Giro();

        this.MoveLogic(giro);
        this.RotateLogic(giro);
        

        //this.this.player.transform.Rotate(this.Rotation(giro) * Time.deltaTime * 5);
    }

    private int Giro()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            return -1;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            return 1;
        }
        return 0;
    }
    private void JumpLogic()
    {
        if (!Input.GetKeyDown(KeyCode.Space))
        {
            return;
        }

        if (!this.inGrounded || this.sliding)
        {
            return;
        }
        AudioManager.instance.ReproducirSonidoPlayer(jumpClip);
        GameManager.instance.setAnimationTriggerPlayer("Jump");
        this.rb.AddForce(new Vector3(0, this.jumpForce, 0), ForceMode.Impulse);
        this.jump = true;

        //salto
    }

    private void SlideLogic()
    {
        this.slideCronometer += Time.deltaTime;
        if (this.slideCronometer > 0 && this.sliding)
        {
            this.sliding = false;
            this.playerColider.height = 1.816f;
            this.playerColider.center = new Vector3(-0.311f, -0.4591f, 0.197f);
            AudioManager.instance.ReproducirSonidoPlayer(this.runClip);
        }

        if (!Input.GetKeyDown(KeyCode.LeftAlt))
        {
            return;
        }

        if (!this.inGrounded || this.sliding)
        {
            return;
        }

        this.sliding = true;
        GameManager.instance.setAnimationTriggerPlayer("Slide");
        AudioManager.instance.ReproducirSonidoPlayer(this.slideClip, false);
        this.slideCronometer = -1.1f;
        this.playerColider.height = 1.2f;
        this.playerColider.center = new Vector3(-0.311f, -0.79f, 0.197f);
        foreach (ParticleSystem particula in this.particulas)
        {
            particula.Play();
        }
    }

    private void MoveLogic(int giro)
    {
        this.transform.Translate(giro * Time.deltaTime * this.spinSpeed, 0, Time.deltaTime * this.actualSpeed);
        this.cam.transform.position = new Vector3(this.player.transform.position.x - 0.4f, this.cam.transform.position.y, this.cam.transform.position.z);
    }

    private void RotateLogic(int giro)
    { 
        float angle = Mathf.Round(this.player.transform.rotation.y * 100);
        if(giro == 0)
        {
            if(angle != 0)
            {
                int rotacion = (angle < 0) ? 1 : -1;
                this.player.transform.Rotate(new Vector3(0, rotacion * Time.deltaTime * 15, 0));
            }
            else
            {
                this.player.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
        else
        {
            if(giro == 1 && this.maxAngleRotation > angle)
            {
                this.player.transform.Rotate(new Vector3(0, giro * Time.deltaTime * 80, 0));
            }

            else if(giro == -1 && this.minAngleRotation < angle)
            {
                this.player.transform.Rotate(new Vector3(0, giro * Time.deltaTime * 80, 0));
            }
        }
    }
}
