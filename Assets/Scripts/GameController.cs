using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject moneyLabel;
    public GameObject taskLabel;
    public GameObject pauseLabel;

    public GameObject player;
    public AudioSource chaChing;
    public AudioSource taskCompleteSound;
    public bool isPaused = false;

    private int bradleyBucks = 0;
    private int taskIndex = 0;

    private string[] tasks =
        { "Visit the item shop",
        "Collect $10BB in the abandoned house",
        "Repair computer screens",
        "Earn $15BB on the computer",
        "Buy an axe from the item shop",
        "Earn $200BB from chopping trees",
        "Buy room decorations from the item shop",
        "Search the forest for helpful items",
        "Open the neighbor's house",
        "Buy California portal",
        "Enter the California portal"};


    void OnDisable()
    {
        PlayerPrefs.SetInt("money", bradleyBucks);
        PlayerPrefs.SetInt("taskindex", taskIndex);
    }

    void OnEnable()
    {
        if (PlayerPrefs.HasKey("money") && PlayerPrefs.HasKey("taskindex"))
        {
            bradleyBucks = PlayerPrefs.GetInt("money");
            taskIndex = PlayerPrefs.GetInt("taskindex");

        }
        else
        {
            bradleyBucks = 0;
            taskIndex = 0;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        chaChing.Stop();
        taskCompleteSound.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
        }

        if (isPaused)
        {
            moneyLabel.SetActive(false);
            taskLabel.SetActive(false);
            pauseLabel.SetActive(true);
            return;
        } else
        {
            moneyLabel.SetActive(true);
            taskLabel.SetActive(true);
            pauseLabel.SetActive(false);
        }

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


        if (Input.GetKeyDown(KeyCode.K))
        {
            SceneManager.LoadScene("Baseball");
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
