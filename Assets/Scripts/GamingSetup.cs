using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GamingSetup : MonoBehaviour
{
    public GameObject player;
    public GameObject keybindLabel;

    private string text = "Press B to gamble on MLB Games";

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.GetChild(1).GetChild(2).gameObject.activeInHierarchy)
        {
            float playerX = player.transform.position.x;
            float playerZ = player.transform.position.z;
            float shopX = transform.position.x;
            float shopZ = transform.position.z;

            float distance = Mathf.Sqrt(Mathf.Pow(shopX - playerX, 2) + Mathf.Pow(shopZ - playerZ, 2));

            if (distance < 5 && !player.GetComponent<PlayerMovement>().IsRiding())
            {
                TMP_Text mText = keybindLabel.GetComponent<TMP_Text>();
                mText.SetText(text);
                keybindLabel.SetActive(true);

                if (Input.GetKeyDown(KeyCode.B))
                {
                    SceneManager.LoadScene("Baseball");
                }
            }
            else if (keybindLabel.GetComponent<TMP_Text>().text == text)
            {
                keybindLabel.SetActive(false);
            }
        }
    }
}
