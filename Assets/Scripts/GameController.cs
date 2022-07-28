using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public TMP_Text moneyLabel;
    public TMP_Text taskLabel;

    private int bradleyBucks = 0;
    private int taskIndex = 0;

    private string[] tasks = { "Visit the item shop", "Repair computer screens",



        "Search the forest for helpful items", "Open the neighbor's house", "Buy California portal" };


    private int[] taskRewards = { };

    // Start is called before the first frame update
    void Start()
    {
   
    }

    // Update is called once per frame
    void Update()
    {
        moneyLabel.SetText(bradleyBucks + " Bradley Bucks");
        taskLabel.SetText("Task: " + tasks[taskIndex]);
    }
}
