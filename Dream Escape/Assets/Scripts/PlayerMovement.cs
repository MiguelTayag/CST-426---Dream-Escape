using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{


    public CharacterController characterController;
    public float movementSpeed = 10f;
    private float gravity = -20f;
    Vector3 velocity;

    GameObject groundCheck;
    private float groundDistance = 0.4f;
    public LayerMask groundMask;
    bool isGrounded;
    private float jumpHeight = 3f;

    private void Start()
    {
        groundCheck = GameObject.FindWithTag("groundcheck");
    }

    // Update is called once per frame
    void Update()
    {

        isGrounded = Physics.CheckSphere(groundCheck.transform.position, groundDistance, groundMask);
        Debug.Log(isGrounded);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float xDirection = Input.GetAxis("Horizontal");
        float zDirection = Input.GetAxis("Vertical");

        Vector3 move = transform.right * xDirection + transform.forward * zDirection;

        characterController.Move(move * movementSpeed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        characterController.Move(velocity * Time.deltaTime);
    }
}
