using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public GameObject gameController;

    public float walkingSpeed = 12f;

    private float speed;

    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;

    private void Start()
    {
        speed = walkingSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameController.GetComponent<GameController>().isPaused)
        {
            return;
        }

        // Create small invisible sphere
        // If it collides with anything in the mask, isGrounded is set to true
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = walkingSpeed * 1.5f;
        }
        else
        {
            speed = walkingSpeed;
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
}
