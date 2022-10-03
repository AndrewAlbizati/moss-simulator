using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ComputerController : MonoBehaviour
{
    [Header("Background")]
    public GameObject background;

    [Header("Buttons")]
    public GameObject mlbButton;
    public GameObject nbaButton;
    public GameObject nflButton;

    [Header("Back to Game Button")]
    public GameObject backButton;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        mlbButton.GetComponent<Button>().interactable = true;

        int taskIndex = PlayerPrefs.GetInt("taskindex");

        nbaButton.GetComponent<Button>().interactable = taskIndex > 10;

        nflButton.GetComponent<Button>().interactable = taskIndex > 12;
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

    public void ApplicationPressed(Sprite backgroundSprite)
    {
        background.GetComponent<Image>().sprite = backgroundSprite;
    }
}
