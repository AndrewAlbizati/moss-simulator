using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private AudioSource titleScreenMusic;

    void Start()
    {
        PlayerPrefs.DeleteAll();
        titleScreenMusic = gameObject.GetComponent<AudioSource>();
        titleScreenMusic.Play();
    }

    void Update()
    {
        // Loop the title screen music
        if (!titleScreenMusic.isPlaying)
        {
            titleScreenMusic.Play();
        }
    }

    public void NewGame()
    {
        titleScreenMusic.Stop();
        SceneManager.LoadScene("Ohio");
    }

    public void QuitGame()
    {
        titleScreenMusic.Stop();
        Application.Quit();
    }

    public void ShowCredits()
    {
        titleScreenMusic.Stop();
        SceneManager.LoadScene("Credits");
    }
}
