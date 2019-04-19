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
            bool haveShield = other.gameObject.GetComponent<PlayerCharacter>().haveShieldPU();
            other.gameObject.GetComponent<PlayerCharacter>().Hurt(damage);
            if (!haveShield)
            {
                other.gameObject.GetComponent<PlayerInput>().setBeingHit(true);
                Debug.Log("Me queda de vida: " + other.gameObject.GetComponent<PlayerCharacter>().get_health());
                Messenger<int>.Broadcast(GameEvent.BLUE_HURT, other.gameObject.GetComponent<PlayerCharacter>().get_health());
            }
            this.gameObject.GetComponent<Animator>().SetTrigger("enemyHit");
            //this.gameObject.GetComponent<Collider>().enabled = false;
            //Destroy(this.gameObject);
        }
        else if (other.gameObject.tag == "CrabYellow")
        {
            bool haveShield = other.gameObject.GetComponent<PlayerCharacter>().haveShieldPU();
            other.gameObject.GetComponent<PlayerCharacter>().Hurt(damage);
            if (!haveShield)
            {
                other.gameObject.GetComponent<PlayerInput>().setBeingHit(true);
                Debug.Log("Me queda de vida: " + other.gameObject.GetComponent<PlayerCharacter>().get_health());
                Messenger<int>.Broadcast(GameEvent.YELLOW_HURT, other.gameObject.GetComponent<PlayerCharacter>().get_health());
            }
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

