using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PauseButtons : MonoBehaviour
{
    public GameObject gameControllerObject;
    public GameObject player;

    private GameController gameController;

    private void Start()
    {
        gameController = gameControllerObject.GetComponent<GameController>();
    }

    public void OnQuitButtonPressed()
    {
        SceneManager.LoadScene("Menu");
    }

    public void OnOptionsButtonPressed()
    {
        PlayerPrefs.SetString("previousScene", "Ohio");
        SceneManager.LoadScene("Options");
    }

    public void OkButtonPressed()
    {
        gameController.TogglePaused();
        player.transform.GetChild(1).GetChild(1).GetChild(3).gameObject.SetActive(false);
    }
}
