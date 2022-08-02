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

    private void OnEnable()
    {
        if (PlayerPrefs.HasKey("posx") && PlayerPrefs.HasKey("posy") && PlayerPrefs.HasKey("posz") && PlayerPrefs.HasKey("rotx") && PlayerPrefs.HasKey("roty") && PlayerPrefs.HasKey("rotz"))
        {
            gameObject.transform.position = new Vector3(PlayerPrefs.GetFloat("posx"), PlayerPrefs.GetFloat("posy"), PlayerPrefs.GetFloat("posz"));
            gameObject.transform.eulerAngles = new Vector3(PlayerPrefs.GetFloat("rotx"), PlayerPrefs.GetFloat("roty"), PlayerPrefs.GetFloat("rotz"));
        }
    }


    private void OnDisable()
    {
        PlayerPrefs.SetFloat("posx", transform.position.x);
        PlayerPrefs.SetFloat("posy", transform.position.y);
        PlayerPrefs.SetFloat("posz", transform.position.z);

        PlayerPrefs.SetFloat("rotx", transform.eulerAngles.x);
        PlayerPrefs.SetFloat("roty", transform.eulerAngles.y);
        PlayerPrefs.SetFloat("rotz", transform.eulerAngles.z);
    }

    private void Start()
    {
        speed = walkingSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameController.GetComponent<GameController>().IsPaused())
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

    public void SetPos(float x, float y, float z)
    {
        
    }

    public void SetRotation(float x, float y, float z)
    {
        gameObject.transform.eulerAngles = new Vector3(x, y ,z);
    }
}
