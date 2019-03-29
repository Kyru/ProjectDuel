using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerInput : MonoBehaviour
{
    public float speed = 6.0f;
    public float gravity = -9.8f;
    [SerializeField] private KeyCode keyMovement1;
    [SerializeField] private KeyCode keyMovement2;
    [SerializeField] private KeyCode keyReload;
    [SerializeField] GameObject Bubles;

    private Rigidbody _rigidbody;
    private Animator _animator;
    private double charge;
    private double ch_acc = 0.5;
    private double ch_max = 1;

    // Use this for initialization
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        charge=1;
    }

    // Update is called once per frame
    void Update()
    {
     
        if (Input.GetKey(keyMovement1))
        {
            Vector3 movement = new Vector3(1 * speed, 0, 0);
            _animator.SetFloat("CrabSpeed", speed);
            movement = transform.TransformDirection(movement);
            //transform.Translate(movement * Time.deltaTime);
            _rigidbody.velocity = movement;
        } else if (Input.GetKey(keyMovement2))
        {
            Vector3 movement = new Vector3(-1 * speed, 0, 0);
            _animator.SetFloat("CrabSpeed", speed);
            movement = transform.TransformDirection(movement);
            //transform.Translate(movement * Time.deltaTime);
            _rigidbody.velocity = movement;
        }else
        {
            _rigidbody.velocity = Vector3.zero;
            _animator.SetFloat("CrabSpeed", 0);
        }

        if(Input.GetKey(keyReload))
        {
        	_rigidbody.velocity = Vector3.zero;
            _animator.SetFloat("CrabSpeed", 0);
            _animator.SetBool("Reloading", true);
            Bubles.SetActive(true);
        	if(charge<ch_max){charge+=ch_acc*Time.deltaTime;}
        	else{charge=ch_max;}
        	if(this.gameObject.tag=="CrabYellow"){Messenger<double>.Broadcast(GameEvent.YELLOW_BAR,charge);}
        	if(this.gameObject.tag=="CrabBlue"){Messenger<double>.Broadcast(GameEvent.BLUE_BAR,charge);}

        }else if(charge<ch_max)
        {
            Bubles.SetActive(false);
            _animator.SetBool("Reloading", false);
            if (charge>=ch_acc*Time.deltaTime){charge-=ch_acc*Time.deltaTime;}
        	else if(charge < ch_acc*Time.deltaTime)charge=0;
    		if(this.gameObject.tag=="CrabYellow"){Messenger<double>.Broadcast(GameEvent.YELLOW_BAR,charge);}
        	if(this.gameObject.tag=="CrabBlue"){Messenger<double>.Broadcast(GameEvent.BLUE_BAR,charge);}
    	} else
        {
            _animator.SetBool("Reloading", false);
            Bubles.SetActive(false);
        }
    }

    public double get_charge(){return charge;}
    public void set_charge(double ch){charge=ch;}
}

