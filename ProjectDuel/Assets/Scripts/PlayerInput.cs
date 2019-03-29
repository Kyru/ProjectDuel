using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerInput : MonoBehaviour
{
    public float speed = 6.0f;
    public float gravity = -9.8f;
    [SerializeField] private KeyCode keyMovement1;
    [SerializeField] private KeyCode keyMovement2;
    private Rigidbody _rigidbody;

    // Use this for initialization
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
     
        if (Input.GetKey(keyMovement1))
        {
            Vector3 movement = new Vector3(1 * speed, 0, 0);
            movement = transform.TransformDirection(movement);
            //transform.Translate(movement * Time.deltaTime);
            _rigidbody.velocity = movement;
        } else if (Input.GetKey(keyMovement2))
        {
            Vector3 movement = new Vector3(-1 * speed, 0, 0);
            movement = transform.TransformDirection(movement);
            //transform.Translate(movement * Time.deltaTime);
            _rigidbody.velocity = movement;
        }
        else
        {
            _rigidbody.velocity = new Vector3(0f, 0f, 0f);
        }
    }
}

