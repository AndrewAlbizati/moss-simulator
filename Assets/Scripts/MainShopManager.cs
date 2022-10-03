using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class MainShopManager : MonoBehaviour
{
    [Header("Game Controller")]
    public GameObject gameControllerObject;

    [Header("Screens")]
    public GameObject screen1;
    public GameObject screen2;

    [Header("Food Shops")]
    public GameObject foodShop1;
    public GameObject foodShop2;
    public GameObject foodShop3;

    [Header("Spawnable Objects")]
    public GameObject decorations;
    public GameObject cornFarm;
    public GameObject lawnMower;

    [Header("Shop Lists")]
    public int[] prices = { 10, 15, 200, 500, 500, 1000 };
    public int[] minimumTaskIndexes = { 2, 4, 6, 10, 12, 14 };
    public Material[] materials;
    public UnityEvent[] functions;

    private GameController gameController;
    private Shop shop;
    private int shopIndex = 0;

    private void OnEnable()
    {
        if (PlayerPrefs.HasKey("shopIndex"))
        {
            shopIndex = PlayerPrefs.GetInt("shopIndex");
        }

        screen1.SetActive(shopIndex > 0);
        screen2.SetActive(shopIndex > 0);

        foodShop1.SetActive(shopIndex > 0);
        foodShop2.SetActive(shopIndex > 0);
        foodShop3.SetActive(shopIndex > 0);

        decorations.SetActive(shopIndex > 2);
        cornFarm.SetActive(shopIndex > 5);

        if (shopIndex > prices.Length - 1)
        {
            gameObject.SetActive(false);
            return;
        }
    }

    private void OnDisable()
    {
        PlayerPrefs.SetInt("shopIndex", shopIndex);
    }

    // Start is called before the first frame update
    void Start()
    {
        gameController = gameControllerObject.GetComponent<GameController>();
        shop = GetComponent<Shop>();

        UpdateMaterial();
    }

    public void ScreenRepairBought()
    {
        screen1.SetActive(true);
        screen2.SetActive(true);

        foodShop1.SetActive(true);
        foodShop2.SetActive(true);
        foodShop3.SetActive(true);

        ItemPurchased();
    }

    public void DecorationsBought()
    {
        decorations.SetActive(true);
        ItemPurchased();
    }

    public void CornFarmBought()
    {
        float farmX = cornFarm.transform.GetChild(0).transform.position.x;
        float farmZ = cornFarm.transform.GetChild(0).transform.position.z;
        float mowerX = lawnMower.transform.GetChild(3).position.x;
        float mowerZ = lawnMower.transform.GetChild(3).position.z;

        float distance = Mathf.Sqrt(Mathf.Pow(mowerX - farmX, 2) + Mathf.Pow(mowerZ - farmZ, 2));

        if (distance < 15)
        {
            lawnMower.transform.position = new Vector3(-220.41f, 16.19f, 88.82f);
        }

        cornFarm.SetActive(true);
        ItemPurchased();
    }

    public void ItemPurchased()
    {
        shopIndex++;
        gameController.IncrementTaskIndex();
        UpdateMaterial();
    }

    void UpdateMaterial()
    {
        if (shopIndex > prices.Length - 1)
        {
            gameObject.SetActive(false);
            return;
        }
        gameObject.GetComponent<MeshRenderer>().sharedMaterial = materials[shopIndex];
        shop.price = prices[shopIndex];
        shop.minimumTaskIndex = minimumTaskIndexes[shopIndex];
        shop.onBuyFunction = functions[shopIndex];
    }
}
