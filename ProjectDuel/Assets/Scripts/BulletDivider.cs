using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDivider : MonoBehaviour
{
    [SerializeField] private GameObject shootBlue;
    [SerializeField] private GameObject shootYellow;
    [SerializeField] private GameObject acidBullet;
    [SerializeField] private AudioClip barrelSound;
    [SerializeField] private AudioSource barrelSource;
    private bool isTriggered = false;
    private int row,col;

    public void setRow(int r){row=r;}
    public void setCol(int c){col=c;}

    public int getRow(){return row;}
    public int getCol(){return col;}


    public void OnTriggerEnter(Collider other)
    {
        if (isTriggered == false)
        {

            if (other.gameObject.tag == "ShootYellow")
            {
                Destroy(other.gameObject);
                Instantiate(acidBullet, this.transform.position + Vector3.right * 0.4f,
                    this.transform.rotation, this.gameObject.transform.parent.transform);
                Instantiate(acidBullet, this.gameObject.transform.position + (Vector3.right * 0.5f),
                                    this.gameObject.transform.parent.transform.rotation, this.gameObject.transform.parent.transform);
                barrelSource.PlayOneShot(barrelSound);

                // Este codigo se utiliza, si se quiere usar el shootYellow prefab
                /*
                other.transform.position = this.transform.position + Vector3.right * 0.4f;
                other.transform.rotation = this.transform.rotation;
                Instantiate(shootYellow, this.gameObject.transform.position + (Vector3.right * 0.5f),
                    this.gameObject.transform.parent.transform.rotation, this.gameObject.transform.parent.transform);
                */
            }
            if (other.gameObject.tag == "ShootBlue")
            {
                Destroy(other.gameObject);
                Instantiate(acidBullet, this.transform.position + Vector3.left * 0.4f,
                    Quaternion.Inverse(this.transform.rotation), this.gameObject.transform.parent.transform);
                Instantiate(acidBullet, this.gameObject.transform.position + (Vector3.left * 0.5f),
                    Quaternion.Inverse(this.gameObject.transform.parent.transform.rotation), this.gameObject.transform.parent.transform);
                barrelSource.PlayOneShot(barrelSound);

                // Este codigo se utiliza, si se quiere usar el shootBlue prefab
                /*
                other.transform.position = this.transform.position + Vector3.left * 0.4f;
                other.transform.rotation = Quaternion.Inverse(this.transform.rotation);
                Instantiate(shootBlue, this.gameObject.transform.position + (Vector3.left * 0.5f),
                   Quaternion.Inverse(this.gameObject.transform.parent.transform.rotation), this.gameObject.transform.parent.transform);
                */
            }
            if (other.gameObject.tag == "AcidBullet")
            {
                float random = Random.Range(0f, 1f);
                if (random > 0.5f)
                {
                    Destroy(other.gameObject);
                    Instantiate(acidBullet, this.transform.position + Vector3.right * 0.4f,
                        this.transform.rotation, this.gameObject.transform.parent.transform);
                    Instantiate(acidBullet, this.gameObject.transform.position + (Vector3.right * 0.5f),
                        this.gameObject.transform.parent.transform.rotation, this.gameObject.transform.parent.transform);
                }
                else
                {
                    Destroy(other.gameObject);
                    Instantiate(acidBullet, this.transform.position + Vector3.left * 0.4f,
                        Quaternion.Inverse(this.transform.rotation), this.gameObject.transform.parent.transform);
                    Instantiate(acidBullet, this.gameObject.transform.position + (Vector3.left * 0.5f),
                        Quaternion.Inverse(this.gameObject.transform.parent.transform.rotation), this.gameObject.transform.parent.transform);
                }
                barrelSource.PlayOneShot(barrelSound);
            }
            Messenger<int, int>.Broadcast(GameEvent.ROW_COL_OC, row, col);
        }
        isTriggered = true;
        Destroy(this.gameObject);
    }

    public void OnTriggerExit(Collider other)
    {
        isTriggered = false;
    }
}
