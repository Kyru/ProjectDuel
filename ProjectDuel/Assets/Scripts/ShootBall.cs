﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBall : MonoBehaviour
{
    public float speed = 10.0f;
    public int damage = 1;
    void Update()
    {
        transform.Translate(0, 0, speed * Time.deltaTime);
    }
    /*
    private void OnTriggerEnter(Collider other)
    {
        PlayerCharacter player = other.GetComponent<PlayerCharacter>();
        if (player != null)
        {
            player.Hurt(damage);
        }
        Destroy(this.gameObject);
    }
    */

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "BulletDestroyer"){
            Destroy(gameObject);
        }
    }
}
