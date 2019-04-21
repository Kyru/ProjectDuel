using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBall : MonoBehaviour
{

    public float speed = 10.0f;
    public int damage = 1;
    private bool _obstacleBullet;
    public Vector3 worldDirection;
    public Vector3 movement;
    private bool can_hit = false;

    void Start()
    {
        //movement = Vector3.Normalize(Vector3.left + Vector3.forward);
        worldDirection = transform.TransformDirection(Vector3.forward);
    }

    void Update()
    {
        transform.Translate(worldDirection * speed * Time.deltaTime, Space.World);
    }

    public void OnTriggerEnter(Collider other)
    {
        bool haveShield;
        if (other.gameObject.tag == "BulletSpinner")
        {
            can_hit=true;
        }

        if (other.gameObject.tag == "BulletDestroyer" || other.gameObject.tag == "BackChest")
        {
            Destroy(gameObject);
        }

        switch (this.gameObject.tag)
        {
            case "ShootYellow":
                switch(other.gameObject.tag)
                {
                    case "ShootBlue":
                        Destroy(this.gameObject);
                        Destroy(other.gameObject);
                        break;
                    case "AcidBullet":
                        Destroy(this.gameObject);
                        Destroy(other.gameObject);
                        break;
                    case "CrabYellow":
                        if (can_hit)
                        {
                            haveShield = other.gameObject.GetComponent<PlayerCharacter>().haveShieldPU();
                            other.gameObject.GetComponent<PlayerCharacter>().Hurt(damage);
                            if (!haveShield)
                            {
                                other.gameObject.GetComponent<PlayerInput>().setBeingHit(true);
                                Messenger<int>.Broadcast(GameEvent.YELLOW_HURT, other.gameObject.GetComponent<PlayerCharacter>().get_health());
                            }
                            Destroy(this.gameObject);
                        }
                        break;
                    case "CrabBlue":
                        haveShield = other.gameObject.GetComponent<PlayerCharacter>().haveShieldPU();
                        other.gameObject.GetComponent<PlayerCharacter>().Hurt(damage);
                        if (!haveShield)
                        {
                            other.gameObject.GetComponent<PlayerInput>().setBeingHit(true);
                            Messenger<int>.Broadcast(GameEvent.BLUE_HURT, other.gameObject.GetComponent<PlayerCharacter>().get_health());
                        }
                        Destroy(this.gameObject);
                        break;
                    case "SpeedPowerUp":
                        Messenger<string>.Broadcast(GameEvent.SPEED_POWERUP_ADD, CrabType.CRAB_YELLOW);
                        break;
                    case "ExtraBallPowerUp":
                        Debug.Log("One extra ball for blue");
                        Messenger<string>.Broadcast(GameEvent.EXTRA_BALL_POWERUP_ADD, CrabType.CRAB_YELLOW);
                        break;
                    case "ReloadPowerUp":
                        Messenger<string>.Broadcast(GameEvent.RELOAD_POWERUP_ADD, CrabType.CRAB_YELLOW);
                        break;
                    case "ShieldPowerUp":
                        Messenger<string>.Broadcast(GameEvent.SHIELD_POWERUP_ADD, CrabType.CRAB_YELLOW);
                        break;
                }
                break;
            case "ShootBlue":
                switch (other.gameObject.tag)
                { 
                    case "AcidBullet":
                        Destroy(this.gameObject);
                        Destroy(other.gameObject);
                        break;
                    case "CrabBlue":
                        if (can_hit)
                        {
                            haveShield = other.gameObject.GetComponent<PlayerCharacter>().haveShieldPU();
                            other.gameObject.GetComponent<PlayerCharacter>().Hurt(damage);
                            if (!haveShield)
                            {
                                other.gameObject.GetComponent<PlayerInput>().setBeingHit(true);
                                Messenger<int>.Broadcast(GameEvent.BLUE_HURT, other.gameObject.GetComponent<PlayerCharacter>().get_health());
                            }
                            Destroy(this.gameObject);
                        }
                        break;
                    case "CrabYellow":
                        haveShield = other.gameObject.GetComponent<PlayerCharacter>().haveShieldPU();
                        other.gameObject.GetComponent<PlayerCharacter>().Hurt(damage);
                        if (!haveShield)
                        {
                            other.gameObject.GetComponent<PlayerInput>().setBeingHit(true);
                            Messenger<int>.Broadcast(GameEvent.YELLOW_HURT, other.gameObject.GetComponent<PlayerCharacter>().get_health());
                        }
                        Destroy(this.gameObject);
                        break;
                    case "SpeedPowerUp":
                        Messenger<string>.Broadcast(GameEvent.SPEED_POWERUP_ADD, CrabType.CRAB_BLUE);
                        break;
                    case "ExtraBallPowerUp":
                        Debug.Log("On tigger enter ShootBall");
                        Messenger<string>.Broadcast(GameEvent.EXTRA_BALL_POWERUP_ADD, CrabType.CRAB_BLUE);
                        break;
                    case "ReloadPowerUp":
                        Messenger<string>.Broadcast(GameEvent.RELOAD_POWERUP_ADD, CrabType.CRAB_BLUE);
                        break;
                    case "ShieldPowerUp":
                        Messenger<string>.Broadcast(GameEvent.SHIELD_POWERUP_ADD, CrabType.CRAB_BLUE);
                        break;
                }
                break;
            case "AcidBullet":
                switch(other.gameObject.tag)
                {
                    case "CrabBlue":
                        haveShield = other.gameObject.GetComponent<PlayerCharacter>().haveShieldPU();
                        other.gameObject.GetComponent<PlayerCharacter>().Hurt(damage);
                        if (!haveShield)
                        {
                            other.gameObject.GetComponent<PlayerInput>().setBeingHit(true);
                            Messenger<int>.Broadcast(GameEvent.BLUE_HURT, other.gameObject.GetComponent<PlayerCharacter>().get_health());
                        }
                        Destroy(this.gameObject);
                        break;
                    case "CrabYellow":
                        haveShield = other.gameObject.GetComponent<PlayerCharacter>().haveShieldPU();
                        other.gameObject.GetComponent<PlayerCharacter>().Hurt(damage);
                        if (!haveShield)
                        {
                            other.gameObject.GetComponent<PlayerInput>().setBeingHit(true);
                            Messenger<int>.Broadcast(GameEvent.YELLOW_HURT, other.gameObject.GetComponent<PlayerCharacter>().get_health());
                        }
                        Destroy(this.gameObject);
                        break;
                }
                break;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "wall":
                worldDirection = Vector3.Reflect(worldDirection, collision.contacts[0].normal);
                worldDirection.y = 0.0f;
                break;
            case "BulletRedirectioner":
                worldDirection = Vector3.Reflect(worldDirection, collision.contacts[0].normal);
                worldDirection.y = 0.0f;
                can_hit = true;
                //Destroy(collision.gameObject);
                break;
            case "BulletDestroyer":
                Destroy(this.gameObject);
                break;
        }
    }
}
