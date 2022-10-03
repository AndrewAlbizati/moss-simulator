using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FootballController : MonoBehaviour
{
    [Header("Scoreboards")]
    public GameObject recordBoard;
    public GameObject betboard;
    public GameObject statusPopup;

    [Header("Background Music")]
    public AudioClip footballMusic;

    [Header("Message Sprites")]
    public Sprite winner;
    public Sprite loser;

    [Header("Audio")]
    public AudioClip winnerAudio;
    public AudioClip loserAudio;

    private AudioSource audioSource;

    private int predictedWins = 8;
    private int predictedLosses = 9;

    private int wins = 0;
    private int losses = 0;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        audioSource = gameObject.GetComponent<AudioSource>();

        UpdateRecordLabel();
        betboard.SetActive(true);
        recordBoard.SetActive(false);
        statusPopup.SetActive(false);
    }

    public void WinClicked()
    {
        if (predictedWins < 17)
        {
            predictedWins++;
            predictedLosses--;
            UpdateRecordLabel();
        }
    }

    public void LoseClicked()
    {
        if (predictedLosses < 17)
        {
            predictedWins--;
            predictedLosses++;
            UpdateRecordLabel();
        }
    }

    private void UpdateRecordLabel()
    {
        betboard.transform.GetChild(0).GetChild(3).GetComponent<TMP_Text>().SetText(predictedWins + " - " + predictedLosses);
    }

    public void DoneClicked()
    {
        StartCoroutine(StartGame());
    }

    public void ContinueClicked()
    {
        SceneManager.LoadScene("Ohio");
    }

    IEnumerator StartGame()
    {
        betboard.SetActive(false);
        recordBoard.SetActive(true);

        audioSource.Stop();
        audioSource.clip = footballMusic;
        audioSource.Play();

        UpdateRecordBoard();

        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < 17; i++)
        {
            if (UnityEngine.Random.value <= 0.318f)
            {
                wins++;
            }
            else
            {
                losses++;
            }

            UpdateRecordBoard();

            yield return new WaitForSeconds(0.5f);
        }


        audioSource.Stop();

        int winDiff = Math.Abs(predictedWins - wins);

        int amountWon = -5;

        switch (winDiff)
        {
            case 0:
                amountWon = Math.Abs(amountWon) * 32;
                break;
            case 1:
                amountWon = Math.Abs(amountWon) * 16;
                break;
            case 2:
                amountWon = Math.Abs(amountWon) * 8;
                break;
            case 3:
                amountWon = Math.Abs(amountWon) * 4;
                break;
            case 4:
                amountWon = Math.Abs(amountWon) * 2;
                break;     
        }

        
        audioSource.volume = 0.3f;

        PlayerPrefs.SetInt("money", PlayerPrefs.GetInt("money") + amountWon);
        statusPopup.transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>().SetText("$" + Math.Abs(amountWon) + " BB");

        if (amountWon > 0)
        {
            statusPopup.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = winner;
            audioSource.PlayOneShot(winnerAudio);
        }
        else
        {
            statusPopup.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = loser;
            audioSource.PlayOneShot(loserAudio);
        }


        statusPopup.SetActive(true);
    }

    public void UpdateRecordBoard()
    {
        recordBoard.transform.GetChild(0).GetChild(2).GetComponent<TMP_Text>().SetText("Prediction: " + predictedWins + " - " + predictedLosses);
        recordBoard.transform.GetChild(0).GetChild(3).GetComponent<TMP_Text>().SetText(wins + " - " + losses);
    }
}

