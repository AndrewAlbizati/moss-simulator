using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public GameObject player;
    public GameObject screen1;
    public GameObject screen2;
    public GameObject decorations;
    public GameObject keybindLabel;
    public GameObject gameControllerObject;

    public Material[] materials;

    private GameController gameController;

    private int[] prices = { 10, 15, 200, 500, 500 };

    private int shopIndex = 0;

    private void OnEnable()
    {
        if (PlayerPrefs.HasKey("shopindex"))
        {
            shopIndex = PlayerPrefs.GetInt("shopindex");
        }

        screen1.SetActive(shopIndex > 0);
        screen2.SetActive(shopIndex > 0);

        decorations.SetActive(shopIndex > 2);
    }

    private void OnDisable()
    {
        PlayerPrefs.SetInt("shopindex", shopIndex);
    }

    // Start is called before the first frame update
    void Start()
    {
        gameController = gameControllerObject.GetComponent<GameController>();

        keybindLabel.SetActive(false);
        UpdateMaterial();
    }

    // Update is called once per frame
    void Update()
    {
        string text = "Press " + gameController.actionKey.ToString() + " to buy for $" + prices[shopIndex] + " Bradley Bucks";

        float playerX = player.transform.position.x;
        float playerZ = player.transform.position.z;
        float shopX = transform.position.x;
        float shopZ = transform.position.z;

        float distance = Mathf.Sqrt(Mathf.Pow(shopX - playerX, 2) + Mathf.Pow(shopZ - playerZ, 2));

        if (distance < 5 && !player.GetComponent<PlayerMovement>().IsRiding())
        {
            TMP_Text mText = keybindLabel.GetComponent<TMP_Text>();
            mText.SetText(text);

            if (gameController.GetBradleyBucks() >= prices[shopIndex])
            {
                switch (shopIndex)
                {
                    case 0:
                        if (gameController.GetTaskIndex() == 2)
                        {
                            keybindLabel.SetActive(true);
                            if (Input.GetKeyDown(gameController.actionKey))
                            {
                                keybindLabel.SetActive(false);
                                screen1.SetActive(true);
                                screen2.SetActive(true);

                                gameController.IncrementTaskIndex();
                                gameController.SpendMoney(prices[shopIndex]);

                                shopIndex++;
                                UpdateMaterial();
                            }
                        }
                        break;
                    case 1:
                        if (gameController.GetTaskIndex() == 4)
                        {
                            keybindLabel.SetActive(true);
                            if (Input.GetKeyDown(gameController.actionKey))
                            {
                                keybindLabel.SetActive(false);

                                gameController.IncrementTaskIndex();
                                gameController.SpendMoney(prices[shopIndex]);

                                shopIndex++;
                                UpdateMaterial();
                            }
                        }
                        break;
                    case 2:
                        if (gameController.GetTaskIndex() == 6)
                        {
                            keybindLabel.SetActive(true);
                            if (Input.GetKeyDown(gameController.actionKey))
                            {
                                keybindLabel.SetActive(false);
                                decorations.SetActive(true);

                                gameController.IncrementTaskIndex();
                                gameController.SpendMoney(prices[shopIndex]);

                                shopIndex++;
                                UpdateMaterial();
                            }
                        }
                        break;
                    case 3:
                        if (gameController.GetTaskIndex() == 10)
                        {
                            keybindLabel.SetActive(true);
                            if (Input.GetKeyDown(gameController.actionKey))
                            {
                                keybindLabel.SetActive(false);

                                gameController.IncrementTaskIndex();
                                gameController.SpendMoney(prices[shopIndex]);

                                shopIndex++;
                                UpdateMaterial();
                            }
                        }
                        break;
                    case 4:
                        if (gameController.GetTaskIndex() == 12)
                        {
                            keybindLabel.SetActive(true);
                            if (Input.GetKeyDown(gameController.actionKey))
                            {
                                keybindLabel.SetActive(false);

                                gameController.IncrementTaskIndex();
                                gameController.SpendMoney(prices[shopIndex]);

                                shopIndex++;
                                UpdateMaterial();
                            }
                        }
                        break;
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

    void UpdateMaterial()
    {
        MeshRenderer mr = gameObject.GetComponent<MeshRenderer>();
        mr.sharedMaterial = materials[shopIndex];
    }
}
