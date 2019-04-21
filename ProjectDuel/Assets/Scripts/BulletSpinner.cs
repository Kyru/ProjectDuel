using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpinner : MonoBehaviour
{
    [SerializeField] private GameObject shootBlue;
    [SerializeField] private GameObject shootYellow;
    [SerializeField] private GameObject acidBullet;
    [SerializeField] private AudioClip spinnerSound;
    [SerializeField] private AudioSource spinnerSource;

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
        spinnerSource.PlayOneShot(spinnerSound);
        isTriggered = true;
        Destroy(this.gameObject);
        Messenger<int,int>.Broadcast(GameEvent.ROW_COL_OC,row,col);
    }
    public void OnTriggerExit(Collider other)
    {
        isTriggered = false;
    }
}
