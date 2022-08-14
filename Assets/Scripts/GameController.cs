using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject labelsCanvas;
    public GameObject pauseCanvas;

    public GameObject player;
    public GameObject lawnMower;

    public AudioSource chaChing;
    public AudioSource taskCompleteSound;

    private GameObject moneyLabel;
    private GameObject taskLabel;
    private GameObject keybindLabel;
    private GameObject crosshair;

    private int bradleyBucks = 0;
    private int coniferCount = 0;
    private int taskIndex = 0;

    private bool isPaused = false;
    private bool isRiding = false;

    private string[] tasks =
        { "Visit the item shop",
        "Collect $10BB in the abandoned house",
        "Repair the computer screens",
        "Earn $15BB on the computer",
        "Buy an axe from the item shop",
        "Earn $200BB from chopping trees",
        "Buy room decorations",
        "Search the mountains for helpful items",
        "Open the neighbor's house",





        "Buy California portal",
        "Enter the California portal"};


    void OnDisable()
    {
        PlayerPrefs.SetInt("money", bradleyBucks);
        PlayerPrefs.SetInt("conifers", coniferCount);
        PlayerPrefs.SetInt("taskindex", taskIndex);
    }

    void OnEnable()
    {
        if (PlayerPrefs.HasKey("money") && PlayerPrefs.HasKey("conifers") && PlayerPrefs.HasKey("taskindex"))
        {
            bradleyBucks = PlayerPrefs.GetInt("money");
            coniferCount = PlayerPrefs.GetInt("conifers");
            taskIndex = PlayerPrefs.GetInt("taskindex");
        }
        else
        {
            bradleyBucks = 0;
            coniferCount = 0;
            taskIndex = 0;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        moneyLabel = labelsCanvas.transform.GetChild(0).gameObject;
        taskLabel = labelsCanvas.transform.GetChild(1).gameObject;
        keybindLabel = labelsCanvas.transform.GetChild(2).gameObject;
        crosshair = labelsCanvas.transform.GetChild(3).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePaused();
        }


        if (isPaused)
        {
            moneyLabel.SetActive(false);
            taskLabel.SetActive(false);
            keybindLabel.SetActive(false);
            crosshair.SetActive(false);
            pauseCanvas.SetActive(true);
            return;
        } else
        {
            moneyLabel.SetActive(true);
            taskLabel.SetActive(true);
            crosshair.SetActive(true);
            pauseCanvas.SetActive(false);
        }

        if (isRiding)
        {
            player.GetComponent<CharacterController>().enabled = false;
            Transform seat = lawnMower.transform.GetChild(6);
            player.transform.position = new Vector3(seat.position.x, seat.position.y + 2, seat.position.z);
        }
        else
        {
            player.GetComponent<CharacterController>().enabled = true;
        }

        string bradleyBuckDisplay = string.Format("{0:n0}", bradleyBucks) + " Bradley Buck" + (bradleyBucks == 1 ? "" : "s");
        string conifersDisplay = taskIndex > 4 ? coniferCount + " Conifer" + (coniferCount == 1 ? "" : "s") : "";
        moneyLabel.GetComponent<TMP_Text>().SetText(bradleyBuckDisplay + "\n" + conifersDisplay);

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

            case 3:
                if (bradleyBucks >= 15)
                {
                    IncrementTaskIndex();
                }
                break;
            case 5:
                if (bradleyBucks >= 200)
                {
                    IncrementTaskIndex();
                }
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
        PlayChaChing();
        bradleyBucks -= amount;
    }

    public int GetConiferCount()
    {
        return coniferCount;
    }

    public void IncrementConiferCount()
    {
        coniferCount++;
    }

    public void ResetConiferCount()
    {
        coniferCount = 0;
    }

    public void IncrementTaskIndex()
    {
        taskCompleteSound.Play();
        taskIndex++;
    }

    public void TogglePaused()
    {
        isPaused = !isPaused;
    }

    public bool IsPaused()
    {
        return isPaused;
    }

    public void ToggleRiding()
    {
        isRiding = !isRiding;
        if (isRiding)
        {

        }
    }

    public bool IsRiding()
    {
        return isRiding;
    }
}
