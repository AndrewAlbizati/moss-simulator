using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ComputerController : MonoBehaviour
{
    public GameObject mlbButton;
    public GameObject nbaButton;
    public GameObject nflButton;

    public GameObject backButton;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        mlbButton.GetComponent<Button>().interactable = true;

        int taskIndex = PlayerPrefs.GetInt("taskindex");

        nbaButton.GetComponent<Button>().interactable = taskIndex > 9;

        nflButton.GetComponent<Button>().interactable = taskIndex > 10;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MLBButtonPressed()
    {
        SceneManager.LoadScene("Baseball");
    }

    public void NBAButtonPressed()
    {
        SceneManager.LoadScene("Basketball");
    }

    public void NFLButtonPressed()
    {
        SceneManager.LoadScene("Football");
    }

    public void BackButtonPressed()
    {
        SceneManager.LoadScene("Ohio");
    }
}
