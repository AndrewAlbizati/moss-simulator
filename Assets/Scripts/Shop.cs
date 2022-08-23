using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Shop : MonoBehaviour
{
    public GameObject gameControllerObject;
    public GameObject player;
    public GameObject keybindLabel;

    [Header("Shop Settings")]
    public int price;
    public int minimumTaskIndex;
    public UnityEvent onBuyFunction;

    private GameController gameController;

    // Start is called before the first frame update
    void Start()
    {
        gameController = gameControllerObject.GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!player.GetComponent<PlayerMovement>().IsRiding() && !gameController.IsPaused() && gameController.GetBradleyBucks() >= price)
        {
            float playerX = player.transform.position.x;
            float playerZ = player.transform.position.z;
            float shopX = transform.position.x;
            float shopZ = transform.position.z;

            float distance = Mathf.Sqrt(Mathf.Pow(shopX - playerX, 2) + Mathf.Pow(shopZ - playerZ, 2));

            if (gameController.GetTaskIndex() >= minimumTaskIndex && distance < 5)
            {
                TMP_Text mText = keybindLabel.GetComponent<TMP_Text>();
                mText.SetText(GetShopText());
                keybindLabel.SetActive(true);

                if (Input.GetKeyDown(gameController.actionKey))
                {
                    keybindLabel.SetActive(false);

                    gameController.SpendMoney(price);
                    onBuyFunction.Invoke();
                }
                return;
            }
        }
        if (keybindLabel.GetComponent<TMP_Text>().text == GetShopText())
        {
            keybindLabel.SetActive(false);
        }
    }

    private string GetShopText()
    {
        return "Press " + gameController.actionKey.ToString() + " to buy for $" + price + " Bradley Bucks"; 
    }
}
