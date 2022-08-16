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
    private bool hasScreens = false;
    private bool hasDecorations = false;

    private string[] texts;
    

    private int[] prices = { 10, 15, 200, 500 };

    private int shopIndex = 0;

    private void OnEnable()
    {
        if (PlayerPrefs.HasKey("shopindex"))
        {
            shopIndex = PlayerPrefs.GetInt("shopindex");
            
        }

        if (PlayerPrefs.HasKey("screensactive"))
        {
            hasScreens = PlayerPrefs.GetInt("screensactive") == 1;
        }

        if (PlayerPrefs.HasKey("hasaxe"))
        {
        
        }

        if (PlayerPrefs.HasKey("hasdecorations"))
        {
            hasDecorations = PlayerPrefs.GetInt("hasdecorations") == 1;
        }
    }

    private void OnDisable()
    {
        PlayerPrefs.SetInt("shopindex", shopIndex);
        PlayerPrefs.SetInt("screensactive", hasScreens ? 1 : 0);
        PlayerPrefs.SetInt("hasdecorations", hasDecorations ? 1 : 0);
    }

    // Start is called before the first frame update
    void Start()
    {
        gameController = gameControllerObject.GetComponent<GameController>();

        texts = new string[] {
            "Press " + gameController.actionKey.ToString() + " to buy for $10 Bradley Bucks",
            "Press " + gameController.actionKey.ToString() + " to buy for $15 Bradley Bucks",
            "Press " + gameController.actionKey.ToString() + " to buy for $200 Bradley Bucks",
            "Press " + gameController.actionKey.ToString() + " to buy for $500 Bradley Bucks",
        };

        screen1.SetActive(false);
        screen2.SetActive(false);
        decorations.SetActive(false);
        keybindLabel.SetActive(false);
        UpdateMaterial();
    }

    // Update is called once per frame
    void Update()
    {
        screen1.SetActive(hasScreens);
        screen2.SetActive(hasScreens);
        decorations.SetActive(hasDecorations);


        float playerX = player.transform.position.x;
        float playerZ = player.transform.position.z;
        float shopX = transform.position.x;
        float shopZ = transform.position.z;

        float distance = Mathf.Sqrt(Mathf.Pow(shopX - playerX, 2) + Mathf.Pow(shopZ - playerZ, 2));

        if (distance < 5 && !player.GetComponent<PlayerMovement>().IsRiding())
        {
            TMP_Text mText = keybindLabel.GetComponent<TMP_Text>();
            mText.SetText(texts[shopIndex]);

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
                                hasScreens = true;


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
                                hasDecorations = true;

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
                }
            }
            else
            {
                keybindLabel.SetActive(false);
            }
            
        }
        else if (keybindLabel.GetComponent<TMP_Text>().text == texts[shopIndex])
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
