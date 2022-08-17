using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LawnMower : MonoBehaviour
{
    public GameObject gameControllerObject;
    public GameObject player;
    public GameObject keybindLabel;

    private GameController gameController;
    private string rideText;
    private string dismountText;

    private void OnDisable()
    {
        PlayerPrefs.SetFloat("mowerPosX", transform.position.x);
        PlayerPrefs.SetFloat("mowerPosY", transform.position.y);
        PlayerPrefs.SetFloat("mowerPosZ", transform.position.z);

        PlayerPrefs.SetFloat("mowerRotX", transform.eulerAngles.x);
        PlayerPrefs.SetFloat("mowerRotY", transform.eulerAngles.y);
        PlayerPrefs.SetFloat("mowerRotZ", transform.eulerAngles.z);
    }

    private void OnEnable()
    {
        if (PlayerPrefs.HasKey("mowerPosX") && PlayerPrefs.HasKey("mowerPosY") && PlayerPrefs.HasKey("mowerPosZ") && PlayerPrefs.HasKey("mowerRotX") && PlayerPrefs.HasKey("mowerRotY") && PlayerPrefs.HasKey("mowerRotZ"))
        {
            gameObject.transform.position = new Vector3(PlayerPrefs.GetFloat("mowerPosX"), PlayerPrefs.GetFloat("mowerPosY"), PlayerPrefs.GetFloat("mowerPosZ"));
            gameObject.transform.eulerAngles = new Vector3(PlayerPrefs.GetFloat("mowerRotX"), PlayerPrefs.GetFloat("mowerRotY"), PlayerPrefs.GetFloat("mowerRotZ"));
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        gameController = gameControllerObject.GetComponent<GameController>();
        rideText = "Press " + gameController.actionKey.ToString() + " to ride on lawn mower";
        dismountText = "Press " + gameController.actionKey.ToString() + " to dismount lawn mower";
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameController.IsPaused() && gameController.GetTaskIndex() > 8)
        {
            float playerX = player.transform.position.x;
            float playerZ = player.transform.position.z;
            float mowerX = transform.position.x;
            float mowerZ = transform.position.z;

            float distance = Mathf.Sqrt(Mathf.Pow(mowerX - playerX, 2) + Mathf.Pow(mowerZ - playerZ, 2));

            if (distance < 5)
            {
                TMP_Text mText = keybindLabel.GetComponent<TMP_Text>();
                mText.SetText(player.GetComponent<PlayerMovement>().IsRiding() ? dismountText : rideText);
                keybindLabel.SetActive(true);

                if (Input.GetKeyDown(gameController.actionKey))
                {
                    player.GetComponent<PlayerMovement>().ToggleRiding();
                }
            }
            else if (keybindLabel.GetComponent<TMP_Text>().text == rideText || keybindLabel.GetComponent<TMP_Text>().text == dismountText)
            {
                keybindLabel.SetActive(false);
            }


            if (player.GetComponent<PlayerMovement>().IsRiding())
            {
                transform.eulerAngles = new Vector3(player.transform.eulerAngles.x, player.transform.eulerAngles.y + 180, player.transform.eulerAngles.z);
            }
        }
    }
}


