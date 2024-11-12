using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f; // Velocidade de movimento ajust�vel
    [SerializeField] Rigidbody rb; // Rigidbody para mover o objeto
    Vector3 movement; // Armazenar o movimento como um vetor
    [SerializeField] GameObject characterModel;
    [SerializeField] Animator playerAnimator;
    [SerializeField] FixedJoystick joystick;
    void Update()
    {
        // Obter dire��o do joystick
        Vector2 joystickDirection = joystick.Direction;

        // Cria um vetor de movimento baseado na dire��o do joystick
        movement = new Vector3(joystickDirection.x, 0f, joystickDirection.y).normalized;

        if (movement != Vector3.zero)
        {
            playerAnimator.SetBool("Moving", true);
            float rotateSpeed = 10f;
            transform.forward = Vector3.Slerp(transform.forward, movement, Time.deltaTime * rotateSpeed);
        }
        else
        {
            playerAnimator.SetBool("Moving", false);
        }
    }
    void FixedUpdate()
    {
        // Mover o Rigidbody baseado no vetor de movimento
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    // Fun��o para executar o punch
    public void PerformPunch()
    {
        playerAnimator.SetBool("Punch", true); // Inicia a anima��o
        Invoke("StopPunch", 0.5f); // Chama StopPunch ap�s 0.5 segundos (ajuste conforme necess�rio)
        playerAnimator.SetLayerWeight(1, 1);
    }

    // Fun��o para parar a anima��o de punch
    private void StopPunch()
    {
        playerAnimator.SetBool("Punch", false);
        playerAnimator.SetLayerWeight(1, 0);
    }

}