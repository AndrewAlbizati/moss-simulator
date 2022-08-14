using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject lawnMower;
    public GameObject gameControllerObject;

    public float walkingSpeed = 12f;

    private float speed;

    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    public AudioClip walk;
    public AudioClip mow;


    Vector3 velocity;
    bool isGrounded;
    bool isRiding;

    private GameController gameController;
    private CharacterController controller;

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
        isRiding = false;

        controller = gameObject.GetComponent<CharacterController>();
        gameController = gameControllerObject.GetComponent<GameController>();
        speed = walkingSpeed;

        controller.enabled = true;
        lawnMower.GetComponent<CharacterController>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameController.IsPaused())
        {
            gameObject.GetComponent<AudioSource>().enabled = false;
            return;
        }

        if (!isRiding)
        {
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

            if (Input.GetKey(KeyCode.LeftShift))
            {
                //gameObject.GetComponent<AudioSource>().pitch = 0.5f;
                gameObject.GetComponent<AudioSource>().pitch = 1.2f;
                speed = walkingSpeed * 1.5f;
            }
            else
            {
                //gameObject.GetComponent<AudioSource>().pitch = 0.2f;
                gameObject.GetComponent<AudioSource>().pitch = 1f;
                speed = walkingSpeed;
            }

            // Play walking sound
            if (isGrounded && (move.x != 0 || move.z != 0))
            {
                gameObject.GetComponent<AudioSource>().enabled = true;
            }
            else
            {
                gameObject.GetComponent<AudioSource>().enabled = false;
            }

            controller.Move(move * speed * Time.deltaTime);

            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }

            

            velocity.y += gravity * Time.deltaTime;

            controller.Move(velocity * Time.deltaTime);
        }
        else
        {
            gameObject.GetComponent<AudioSource>().enabled = true;
            // Create small invisible sphere
            // If it collides with anything in the mask, isGrounded is set to true
            isGrounded = Physics.CheckSphere(lawnMower.transform.GetChild(25).position, groundDistance, groundMask);

            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }

            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 move = transform.right * x + transform.forward * z;

            lawnMower.GetComponent<CharacterController>().Move(move * speed * Time.deltaTime);

            velocity.y += gravity * Time.deltaTime;

            lawnMower.GetComponent<CharacterController>().Move(velocity * Time.deltaTime);
        }
    }

    public void SetRotation(float x, float y, float z)
    {
        gameObject.transform.eulerAngles = new Vector3(x, y ,z);
    }

    public bool IsRiding()
    {
        return isRiding;
    }

    public void ToggleRiding()
    {
        isRiding = !isRiding;

        gameObject.GetComponent<AudioSource>().clip = isRiding ? mow : walk;
        gameObject.GetComponent<AudioSource>().enabled = true;
        gameObject.GetComponent<AudioSource>().Play();
        gameObject.GetComponent<AudioSource>().enabled = isRiding;

        controller.enabled = !isRiding;
        lawnMower.GetComponent<CharacterController>().enabled = isRiding;

        
        
    }
}
