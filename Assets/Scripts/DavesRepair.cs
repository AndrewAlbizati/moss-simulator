using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DavesRepair : MonoBehaviour
{
    public GameObject player;
    public GameObject screen1;
    public GameObject screen2;
    public GameObject keybindLabel;
    public GameObject gameController;

    private bool purchased = false;

    // Start is called before the first frame update
    void Start()
    {
        screen1.SetActive(false);
        screen2.SetActive(false);
        keybindLabel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        float playerX = player.transform.position.x;
        float playerZ = player.transform.position.z;
        float shopX = transform.position.x;
        float shopZ = transform.position.z;

        float distance = Mathf.Sqrt(Mathf.Pow(shopX - playerX, 2) + Mathf.Pow(shopZ - playerZ, 2));

        if (distance < 5)
        {
            if (!purchased)
            {
                keybindLabel.SetActive(true);
                TMP_Text mText = keybindLabel.GetComponent<TMP_Text>();
                mText.SetText("Press B to buy for $10 Bradley Bucks");
                keybindLabel.SetActive(true);

                if (Input.GetKeyDown(KeyCode.B))
                {
                    purchased = true;
                    keybindLabel.SetActive(false);
                    screen1.SetActive(true);
                    screen2.SetActive(true);
                    gameController.GetComponent<GameController>().IncrementTaskIndex();
                    gameController.GetComponent<GameController>().SpendMoney(10);
                }
            }
        }
        else
        {
            keybindLabel.SetActive(false);
        }
    }
}
