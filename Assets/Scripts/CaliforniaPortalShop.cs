using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CaliforniaPortalShop : MonoBehaviour
{
    public GameObject gameControllerObject;
    public GameObject player;
    public GameObject keybindLabel;
    public GameObject californiaPortal;

    private GameController gameController;
    private string text;

    // Start is called before the first frame update
    void Start()
    {
        gameController = gameControllerObject.GetComponent<GameController>();
        text = "Press " + gameController.actionKey.ToString() + " to buy for $100,000 Bradley Bucks";
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameController.IsPaused())
        {
            if (gameController.GetBradleyBucks() >= 100000)
            {
                float playerX = player.transform.position.x;
                float playerZ = player.transform.position.z;
                float shopX = transform.position.x;
                float shopZ = transform.position.z;

                float distance = Mathf.Sqrt(Mathf.Pow(shopX - playerX, 2) + Mathf.Pow(shopZ - playerZ, 2));

                if (distance < 5)
                {
                    TMP_Text mText = keybindLabel.GetComponent<TMP_Text>();
                    mText.SetText(text);
                    keybindLabel.SetActive(true);

                    if (Input.GetKeyDown(gameController.actionKey))
                    {
                        keybindLabel.SetActive(false);
                        californiaPortal.SetActive(true);
                        gameObject.SetActive(false);
                        gameController.IncrementTaskIndex();
                        gameController.SpendMoney(100000);
                    }
                }
                else if (keybindLabel.GetComponent<TMP_Text>().text == text)
                {
                    keybindLabel.SetActive(false);
                }
            }
            else
            {
                keybindLabel.SetActive(false);
            }
        }
    }
}
