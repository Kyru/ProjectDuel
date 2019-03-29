using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
	[SerializeField] private int health;    

    public void Hurt(int damage) 
    {
    	if(health != 0)health-= damage;
    }

    public int get_health(){return health;}
}
