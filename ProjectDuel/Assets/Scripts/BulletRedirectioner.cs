using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletRedirectioner : MonoBehaviour
{
    private bool isTriggered = false;
    private int row,col;

    public void setRow(int r){row=r;}
    public void setCol(int c){col=c;}

    public int getRow(){return row;}
    public int getCol(){return col;}

    public void OnTriggerEnter(Collider other)
    {
        isTriggered = true;
        Destroy(this.gameObject);
        Messenger<int,int>.Broadcast(GameEvent.ROW_COL_OC,row,col);
    }
    public void OnTriggerExit(Collider other)
    {
        isTriggered = false;
    }
}
