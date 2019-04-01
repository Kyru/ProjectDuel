using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBall : MonoBehaviour
{
    public float speed = 10.0f;
    public int damage = 1;
    private bool _obstacleBullet;
    void Update()
    {
        transform.Translate(0, 0, speed * Time.deltaTime);

    }
    
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "BulletDestroyer")
        {
            Destroy(gameObject);
        }
        if (this.gameObject.tag == "ShootYellow" && other.gameObject.tag == "ShootBlue")
        {
            Destroy(this.gameObject);
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "CrabYellow" && this.gameObject.tag == "ShootBlue")
        {
            other.gameObject.GetComponent<PlayerCharacter>().Hurt(damage);
            Messenger<int>.Broadcast(GameEvent.YELLOW_HURT, other.gameObject.GetComponent<PlayerCharacter>().get_health());
            Destroy(this.gameObject);
        }
        if (other.gameObject.tag == "CrabBlue" && this.gameObject.tag == "ShootYellow")
        {
            other.gameObject.GetComponent<PlayerCharacter>().Hurt(damage);
            Messenger<int>.Broadcast(GameEvent.BLUE_HURT, other.gameObject.GetComponent<PlayerCharacter>().get_health());
            Destroy(this.gameObject);
        }
    }
}
