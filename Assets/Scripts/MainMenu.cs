using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string newGameScene;
    public AudioSource titleScreenMusic;


    void Start()
    {
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
        SceneManager.LoadScene(newGameScene);
    }

    public void QuitGame()
    {
        titleScreenMusic.Stop();
        Application.Quit();
    }
}
