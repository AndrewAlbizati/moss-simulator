using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PauseButtons : MonoBehaviour
{
    public void OnQuitButtonPressed()
    {
        SceneManager.LoadScene("Menu");
    }

    public void OnOptionsButtonPressed()
    {
        PlayerPrefs.SetString("previousScene", "Ohio");
        SceneManager.LoadScene("Options");
    }
}
