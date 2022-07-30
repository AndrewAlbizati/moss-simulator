using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject moneyLabel;
    public GameObject taskLabel;
    public GameObject player;
    public AudioSource chaChing;
    public AudioSource taskCompleteSound;

    private int bradleyBucks = 100000;
    private int taskIndex = 0;

    private string[] tasks =
        { "Visit the item shop",
        "Collect $10BB in the abandoned house",
        "Repair computer screens",
        "Search the forest for helpful items",
        "Open the neighbor's house",
        "Buy California portal",
        "Enter the California portal"};

    // Start is called before the first frame update
    void Start()
    {
        chaChing.Stop();
        taskCompleteSound.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        moneyLabel.GetComponent<TMP_Text>().SetText(string.Format("{0:n0}", bradleyBucks) + " Bradley Buck" + (bradleyBucks == 1 ? "" : "s"));
        taskLabel.GetComponent<TMP_Text>().SetText("Task: " + tasks[taskIndex]);

        float playerX = player.transform.position.x;
        float playerZ = player.transform.position.z;

        switch (taskIndex)
        {
            case 0:
                float storeX = -325f;
                float storeZ = 185f;

                float shopDistance = Mathf.Sqrt(Mathf.Pow(storeX - playerX, 2) + Mathf.Pow(storeZ - playerZ, 2));

                if (shopDistance < 10)
                {
                    IncrementTaskIndex();
                }
                break;

            case 1:
                if (bradleyBucks >= 10)
                {
                    IncrementTaskIndex();
                }
                break;

            case 2:
                break;
        }
    }

    public void PlayChaChing()
    {
        chaChing.Play();
    }

    public int GetTaskIndex()
    {
        return taskIndex;
    }

    public int GetBradleyBucks()
    {
        return bradleyBucks;
    }

    public void AddMoney(int amount)
    {
        bradleyBucks += amount;
    }

    public void SpendMoney(int amount)
    {
        chaChing.Play();
        bradleyBucks -= amount;
    }

    public void IncrementTaskIndex()
    {
        taskCompleteSound.Play();
        taskIndex++;
    } 
}
