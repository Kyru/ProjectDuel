using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public int damage = 1;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "BulletDestroyer")
        {
            Destroy(this.gameObject);
        }
        else if (other.gameObject.tag == "CrabBlue")
        {
            other.gameObject.GetComponent<PlayerCharacter>().Hurt(damage);
            if (!other.gameObject.GetComponent<PlayerCharacter>().haveShieldPU())
                other.gameObject.GetComponent<PlayerInput>().setBeingHit(true);
            Messenger<int>.Broadcast(GameEvent.BLUE_HURT, other.gameObject.GetComponent<PlayerCharacter>().get_health());
            this.gameObject.GetComponent<Animator>().SetTrigger("enemyHit");
            //this.gameObject.GetComponent<Collider>().enabled = false;
            //Destroy(this.gameObject);
        }
        else if (other.gameObject.tag == "CrabYellow")
        {
            other.gameObject.GetComponent<PlayerCharacter>().Hurt(damage);
            if (!other.gameObject.GetComponent<PlayerCharacter>().haveShieldPU())
                other.gameObject.GetComponent<PlayerInput>().setBeingHit(true);
            Messenger<int>.Broadcast(GameEvent.YELLOW_HURT, other.gameObject.GetComponent<PlayerCharacter>().get_health());
            this.gameObject.GetComponent<Animator>().SetTrigger("enemyHit");
            //this.gameObject.GetComponent<Collider>().enabled = false;
            //Destroy(this.gameObject);
        }
        else if (other.gameObject.tag == "ShootYellow" || other.gameObject.tag == "ShootBlue" || other.gameObject.tag == "AcidBullet")
        {
            Destroy(other.gameObject);
            this.gameObject.GetComponent<Animator>().SetTrigger("enemyHit");
            StartCoroutine("EnemyDeath");
        }
    }

    // Espera un poco antes de morir, para que salga la animación completa
    IEnumerator EnemyDeath()
    {
        yield return new WaitForSeconds(0.3f);
        Destroy(this.gameObject);
    }
}

