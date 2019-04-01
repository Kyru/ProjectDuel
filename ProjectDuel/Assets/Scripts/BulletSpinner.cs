using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpinner : MonoBehaviour
{
    [SerializeField] private GameObject shootBlue;
    [SerializeField] private GameObject shootYellow;
    [SerializeField] private GameObject acidBullet;
    private bool isTriggered = false;
    public void OnTriggerEnter(Collider other)
    {
        if (isTriggered == false)
        {
            if (other.gameObject.tag == "ShootYellow")
            {
                var randomRotation = Quaternion.Euler(0, Random.Range(0, 360), 0);

                Destroy(other.gameObject);
                Instantiate(shootYellow, this.transform.position,
                    randomRotation, this.gameObject.transform.parent.transform);
            }
            if (other.gameObject.tag == "ShootBlue")
            {
                var randomRotation = Quaternion.Euler(0, Random.Range(0, 360), 0);

                Destroy(other.gameObject);
                Instantiate(shootBlue, this.transform.position,
                    randomRotation, this.gameObject.transform.parent.transform);
            }
            if (other.gameObject.tag == "AcidBullet")
            {
                var randomRotation = Quaternion.Euler(0, Random.Range(0, 360), 0);

                Destroy(other.gameObject);
                Instantiate(acidBullet, this.transform.position,
                    randomRotation, this.gameObject.transform.parent.transform);
            }
        }
        isTriggered = true;
    }
    public void OnTriggerExit(Collider other)
    {
        isTriggered = false;
    }
}
