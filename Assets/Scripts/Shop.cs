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
    public GameObject gameController;

    public Material[] materials;


    private bool hasScreens = false;
    private bool hasDecorations = false;

    private readonly string[] texts =
    {
        "Press B to buy for $10 Bradley Bucks",
        "Press B to buy for $15 Bradley Bucks",
        "Press B to buy for $200 Bradley Bucks",
    };

    private int[] prices = { 10, 15, 200 };

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

            if (gameController.GetComponent<GameController>().GetBradleyBucks() >= prices[shopIndex])
            {
                switch (shopIndex)
                {
                    case 0:
                        if (gameController.GetComponent<GameController>().GetTaskIndex() == 2)
                        {
                            keybindLabel.SetActive(true);
                            if (Input.GetKeyDown(KeyCode.B))
                            {
                                keybindLabel.SetActive(false);
                                screen1.SetActive(true);
                                screen2.SetActive(true);
                                hasScreens = true;


                                gameController.GetComponent<GameController>().IncrementTaskIndex();
                                gameController.GetComponent<GameController>().SpendMoney(prices[shopIndex]);

                                shopIndex++;
                                UpdateMaterial();
                                /*gameController.GetComponent<GameController>().AddMoney(15);
                                gameController.GetComponent<GameController>().IncrementTaskIndex();*/
                            }
                        }
                        break;
                    case 1:
                        if (gameController.GetComponent<GameController>().GetTaskIndex() == 4)
                        {
                            keybindLabel.SetActive(true);
                            if (Input.GetKeyDown(KeyCode.B))
                            {
                                keybindLabel.SetActive(false);

                                gameController.GetComponent<GameController>().IncrementTaskIndex();
                                gameController.GetComponent<GameController>().SpendMoney(prices[shopIndex]);

                                shopIndex++;
                                UpdateMaterial();


                                /*gameController.GetComponent<GameController>().AddMoney(200);
                                gameController.GetComponent<GameController>().IncrementTaskIndex();*/
                            }
                        }
                        break;
                    case 2:
                        if (gameController.GetComponent<GameController>().GetTaskIndex() == 6)
                        {
                            keybindLabel.SetActive(true);
                            if (Input.GetKeyDown(KeyCode.B))
                            {
                                keybindLabel.SetActive(false);
                                decorations.SetActive(true);
                                hasDecorations = true;

                                gameController.GetComponent<GameController>().IncrementTaskIndex();
                                gameController.GetComponent<GameController>().SpendMoney(prices[shopIndex]);

                                /*shopIndex++;
                                UpdateMaterial();*/
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
