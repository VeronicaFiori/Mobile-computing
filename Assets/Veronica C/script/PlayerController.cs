using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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
    public float surfaceCheckRadius = 0.1f;
    public Vector3 surfaceCheckOffset;
    public LayerMask surfaceLayer;
    bool onSurface;
    
    [SerializeField] float fallingSpeed;
    [SerializeField] Vector3 moveDir;

    public void Update()
    {
        if (onSurface)
        {
            fallingSpeed = -0.5f;
        }
        else
        {
            fallingSpeed += Physics.gravity.y * Time.deltaTime;
        }

        var velocity = moveDir * movementSpeed;
        velocity.y = fallingSpeed;

        PlayerMovement();
        SurfaceCheck();
        Debug.Log("Player on Surface" + onSurface);
    }

    void PlayerMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        float movementAmount =Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));

        var movementInput = (new Vector3(horizontal, 0, vertical)).normalized;

        var movementDirection = MCC.flatRotation * movementInput;

       CC.Move(movementDirection * movementSpeed * Time.deltaTime);

        if (movementAmount > 0)
        {
            requireRotation = Quaternion.LookRotation(movementDirection);
        }

       movementDirection = moveDir;

        transform.rotation = Quaternion.RotateTowards(transform.rotation, requireRotation,rotSpeed *Time.deltaTime);
        animator.SetFloat("movementValue", movementAmount,0.2f, Time.deltaTime);
    }

    void SurfaceCheck()
    {
        onSurface = Physics.CheckSphere(transform.TransformPoint(surfaceCheckOffset), surfaceCheckRadius, surfaceLayer);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.TransformPoint(surfaceCheckOffset), surfaceCheckRadius);
    }
}
