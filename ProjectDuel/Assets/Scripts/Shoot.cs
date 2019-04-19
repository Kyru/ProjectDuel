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
    private bool is_sudden_death;
    private bool canShoot;

    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _animator = GetComponent<Animator>();

        is_sudden_death = false;
        canShoot = true;

        Messenger.AddListener(GameEvent.SUDDEN_DEATH, sudden_death);
    }

    // Update is called once per frame
    void Update()
    {
        if (!is_sudden_death)
        {
            if (Input.GetKeyDown(keyShoot) && this.gameObject.GetComponent<PlayerInput>().get_charge() == 1
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
        else
        {
            if (Input.GetKeyDown(keyShoot) && !GetComponent<PlayerInput>().getBeingHit())
            {
                if (canShoot)
                {
                    _audioSource.Play();
                    _animator.SetTrigger("CrabShoot");
                    _animator.SetBool("Reloading", false);
                    _ball = Instantiate(ballPrefab) as GameObject;
                    _ball.transform.position = transform.TransformPoint(Vector3.forward * 10f);
                    _ball.transform.rotation = transform.rotation;
                    StartCoroutine("limit_shoot");
                }
            }
        }

    }

    IEnumerator limit_shoot()
    {
        canShoot = false;
        yield return new WaitForSeconds(0.5f);
        canShoot = true;
    }

    void sudden_death()
    {
        is_sudden_death = true;
    }
}
