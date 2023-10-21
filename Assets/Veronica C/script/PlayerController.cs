using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Movement")]
    public float movementSpeed = 5f;
    public float rotSpeed = 450f;
    public cameraController MCC;
    Quaternion requireRotation;

    [Header("Player Animator")]
    public Animator animator;


    [Header("Player Animator")]
    public CharacterController CC;

    public void Update()
    {
        PlayerMovement();
    }

    void PlayerMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        float movementAmount =Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));

        var movementInput = (new Vector3(horizontal, 0, vertical)).normalized;

        var movementDirection = MCC.flatRotation * movementInput;

        if (movementAmount > 0)
        {
             CC.Move(movementDirection * movementSpeed * Time.deltaTime);
            requireRotation = Quaternion.LookRotation(movementDirection);
        }

        transform.rotation = Quaternion.RotateTowards(transform.rotation, requireRotation,rotSpeed *Time.deltaTime);
        animator.SetFloat("movementValue", movementAmount,0.2f, Time.deltaTime);
    }

}
