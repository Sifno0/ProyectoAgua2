using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController controller;

    public float fSpeed = 12f;
    public float fGravity = -9.81f;
    public float fJumpHeight = 3f;
    public float fGroundDistance = 0.4f;

    public Transform groundCheck;
    public LayerMask groundMask;

    Vector3 velocity;
    public bool bIsGrounded;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bIsGrounded = Physics.CheckSphere(groundCheck.position, fGroundDistance, groundMask);

        if (bIsGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * fSpeed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && bIsGrounded)
        {
            velocity.y = Mathf.Sqrt(fJumpHeight * -2f * fGravity);
        }

        velocity.y += fGravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

    }
}
