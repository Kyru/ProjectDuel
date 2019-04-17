using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBall : MonoBehaviour
{

    public float speed = 10.0f;
    public int damage = 1;
    private bool _obstacleBullet;
    private bool can_hit=false;
    void Update()
    {
        transform.Translate(0, 0, speed * Time.deltaTime);

    }

    public void OnTriggerEnter(Collider other)
    {   if(other.gameObject.tag == "BulletSpinner")
        {
            can_hit=true;
        }

        if (other.gameObject.tag == "BulletDestroyer")
        {
            Destroy(gameObject);
        }
        else if (this.gameObject.tag == "ShootYellow" && other.gameObject.tag == "ShootBlue")
        {
            Destroy(this.gameObject);
            Destroy(other.gameObject);
        }
        else if (this.gameObject.tag == "ShootYellow" && other.gameObject.tag == "AcidBullet")
        {
            Destroy(this.gameObject);
            Destroy(other.gameObject);
        }
        else if (this.gameObject.tag == "ShootBlue" && other.gameObject.tag == "AcidBullet")
        {
            Destroy(this.gameObject);
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "CrabYellow" && this.gameObject.tag == "ShootBlue")
        {
            other.gameObject.GetComponent<PlayerCharacter>().Hurt(damage);
            other.gameObject.GetComponent<Animator>().SetTrigger("CrabHit");
            other.gameObject.GetComponent<PlayerInput>().setBeingHit(true);
            Messenger<int>.Broadcast(GameEvent.YELLOW_HURT, other.gameObject.GetComponent<PlayerCharacter>().get_health());
            Destroy(this.gameObject);
        }
        else if (other.gameObject.tag == "CrabYellow" && this.gameObject.tag == "ShootYellow" && can_hit)
        {
            other.gameObject.GetComponent<PlayerCharacter>().Hurt(damage);
            other.gameObject.GetComponent<Animator>().SetTrigger("CrabHit");
            other.gameObject.GetComponent<PlayerInput>().setBeingHit(true);
            Messenger<int>.Broadcast(GameEvent.YELLOW_HURT, other.gameObject.GetComponent<PlayerCharacter>().get_health());
            Destroy(this.gameObject);
        }
        else if (other.gameObject.tag == "CrabBlue" && this.gameObject.tag == "ShootYellow")
        {
            other.gameObject.GetComponent<PlayerCharacter>().Hurt(damage);
            other.gameObject.GetComponent<Animator>().SetTrigger("CrabHit");
            other.gameObject.GetComponent<PlayerInput>().setBeingHit(true);
            Messenger<int>.Broadcast(GameEvent.BLUE_HURT, other.gameObject.GetComponent<PlayerCharacter>().get_health());
            Destroy(this.gameObject);
        }
        else if (other.gameObject.tag == "CrabBlue" && this.gameObject.tag == "ShootBlue" && can_hit)
        {
            other.gameObject.GetComponent<PlayerCharacter>().Hurt(damage);
            other.gameObject.GetComponent<Animator>().SetTrigger("CrabHit");
            other.gameObject.GetComponent<PlayerInput>().setBeingHit(true);
            Messenger<int>.Broadcast(GameEvent.BLUE_HURT, other.gameObject.GetComponent<PlayerCharacter>().get_health());
            Destroy(this.gameObject);
        }
        else if (other.gameObject.tag == "CrabBlue" && this.gameObject.tag == "AcidBullet")
        {
            other.gameObject.GetComponent<PlayerCharacter>().Hurt(damage);
            other.gameObject.GetComponent<Animator>().SetTrigger("CrabHit");
            other.gameObject.GetComponent<PlayerInput>().setBeingHit(true);
            Messenger<int>.Broadcast(GameEvent.BLUE_HURT, other.gameObject.GetComponent<PlayerCharacter>().get_health());
            Destroy(this.gameObject);
        }
        else if (other.gameObject.tag == "CrabYellow" && this.gameObject.tag == "AcidBullet")
        {
            other.gameObject.GetComponent<PlayerCharacter>().Hurt(damage);
            other.gameObject.GetComponent<Animator>().SetTrigger("CrabHit");
            other.gameObject.GetComponent<PlayerInput>().setBeingHit(true);
            Messenger<int>.Broadcast(GameEvent.YELLOW_HURT, other.gameObject.GetComponent<PlayerCharacter>().get_health());
            Destroy(this.gameObject);
        }
    }
}
