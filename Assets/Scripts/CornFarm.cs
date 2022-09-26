using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;

public class CornFarm : MonoBehaviour
{
    public GameObject gameControllerObject;
    public GameObject player;
    public GameObject keybindLabel;

    private float growthLevel = 1.0f;
    private int pricePerCorn = 50;
    private string text;
    private GameController gameController;

    private void OnEnable()
    {
        if (PlayerPrefs.HasKey("growthLevel"))
        {
            growthLevel = PlayerPrefs.GetFloat("growthLevel");
        }
        UpdateScale();
    }

    private void OnDisable()
    {
        PlayerPrefs.SetFloat("growthLevel", growthLevel);
    }

    // Start is called before the first frame update
    void Start()
    {
        gameController = gameControllerObject.GetComponent<GameController>();
        
        text = "Press " + gameController.actionKey.ToString() + " to harvest";
        InvokeRepeating("Grow", 1f, 0.25f);
    }

    // Update is called once per frame
    void Update()
    {
        if (!player.GetComponent<PlayerMovement>().IsRiding() && !gameController.IsPaused())
        {
            float playerX = player.transform.position.x;
            float playerZ = player.transform.position.z;
            float farmX = transform.position.x;
            float farmZ = transform.position.z;

            float distance = Mathf.Sqrt(Mathf.Pow(farmX - playerX, 2) + Mathf.Pow(farmZ - playerZ, 2));

            if (growthLevel >= 3.5f && distance < 10)
            {
                TMP_Text mText = keybindLabel.GetComponent<TMP_Text>();
                mText.SetText(text);
                keybindLabel.SetActive(true);

                if (Input.GetKeyDown(gameController.actionKey))
                {
                    keybindLabel.SetActive(false);
                    Harvest();
                }
                return;
            }
        }
        if (keybindLabel.GetComponent<TMP_Text>().text == text)
        {
            keybindLabel.SetActive(false);
        }
    }

    private void Grow()
    {
        if (gameController.IsPaused() || growthLevel >= 3.5f)
        {
            return;
        }

        growthLevel += 0.005f;
        UpdateScale();
    }

    private void Harvest()
    {
        growthLevel = 1.0f;

        gameController.AddMoney(16 * pricePerCorn);
        gameController.PlayChaChing();

        UpdateScale();
    }

    private void UpdateScale()
    {
        for (int i = 1; i <= 16; i++)
        {
            gameObject.transform.GetChild(i).transform.localScale = new Vector3(growthLevel, growthLevel, growthLevel);
        }
    }
}
