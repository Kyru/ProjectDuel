using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
	[SerializeField] private int health;
    // id must be BLUE or YELLOW
    [SerializeField] private string id;
    [SerializeField] private GameObject enemy_scrub;
    [SerializeField] private float shieldPUTime = 10f;

    private bool haveShield;
    private float shieldPULastTime = 10f;
    private float timeFlickingPU;
    private Animator _animator;

    private void Start()
    {
        haveShield = false;
        shieldPULastTime = float.MaxValue;
        timeFlickingPU = GetComponent<PlayerInput>().getTimeFlickingPU();
        _animator = GetComponent<Animator>();

        Messenger<string>.AddListener(GameEvent.SHIELD_POWERUP_ADD, addShield);
        Messenger.AddListener(GameEvent.END, removeListeners);
    }

    public void Hurt(int damage) 
    {
        if (haveShield)
        {
            haveShield = false;
            Messenger<string>.Broadcast(GameEvent.SHIELD_POWERUP_REMOVE_INSTANT, gameObject.tag);
        }
    	else if(health > 0)
        {
            if (!gameObject.GetComponent<PlayerInput>().getBeingHit())
            {
                health -= damage;
                _animator.SetTrigger("CrabHit");
            }
        }
    }

    public int get_health(){return health;}
    public void set_health(int h){health=h;}

    private void addShield(string crab)
    {
        if (gameObject.CompareTag(crab))
        {
            haveShield = true;
            shieldPULastTime = Time.time;
        }
    }

    private void removeShield()
    {
        if (Time.time <= shieldPULastTime)
            haveShield = false;
    }

    public void Update()
    {
        if(health == 0)
        {
            if(id == "BLUE")
            {
                Messenger.Broadcast(GameEvent.BLUE_DIES);
            }
            else
            {
                Messenger.Broadcast(GameEvent.YELLOW_DIES);
            }
            Destroy(this.gameObject);
            Destroy(enemy_scrub.gameObject);
        }

        if (Time.time > shieldPULastTime + shieldPUTime)
        {
            Messenger<string, float>.Broadcast(GameEvent.SHIELD_POWERUP_REMOVE, gameObject.tag, timeFlickingPU);
            Invoke("removeShield", timeFlickingPU);
            shieldPULastTime = float.MaxValue;
        }
    }

    private void removeListeners()
    {
        Messenger<string>.RemoveListener(GameEvent.SHIELD_POWERUP_ADD, addShield);
        Messenger.RemoveListener(GameEvent.END, removeListeners);
    }

    public bool haveShieldPU() { return haveShield; }
}
