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

    // Start is called before the first frame update
    void Start()
    {
        extraBalls = 0;
        _animator = GetComponent<Animator>();

        Messenger<string>.AddListener(GameEvent.EXTRA_BALL_POWERUP_ADD, addExtraBall);
        Messenger.AddListener(GameEvent.END, removeListeners);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(keyShoot)
            && (this.gameObject.GetComponent<PlayerInput>().get_charge()==1 || extraBalls > 0)
            && !GetComponent<PlayerInput>().getBeingHit())
        {
            if ((this.gameObject.GetComponent<PlayerInput>().get_charge() == 1 && extraBalls > 0)
                || this.gameObject.GetComponent<PlayerInput>().get_charge() != 1)
            {    
                extraBalls--;
                Debug.Log("Acabo de gastar una bola extra, me quedan: " + extraBalls);
            }

            _animator.SetTrigger("CrabShoot");
            _animator.SetBool("Reloading", false);
            _ball = Instantiate(ballPrefab) as GameObject;
            _ball.transform.position = transform.TransformPoint(Vector3.forward * 10f);
            _ball.transform.rotation = transform.rotation;
            this.gameObject.GetComponent<PlayerInput>().set_charge(0);
        }
    }

    private void addExtraBall(string crab)
    {
        if (gameObject.CompareTag(crab))
        {
            extraBalls++;
        }
    }

    private void removeListeners()
    {
        Messenger<string>.RemoveListener(GameEvent.EXTRA_BALL_POWERUP_ADD, addExtraBall);
        Messenger.RemoveListener(GameEvent.END, removeListeners);
    }
}
