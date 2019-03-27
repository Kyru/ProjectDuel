using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))] // Gameobject tiene si o si el componente CharacterController. En caso de que no lo tenga lo crea
public class PlayerInput : MonoBehaviour
{
    public float speed = 6.0f;
    public float gravity = -9.8f;
    private CharacterController _charController;

    // Use this for initialization
    void Start()
    {
        _charController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float deltaX = Input.GetAxis("Horizontal") * speed; // Las teclas asociadas están en:

        Vector3 movement = new Vector3(deltaX, 0, 0);

        movement = Vector3.ClampMagnitude(movement, speed); // Movimiento no será más largo que la velocidad
        movement.y = gravity;
        movement = transform.TransformDirection(movement); // convierte desde el sistema local al global

        // Move espera movimiento en sistema de coordenadas global.
        // El TransformDirection nos hace esta conversión.
        _charController.Move(movement * Time.deltaTime); // no movemos el transform para que se calculen las colisiones
    }
}
