using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CaliforniaPortalShop : MonoBehaviour
{
    public GameObject gameController;
    public GameObject player;
    public GameObject keybindLabel;
    public GameObject californiaPortal;

    private string text = "Press B to buy for $100,000 Bradley Bucks";

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeInHierarchy)
        {
            if (gameController.GetComponent<GameController>().GetBradleyBucks() >= 100000)
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

                    if (Input.GetKeyDown(KeyCode.B))
                    {
                        keybindLabel.SetActive(false);
                        californiaPortal.SetActive(true);
                        gameObject.SetActive(false);
                        gameController.GetComponent<GameController>().IncrementTaskIndex();
                        gameController.GetComponent<GameController>().SpendMoney(100000);
                    }
                }
                else if (keybindLabel.GetComponent<TMP_Text>().text == text)
                {
                    keybindLabel.SetActive(false);
                }
            }
        }
    }
}
