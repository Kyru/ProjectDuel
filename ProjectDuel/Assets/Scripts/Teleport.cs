using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public GameObject chest;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "ShootBlue" && this.gameObject.tag == "chestLeft")
        {
            other.transform.position = chest.transform.position + Vector3.left * 0.4f;
        }
        if (other.gameObject.tag == "ShootYellow" && this.gameObject.tag == "chestLeft")
        {
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "ShootYellow" && this.gameObject.tag == "chestRight")
        {
            other.transform.position = chest.transform.position + Vector3.right * 0.4f;
        }
        if (other.gameObject.tag == "ShootBlue" && this.gameObject.tag == "chestRight")
        {
            Destroy(other.gameObject);
        }
    }
}
