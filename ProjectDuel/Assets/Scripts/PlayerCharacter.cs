using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
	[SerializeField] private int health;
    // id must be BLUE or YELLOW
    [SerializeField] private string id;
    [SerializeField] private GameObject enemy_scrub;
    

    public void Hurt(int damage) 
    {
    	if(health != 0)health-= damage;
    }

    public int get_health(){return health;}

    public void Update()
    {
        if(health == 0)
        {
            if(id == "BLUE")
            {
                Messenger.Broadcast(GameEvent.BLUE_DIES);
            }
            else
            {
                Messenger.Broadcast(GameEvent.YELLOW_DIES);
            }
            Destroy(this.gameObject);
            Destroy(enemy_scrub.gameObject);
        }
    }
}
