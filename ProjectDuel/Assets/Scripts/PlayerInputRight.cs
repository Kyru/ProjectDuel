using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))] // Gameobject tiene si o si el componente CharacterController. En caso de que no lo tenga lo crea
public class PlayerInputRight : MonoBehaviour
{
    public float speed = 6.0f;
    public float gravity = -9.8f;
    public static bool isCharging;
    public static bool isMoving;
    private CharacterController _charController;

    // Use this for initialization
    void Start()
    {
        isCharging = false;
        isMoving = true;
        _charController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Vertical2") != 0f && !isCharging && isMoving) // no se estan clicando las teclas de moverse
        {
            GetComponent<Animator>().SetBool("CargandoDisparo", false);
            float deltaX = Input.GetAxis("Vertical2") * speed; // Las teclas asociadas están en:

            Vector3 movement = new Vector3(deltaX, 0, 0);

            movement = Vector3.ClampMagnitude(movement, speed); // Movimiento no será más largo que la velocidad
            movement.y = gravity;
            movement = transform.TransformDirection(movement); // convierte desde el sistema local al global

            // Move espera movimiento en sistema de coordenadas global.
            // El TransformDirection nos hace esta conversión.
            _charController.Move(movement * Time.deltaTime); // no movemos el transform para que se calculen las colisiones
        }
        else
        {
            if (!Input.GetKeyDown(KeyCode.LeftArrow) && !isCharging)
            {
                StartCoroutine("chargingBullet");
            }


        }

    }

    // método de cargar bala (estarás parado 3 segundos)
    IEnumerator chargingBullet()
    {
        yield return StartCoroutine("changeDirection");
        if (!isMoving)
        {
            isCharging = true;
            GetComponent<Animator>().SetBool("CargandoDisparo", true);
        }
        yield return new WaitForSeconds(3);
        isCharging = false;
        isMoving = true;
    }

    // método que comprueba que has estado medio segundo parado para que entre en el método de cargar
    IEnumerator changeDirection()
    {
        yield return new WaitForSeconds(0.5f);
        if (Input.GetAxis("Vertical2") == 0f && !Input.GetKeyDown(KeyCode.LeftArrow))
        {
            isMoving = false;
        }
    }
}
