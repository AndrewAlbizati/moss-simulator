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
        if (gameObject.activeInHierarchy)
        {
            if (gameController.GetComponent<GameController>().GetTaskIndex() == 2)
            {
                if (gameController.GetComponent<GameController>().GetBradleyBucks() >= 10)
                {
                    float playerX = player.transform.position.x;
                    float playerZ = player.transform.position.z;
                    float shopX = transform.position.x;
                    float shopZ = transform.position.z;

                    float distance = Mathf.Sqrt(Mathf.Pow(shopX - playerX, 2) + Mathf.Pow(shopZ - playerZ, 2));

                    if (distance < 5)
                    {
                        keybindLabel.SetActive(true);
                        TMP_Text mText = keybindLabel.GetComponent<TMP_Text>();
                        mText.SetText("Press B to buy for $10 Bradley Bucks");
                        keybindLabel.SetActive(true);

                        if (Input.GetKeyDown(KeyCode.B))
                        {
                            keybindLabel.SetActive(false);
                            screen1.SetActive(true);
                            screen2.SetActive(true);
                            gameObject.SetActive(false);
                            gameController.GetComponent<GameController>().IncrementTaskIndex();
                            gameController.GetComponent<GameController>().SpendMoney(10);
                        }
                    }
                    else
                    {
                        keybindLabel.SetActive(false);
                    }
                }
            }
        }
    }
}
