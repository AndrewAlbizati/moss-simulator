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

    private int bradleyBucks = 0;
    private int taskIndex = 0;

    private string[] tasks = { "Visit the item shop", "Repair computer screens",



        "Search the forest for helpful items", "Open the neighbor's house", "Buy California portal" };

    // Start is called before the first frame update
    void Start()
    {
        chaChing.Stop();
        taskCompleteSound.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        moneyLabel.GetComponent<TMP_Text>().SetText(bradleyBucks + " Bradley Bucks");
        taskLabel.GetComponent<TMP_Text>().SetText("Task: " + tasks[taskIndex]);

        float playerX = player.transform.position.x;
        float playerZ = player.transform.position.z;

        float storeX = -325f;
        float storeZ = 185f;

        float shopDistance = Mathf.Sqrt(Mathf.Pow(storeX - playerX, 2) + Mathf.Pow(storeZ - playerZ, 2));
        if (taskIndex == 0 && shopDistance < 10)
        {
            IncrementTaskIndex();
            AddMoney(10);
        } else if (taskIndex == 1)
        {

        }
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
