using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class MainShopManager : MonoBehaviour
{
    public GameObject screen1;
    public GameObject screen2;
    public GameObject decorations;
    public GameObject gameControllerObject;

    public int[] prices = { 10, 15, 200, 500, 500 };
    public int[] minimumTaskIndexes = { 2, 4, 6, 10, 12 };
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

        decorations.SetActive(shopIndex > 2);
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

    // Update is called once per frame
    void Update()
    {

    }

    public void ScreenRepairBought()
    {
        screen1.SetActive(true);
        screen2.SetActive(true);
        ItemPurchased();
    }

    public void DecorationsBought()
    {
        decorations.SetActive(true);
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
        gameObject.GetComponent<MeshRenderer>().sharedMaterial = materials[shopIndex];
        shop.price = prices[shopIndex];
        shop.minimumTaskIndex = minimumTaskIndexes[shopIndex];
        shop.onBuyFunction = functions[shopIndex];
    }
}
