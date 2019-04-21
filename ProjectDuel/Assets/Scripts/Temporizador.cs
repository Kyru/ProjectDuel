using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Temporizador : MonoBehaviour
{
    [SerializeField] private GameObject blueCrab;
    [SerializeField] private GameObject yellowCrab;

    private bool blockUpdate = false;
    private int blueHealth;
    private int yellowHealth;
    private bool fiveSecondsLeft;

    public float tiempo;

    void Start()
    {
        blueHealth = blueCrab.GetComponent<PlayerCharacter>().get_health();
        yellowHealth = yellowCrab.GetComponent<PlayerCharacter>().get_health();
        fiveSecondsLeft = false;
    }

    void LateUpdate()
    {
        
        if (blueCrab != null && yellowCrab != null && !blockUpdate)
        {
            blueHealth = blueCrab.GetComponent<PlayerCharacter>().get_health();
            yellowHealth = yellowCrab.GetComponent<PlayerCharacter>().get_health();

            tiempo -= Time.deltaTime;

            Messenger<int>.Broadcast(GameEvent.TIME, Mathf.CeilToInt(tiempo));

            if (!fiveSecondsLeft && tiempo <= 5)
            {
                Messenger.Broadcast(GameEvent.FIVE_SECONDS_LEFT);
            }

            if (tiempo <= 0)
            {
                if (blueHealth > yellowHealth)
                {
                    yellowCrab.GetComponent<PlayerCharacter>().set_health(0);
                }
                else if (yellowHealth > blueHealth)
                {
                    blueCrab.GetComponent<PlayerCharacter>().set_health(0);
                }
                else if (yellowHealth == blueHealth)
                {
                    for (int i = 1; i < yellowHealth; i++)
                    {
                        Messenger<int>.Broadcast(GameEvent.BLUE_HURT, blueHealth - i);
                        Messenger<int>.Broadcast(GameEvent.YELLOW_HURT, yellowHealth - i);
                    }

                    yellowCrab.GetComponent<PlayerCharacter>().set_health(1);
                    blueCrab.GetComponent<PlayerCharacter>().set_health(1);

                    Messenger.Broadcast(GameEvent.SUDDEN_DEATH);
                }
            }
        }
    }
    /*private void noUpdate()
    {
        blockUpdate = true;
        Messenger.RemoveListener(GameEvent.END, noUpdate);
    }*/
}
