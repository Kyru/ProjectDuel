using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBall : MonoBehaviour
{

    public float speed = 10.0f;
    public int damage = 1;
    private bool _obstacleBullet;
    private Vector3 worldDirection;
    private Vector3 movement;
    private GameObject garbageWall;
    public Collider triggerCollider;
    public Collider noTriggerCollider;
    void Start()
    {
        worldDirection = transform.TransformDirection(Vector3.right);
        noTriggerCollider.enabled = false;
    }
    void Update()
    {
        movement = new Vector3(0, 0, speed * Time.deltaTime);
        transform.Translate(movement);
        //transform.Translate(worldDirection * speed * Time.deltaTime, Space.World);
    }

    public void OnTriggerEnter(Collider other)
    {
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
        else if (other.gameObject.tag == "CrabBlue" && this.gameObject.tag == "ShootYellow")
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
        else if (other.gameObject.tag == "GarbageWall")
        {
            triggerCollider.enabled = false;
            noTriggerCollider.enabled = true;
            //GetComponent<Collider>().isTrigger = false;
            //garbageWall = other.gameObject;
            //garbageWall.GetComponent<Collider>().isTrigger = false;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("entro en la colision");
        movement = Vector3.Reflect(movement, collision.contacts[0].normal);
        Debug.Log(movement);
        //movement.y = 0.0f;
        //movement.x = 0.0f;
        //Debug.Log(movement);
    }

    void OnCollisionExit(Collision collision)
    {
        triggerCollider.enabled = true;
        noTriggerCollider.enabled = false;
    }
}


/*
// TEST 1
Debug.Log("antes del raycast choco con la pared");
Vector3 thisVelocity = gameObject.GetComponent<Rigidbody>().velocity;
Ray ray = new Ray(transform.position, 3 * thisVelocity);
transform.GetComponent<Rigidbody>().position = transform.position - 3 * thisVelocity;
RaycastHit hit;
if (Physics.SphereCast(ray, 0.75f, out hit))
{
    Debug.Log("Se ha detectado la colisión con la pared inicio");
    Vector3 hitNormal = hit.normal;
    Vector3 hitVelocity = hit.transform.gameObject.GetComponent<Rigidbody>().velocity;
    gameObject.GetComponent<Rigidbody>().velocity = Vector3.Reflect(thisVelocity, hitNormal);
    Debug.Log("Se ha detectado la colisión con la pared final");
}

// TEST 2
else if (other.gameObject.tag == "GarbageWall")
        {
            //speed = -speed;
            Debug.Log("el trigger se detecta");
            //Vector3 thisVelocity = gameObject.GetComponent<Rigidbody>().velocity;
            Vector3 thisVelocity = new Vector3(0,0, speed * Time.deltaTime);
            Vector3 thisPosition = gameObject.GetComponent<Rigidbody>().position;
            Debug.Log(thisPosition);
            Debug.Log(thisVelocity);
            thisPosition = thisPosition - (3 * thisVelocity);
            //Debug.Log(thisPosition);
            RaycastHit hit;
            if (Physics.Raycast(thisPosition, thisVelocity, out hit))
            {
                Vector3 hitNormal = hit.normal;
                Quaternion fromRotation = gameObject.transform.rotation;
                Quaternion toRotation = Quaternion.FromToRotation(thisPosition, hit.normal);
                
                Quaternion newRotation = Quaternion.Euler(0, 136, 0);

                gameObject.GetComponent<Rigidbody>().velocity = Vector3.Reflect(thisVelocity, hitNormal);
                //gameObject.transform.rotation = newRotation;
                Debug.Log("Se ha detectado la colisión con la pared final");
            }
        }
*/

