using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] private GameObject ballPrefab;
    private GameObject _ball;
    private Animator _animator;
    [SerializeField] private KeyCode keyShoot;
    private AudioSource _audioSource;

    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(keyShoot) && this.gameObject.GetComponent<PlayerInput>().get_charge()==1 
            && !GetComponent<PlayerInput>().getBeingHit())
        {
            _audioSource.Play();
            _animator.SetTrigger("CrabShoot");
            _animator.SetBool("Reloading", false);
            _ball = Instantiate(ballPrefab) as GameObject;
            _ball.transform.position = transform.TransformPoint(Vector3.forward * 10f);
            _ball.transform.rotation = transform.rotation;
            this.gameObject.GetComponent<PlayerInput>().set_charge(0);
        }
    }
}
