using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BarnDoors : MonoBehaviour
{
    public GameObject player;
    public GameObject keybindLabel;
    public GameObject gameControllerObject;

    private AudioSource audioSource;
    private GameController gameController;
    private GameObject leftDoor;
    private GameObject rightDoor;

    private string text;

    // Start is called before the first frame update
    void Start()
    {
        gameController = gameControllerObject.GetComponent<GameController>();
        audioSource = gameObject.GetComponent<AudioSource>();

        text = "Press " + gameController.actionKey.ToString() + " to open door";

        leftDoor = gameObject.transform.GetChild(0).gameObject;
        rightDoor = gameObject.transform.GetChild(1).gameObject;

        if (gameController.GetTaskIndex() < 9)
        {
            // Closed door positions
            leftDoor.transform.eulerAngles = new Vector3(0f, 180f, 0f);
            leftDoor.transform.localPosition = new Vector3(2.138f, 0f, 0.525f);

            rightDoor.transform.eulerAngles = new Vector3(0f, 180f, 0f);
            rightDoor.transform.localPosition = new Vector3(-2.138f, 0f, 0.525f);
        }
        else
        {
            // Open door positions
            leftDoor.transform.eulerAngles = new Vector3(0f, 300f, 0f);
            leftDoor.transform.localPosition = new Vector3(2.248f, 0f, 0.212f);

            rightDoor.transform.eulerAngles = new Vector3(0f, 60f, 0f);
            rightDoor.transform.localPosition = new Vector3(-2.248f, 0f, 0.212f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gameController.GetTaskIndex() == 8 && !gameController.IsPaused())
        {
            float playerX = player.transform.position.x;
            float playerZ = player.transform.position.z;
            float doorX = transform.GetChild(0).GetChild(1).position.x;
            float doorZ = transform.GetChild(0).GetChild(1).position.z;

            float distance = Mathf.Sqrt(Mathf.Pow(doorX - playerX, 2) + Mathf.Pow(doorZ - playerZ, 2));

            if (distance < 10)
            {
                TMP_Text mText = keybindLabel.GetComponent<TMP_Text>();
                mText.SetText(text);


                keybindLabel.SetActive(true);
                if (Input.GetKeyDown(gameController.actionKey))
                {
                    StartCoroutine(OpenDoors());
                    gameController.IncrementTaskIndex();
                }
                return;
            }
        }

        if (keybindLabel.GetComponent<TMP_Text>().text == text)
        {
            keybindLabel.SetActive(false);
        }
    }

    IEnumerator OpenDoors()
    {
        audioSource.Play();
        for (int i = 0; i < 120; i++)
        {
            leftDoor.transform.Rotate(new Vector3(0, 1, 0));
            leftDoor.transform.localPosition = new Vector3(leftDoor.transform.localPosition.x + 0.00091f, 0, leftDoor.transform.localPosition.z - 0.0026f);

            rightDoor.transform.Rotate(new Vector3(0, -1, 0));
            rightDoor.transform.localPosition = new Vector3(rightDoor.transform.localPosition.x - 0.00091f, 0, rightDoor.transform.localPosition.z - 0.0026f);

            yield return new WaitForSeconds(0.04f);
        }
    }
}
