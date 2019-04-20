using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] private GameObject ballPrefab;
    private GameObject _ball;
    private Animator _animator;
    private int extraBalls;
    [SerializeField] private KeyCode keyShoot;
    private AudioSource _audioSource;
    private bool is_sudden_death;
    private bool canShoot;
    private bool canAddExtraBall;
    private float coolDownExtraBall;
    private float addBallTime;

    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        extraBalls = 0;
        _animator = GetComponent<Animator>();

        is_sudden_death = false;
        canShoot = true;
        canAddExtraBall = true;
        coolDownExtraBall = 0.1f;

        Messenger.AddListener(GameEvent.SUDDEN_DEATH, sudden_death);

        Messenger<string>.AddListener(GameEvent.EXTRA_BALL_POWERUP_ADD, addExtraBall);
        Messenger.AddListener(GameEvent.END, removeListeners);
    }

    // Update is called once per frame
    void Update()
    {
        if (!is_sudden_death)
        {
            if (Input.GetKeyDown(keyShoot)
            && (this.gameObject.GetComponent<PlayerInput>().get_charge() == 1 || extraBalls > 0)
            && !GetComponent<PlayerInput>().getBeingHit())
            {
                if ((this.gameObject.GetComponent<PlayerInput>().get_charge() == 1 && extraBalls > 0)
                    || this.gameObject.GetComponent<PlayerInput>().get_charge() != 1)
                {
                    extraBalls--;
                    Messenger<string, int>.Broadcast(GameEvent.EXTRA_BALL_POWERUP_CHANGE, gameObject.tag, extraBalls);
                    Debug.Log("Acabo de gastar una bola extra, me quedan: " + extraBalls);
                }
                else
                {
                    this.gameObject.GetComponent<PlayerInput>().set_charge(0);
                }

                _animator.SetTrigger("CrabShoot");
                _animator.SetBool("Reloading", false);
                _ball = Instantiate(ballPrefab) as GameObject;
                _ball.transform.position = transform.TransformPoint(Vector3.forward * 10f);
                _ball.transform.rotation = transform.rotation;
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

        if (!canAddExtraBall && (Time.time > addBallTime + coolDownExtraBall))
        {
            canAddExtraBall = true;
        }

    }

    private void addExtraBall(string crab)
    {
        if (gameObject.CompareTag(crab) && canAddExtraBall)
        {
            extraBalls++;
            Messenger<string, int>.Broadcast(GameEvent.EXTRA_BALL_POWERUP_CHANGE, gameObject.tag, extraBalls);
            addBallTime = Time.time;
            canAddExtraBall = false;
        }
    }

    private void removeListeners()
    {
        Messenger<string>.RemoveListener(GameEvent.EXTRA_BALL_POWERUP_ADD, addExtraBall);
        Messenger.RemoveListener(GameEvent.END, removeListeners);
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
