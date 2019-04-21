using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletRedirectioner : MonoBehaviour
{
    private bool isTriggered = false;
    private int row,col;
    [SerializeField] private AudioClip yellowPlantSound;
    [SerializeField] private AudioSource yellowPlantSource;


    public void setRow(int r){row=r;}
    public void setCol(int c){col=c;}

    public int getRow(){return row;}
    public int getCol(){return col;}

    void Update()
    {
        if(isTriggered && !yellowPlantSource.isPlaying)
        {
            Destroy(this.gameObject);
        }
    }

    public void OnCollisionEnter(Collision other)
    {
       
        yellowPlantSource.PlayOneShot(yellowPlantSound);
        isTriggered = true;
        Messenger<int,int>.Broadcast(GameEvent.ROW_COL_OC,row,col);
        GetComponent<CapsuleCollider>().enabled=false;
        Destroy(this.transform.GetChild(1).gameObject);
        
    }
    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        Destroy(this.transform.GetChild(1).gameObject);

    }
    public void OnTriggerExit(Collider other)
    {
        //isTriggered = false;
    }
}
