using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDivider : MonoBehaviour
{
    [SerializeField] private GameObject shootBlue;
    [SerializeField] private GameObject shootYellow;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "ShootYellow")
        {
            Destroy(other.gameObject);

            Instantiate(shootYellow, this.gameObject.transform.position + (Vector3.right * 0.5f),
                this.gameObject.transform.rotation, this.gameObject.transform);
            Instantiate(shootYellow, this.gameObject.transform.position + (Vector3.right * 0.5f),
                this.gameObject.transform.parent.transform.rotation, this.gameObject.transform);
        }
        if (other.gameObject.tag == "ShootBlue")
        {
            Destroy(other.gameObject);
            Instantiate(shootBlue, this.gameObject.transform.position + (Vector3.right * -1.5f),
                this.gameObject.transform.rotation, this.gameObject.transform);
            //Instantiate(shootBlue, this.gameObject.transform.position + (Vector3.right * 0.5f),
             //   this.gameObject.transform.parent.transform.rotation, this.gameObject.transform);
        }
    }
}
