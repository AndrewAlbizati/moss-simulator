using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private AudioSource titleScreenMusic;
    private bool buttonClicked = false;

    private static bool initDone = false;

    void Start()
    {
        if (initDone == false)
        {
            initDone = true;
            PlayerPrefs.DeleteAll();
        }

        titleScreenMusic = gameObject.GetComponent<AudioSource>();
        titleScreenMusic.Play();
    }

    void Update()
    {
        // Loop the title screen music
        if (!buttonClicked && !titleScreenMusic.isPlaying)
        {
            titleScreenMusic.Play();
        }
    }

    public void NewGame()
    {
        buttonClicked = true;
        titleScreenMusic.Stop();
        SceneManager.LoadScene("Ohio");
    }

    public void QuitGame()
    {
        buttonClicked = true;
        titleScreenMusic.Stop();
        Application.Quit();
    }

    public void ShowOptions()
    {
        buttonClicked = true;
        titleScreenMusic.Stop();
        PlayerPrefs.SetString("previousScene", "Menu");
        SceneManager.LoadScene("Options");
    }
}
