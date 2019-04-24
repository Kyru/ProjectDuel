using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerInput : MonoBehaviour
{
    public float speed = 6.0f;
    public float gravity = -9.8f;
    [SerializeField] float speedPUCoef = 1.25f;
    [SerializeField] float speedPUTime = 10f;
    [SerializeField] double reloadPUCoef = 1.25;
    [SerializeField] double reloadPUTime = 10f;
    [SerializeField] private KeyCode keyMovement1;
    [SerializeField] private KeyCode keyMovement2;
    [SerializeField] private KeyCode keyReload;
    [SerializeField] GameObject Bubles;
    [SerializeField] GameObject Aura;
    [SerializeField] GameObject AuraBottom;
    [SerializeField] private AudioSource audioSourceBubbles;


    private Rigidbody _rigidbody;
    private Animator _animator;
    private double charge;
    private double ch_acc = 0.5;
    private double ch_acc_default;
    private double ch_max = 1;
    private float defSpeed = 6.0f;
    private float speedPULastTime;
    private float reloadPULastTime;
    private ParticleSystem bublesParticleSystem;
    private ParticleSystem auraParticleSystem;
    private ParticleSystem auraBottomParticleSystem;
    private bool crabBeingHit;
    private float timeFlickingPU;


    // Use this for initialization
    void Start()
    {
        defSpeed = speed;
        ch_acc_default = ch_acc;
        speedPULastTime = float.MaxValue;
        reloadPULastTime = float.MaxValue;
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        charge = 1;
        crabBeingHit = false;
        timeFlickingPU = 2f;
        bublesParticleSystem = Bubles.GetComponent<ParticleSystem>();
        auraParticleSystem = Aura.GetComponent<ParticleSystem>();
        auraBottomParticleSystem = AuraBottom.GetComponent<ParticleSystem>();

        bublesParticleSystem.emissionRate = 0;
        auraParticleSystem.emissionRate = 0;
        auraBottomParticleSystem.emissionRate = 0;

        Bubles.SetActive(true);

        Messenger.AddListener(GameEvent.SUDDEN_DEATH, sudden_death);

        Messenger<string>.AddListener(GameEvent.SPEED_POWERUP_ADD, addSpeedPowerUp);
        Messenger<string>.AddListener(GameEvent.RELOAD_POWERUP_ADD, increaseReloadSpeed);

    }

    // Update is called once per frame
    void Update()
    {

        if (!crabBeingHit && Time.timeScale != 0)
        {
            if (Input.GetKey(keyMovement1))
            {
                Vector3 movement = new Vector3(1 * speed, 0, 0);
                _animator.SetFloat("CrabSpeed", speed);
                movement = transform.TransformDirection(movement);
                //transform.Translate(movement * Time.deltaTime);
                _rigidbody.velocity = movement;
            }
            else if (Input.GetKey(keyMovement2))
            {
                Vector3 movement = new Vector3(-1 * speed, 0, 0);
                _animator.SetFloat("CrabSpeed", speed);
                movement = transform.TransformDirection(movement);
                //transform.Translate(movement * Time.deltaTime);
                _rigidbody.velocity = movement;
            }
            else
            {
                _rigidbody.velocity = Vector3.zero;
                _animator.SetFloat("CrabSpeed", 0);
            }

            if (Input.GetKey(keyReload))
            {
                if (!audioSourceBubbles.isPlaying) {
                    audioSourceBubbles.Play();
                }
               
                _rigidbody.velocity = Vector3.zero;
                _animator.SetFloat("CrabSpeed", 0);
                _animator.SetBool("Reloading", true);
                bublesParticleSystem.emissionRate = 4;
                if (charge < ch_max) { charge += ch_acc * Time.deltaTime; }
                else { charge = ch_max; }
                if (this.gameObject.tag == "CrabYellow") { Messenger<double>.Broadcast(GameEvent.YELLOW_BAR, charge); }
                if (this.gameObject.tag == "CrabBlue") { Messenger<double>.Broadcast(GameEvent.BLUE_BAR, charge); }

            }
            else if (charge < ch_max)
            {
                audioSourceBubbles.Stop();
                _animator.SetBool("Reloading", false);
                bublesParticleSystem.emissionRate = 0;
                if (charge >= ch_acc * Time.deltaTime) { charge -= ch_acc * Time.deltaTime; }
                else if (charge < ch_acc * Time.deltaTime) charge = 0;
                if (this.gameObject.tag == "CrabYellow") { Messenger<double>.Broadcast(GameEvent.YELLOW_BAR, charge); }
                if (this.gameObject.tag == "CrabBlue") { Messenger<double>.Broadcast(GameEvent.BLUE_BAR, charge); }
            }
            else
            {
                audioSourceBubbles.Stop();
                _animator.SetBool("Reloading", false);
                bublesParticleSystem.emissionRate = 0;
            }
        }
        else
        {
            Vector3 movement = new Vector3(0, 0, 0);
            movement = transform.TransformDirection(movement);
            _rigidbody.velocity = movement;
        }

        if (Time.time > speedPULastTime + speedPUTime)
        {
            Messenger<string, float>.Broadcast(GameEvent.SPEED_POWERUP_REMOVE, gameObject.tag, timeFlickingPU);
            Invoke("returnDefaultSpeed", timeFlickingPU);
            speedPULastTime = float.MaxValue;
        }

        if (Time.time > reloadPULastTime + reloadPUTime)
        {
            Messenger<string, float>.Broadcast(GameEvent.RELOAD_POWERUP_REMOVE, gameObject.tag, timeFlickingPU);
            Invoke("returnDefaultReloadSpeed", timeFlickingPU);
            reloadPULastTime = float.MaxValue;
        }
    }

    void sudden_death()
    {
        auraParticleSystem.emissionRate = 20f;
        auraBottomParticleSystem.emissionRate = 10f;
    }

    public double get_charge() { return charge; }
    public void set_charge(double ch) { charge = ch; }

    public bool getBeingHit() { return this.crabBeingHit; }
    public void setBeingHit(bool b) { this.crabBeingHit = b; }

    public void finishBeingHit() { this.crabBeingHit = false; }

    private void setSpeed(float speed)
    {
        this.speed = speed;
    }

    private void addSpeedPowerUp(string crab)
    {
        if (gameObject.CompareTag(crab))
        {
            if (this.speed == this.defSpeed)
                setSpeed(this.speed * this.speedPUCoef);

            speedPULastTime = Time.time;
        }
    }
    private void returnDefaultSpeed()
    {
        if (Time.time <= speedPULastTime)
            this.speed = defSpeed;
    }

    private void increaseReloadSpeed(string crab)
    {
        if (gameObject.CompareTag(crab))
        {
            if (this.ch_acc == ch_acc_default)
                ch_acc *= reloadPUCoef;

            reloadPULastTime = Time.time;
        }
    }

    private void returnDefaultReloadSpeed()
    {
        if (Time.time <= reloadPULastTime)
            ch_acc = ch_acc_default;
    }

    private void OnDestroy()
    {
        Messenger<string>.RemoveListener(GameEvent.SPEED_POWERUP_ADD, addSpeedPowerUp);
        Messenger<string>.RemoveListener(GameEvent.RELOAD_POWERUP_ADD, increaseReloadSpeed);
        Messenger.RemoveListener(GameEvent.SUDDEN_DEATH, sudden_death);
    }

    public float getTimeFlickingPU()
    {
        return timeFlickingPU;
    }

}

