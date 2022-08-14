using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TreeShop : MonoBehaviour
{
    public GameObject player;
    public GameObject keybindLabel;
    public GameObject gameControllerObject;

    private GameController gameController;

    private bool isVisible = false;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        gameController = gameControllerObject.GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isVisible)
        {
            if (gameController.GetTaskIndex() < 5)
            {
                return;
            }
            isVisible = true;
            gameObject.GetComponent<MeshRenderer>().enabled = true;
        }

        float playerX = player.transform.position.x;
        float playerZ = player.transform.position.z;
        float shopX = transform.position.x;
        float shopZ = transform.position.z;

        float distance = Mathf.Sqrt(Mathf.Pow(shopX - playerX, 2) + Mathf.Pow(shopZ - playerZ, 2));

        string text = "";
        if (gameController.GetConiferCount() > 0)
        {
            text = "Press B to sell " + gameController.GetConiferCount() + " conifer" + (gameController.GetConiferCount() == 1 ? "" : "s") + " for $" + gameController.GetConiferCount() * 5 + "BB";
        }
         
        if (distance < 5 && !player.GetComponent<PlayerMovement>().IsRiding())
        {
            TMP_Text mText = keybindLabel.GetComponent<TMP_Text>();
            mText.SetText(text);

            if (gameController.GetTaskIndex() >= 5)
            {
                if (gameController.GetConiferCount() > 0)
                {
                    keybindLabel.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.B))
                    {
                        gameController.AddMoney(gameController.GetConiferCount() * 5);
                        gameController.ResetConiferCount();
                        gameController.PlayChaChing();
                    }
                }
            }
            else
            {
                keybindLabel.SetActive(false);
            }
        }
        else if (keybindLabel.GetComponent<TMP_Text>().text == text)
        {
            keybindLabel.SetActive(false);
        }
    }
}
