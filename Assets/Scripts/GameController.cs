using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public KeyCode actionKey;

    public GameObject labelsCanvas;
    public GameObject pauseCanvas;

    public GameObject player;
    public GameObject lawnMower;

    public AudioClip chaChing;
    public AudioClip taskCompleteSound;

    private GameObject moneyLabel;
    private GameObject taskLabel;
    private GameObject keybindLabel;
    private GameObject crosshair;

    private int bradleyBucks = 0;
    private int coniferCount = 0;
    private int taskIndex = 0;

    private float hunger = 1.0f;

    private bool isPaused = false;

    private string[] tasks = {
        "Visit the item shop",

        "Collect $10BB in the abandoned house",
        "Repair the computer screens",

        "Earn $15BB on the computer",
        "Buy an axe from the item shop",

        "Earn $200BB from chopping trees",
        "Buy room decorations",

        "Search the mountains for helpful items",
        "Open the neighbor's house",

        "Earn $500BB for NBA League Pass",
        "Buy NBA League Pass",

        "Earn $500BB for NFL+",
        "Buy NFL+",



        "Buy California portal",
        "Enter the California portal"
    };


    void OnDisable()
    {
        PlayerPrefs.SetFloat("hunger", hunger);
        PlayerPrefs.SetInt("money", bradleyBucks);
        PlayerPrefs.SetInt("conifers", coniferCount);
        PlayerPrefs.SetInt("taskindex", taskIndex);
        PlayerPrefs.SetString("actionKeybind", actionKey.ToString());
    }

    void OnEnable()
    {
        if (PlayerPrefs.HasKey("money") && PlayerPrefs.HasKey("conifers") && PlayerPrefs.HasKey("taskindex"))
        {
            bradleyBucks = PlayerPrefs.GetInt("money");
            coniferCount = PlayerPrefs.GetInt("conifers");
            taskIndex = PlayerPrefs.GetInt("taskindex");
        }

        if (PlayerPrefs.HasKey("actionKeybind"))
        {
            actionKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("actionKeybind"));
        }
        else
        {
            actionKey = KeyCode.E;
        }

        if (PlayerPrefs.HasKey("hunger"))
        {
            hunger = PlayerPrefs.GetFloat("hunger");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        moneyLabel = labelsCanvas.transform.GetChild(0).gameObject;
        taskLabel = labelsCanvas.transform.GetChild(1).gameObject;
        keybindLabel = labelsCanvas.transform.GetChild(2).gameObject;
        crosshair = labelsCanvas.transform.GetChild(3).gameObject;

        UpdateMoneyLabel();
        UpdateTaskLabel();
        UpdateHungerBar();

        moneyLabel.SetActive(true);
        taskLabel.SetActive(true);
        crosshair.SetActive(true);
        keybindLabel.SetActive(false);
        pauseCanvas.SetActive(false);

        InvokeRepeating("DepleteHunger", 1.0f, 1.0f);
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
            return;
        }

        if (player.GetComponent<PlayerMovement>().IsRiding())
        {
            player.GetComponent<CharacterController>().enabled = false;
            Transform seat = lawnMower.transform.GetChild(6);
            player.transform.position = new Vector3(seat.position.x, seat.position.y + 2, seat.position.z);
        }
        else
        {
            player.GetComponent<CharacterController>().enabled = true;
        } 

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

            case 9:
                if (bradleyBucks >= 500)
                {
                    IncrementTaskIndex();
                }
                break;

            case 11:
                if (bradleyBucks >= 500)
                {
                    IncrementTaskIndex();
                }
                break;
        }
    }

    public void PlayChaChing()
    {
        gameObject.GetComponent<AudioSource>().PlayOneShot(chaChing);
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
        UpdateMoneyLabel();
    }

    public void SpendMoney(int amount)
    {
        PlayChaChing();
        bradleyBucks -= amount;
        UpdateMoneyLabel();
    }

    public int GetConiferCount()
    {
        return coniferCount;
    }

    public void IncrementConiferCount()
    {
        coniferCount++;
        UpdateMoneyLabel();
    }

    public void ResetConiferCount()
    {
        coniferCount = 0;
        UpdateMoneyLabel();
    }

    public void UpdateMoneyLabel()
    {
        string bradleyBuckDisplay = "$" + string.Format("{0:n0}", bradleyBucks) + " Bradley Buck" + (bradleyBucks == 1 ? "" : "s");
        string conifersDisplay = taskIndex > 4 ? coniferCount + " Conifer" + (coniferCount == 1 ? "" : "s") : "";
        moneyLabel.GetComponent<TMP_Text>().SetText(bradleyBuckDisplay + "\n" + conifersDisplay);
    }

    public void IncrementTaskIndex()
    {
        gameObject.GetComponent<AudioSource>().PlayOneShot(taskCompleteSound);
        taskIndex++;
        UpdateTaskLabel();
    }

    public void UpdateTaskLabel()
    {
        taskLabel.GetComponent<TMP_Text>().SetText("Task: " + tasks[taskIndex]);
    }

    public void TogglePaused()
    {
        isPaused = !isPaused;
        labelsCanvas.SetActive(!isPaused);
        pauseCanvas.SetActive(isPaused);
    }

    public bool IsPaused()
    {
        return isPaused;
    }

    public void SetHunger(float hungerLevel)
    {
        hunger = hungerLevel;
    }

    public void DepleteHunger()
    {
        if (isPaused)
        {
            // Stop hunger depletion if the game is paused
            return;
        }

        // Deplete hunger just for existing
        hunger -= 0.001f;

        if (player.GetComponent<PlayerMovement>().IsRiding())
        {
            // Stop hunger depletion for walking while riding
        }
        else if (player.GetComponent<PlayerMovement>().isWalking)
        {
            // Deplete more hunger if player is walking
            hunger -= 0.002f;
        }
        else  if (player.GetComponent<PlayerMovement>().isSprinting)
        {
            // Deplete even more hunger if player is sprinting
            hunger -= 0.004f;
        }
        UpdateHungerBar();
    }

    public void UpdateHungerBar()
    {
        labelsCanvas.transform.GetChild(4).GetChild(3).GetComponent<Image>().fillAmount = Mathf.Clamp(hunger, 0.0f, 1.0f);
    }

    public void CheezItsPurchased()
    {
        hunger += 0.1f;
        UpdateHungerBar();
    }

    public void RaisingCanesPurchased()
    {
        hunger += 0.4f;
        UpdateHungerBar();
    }

    public void InNOutPurchased()
    {
        hunger = 1f;
        UpdateHungerBar();
    }
}
