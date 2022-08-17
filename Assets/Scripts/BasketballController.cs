using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BasketballController : MonoBehaviour
{
    public TextAsset textJson;
    public GameObject scoreboard;
    public GameObject betboard;
    public GameObject statusPopup;
    public AudioClip basketballMusic;

    public Sprite winner;
    public Sprite loser;

    [System.Serializable]
    private class Team
    {
        public string name;
        public string city;
        public string abbreviation;
        public float rate;
        public float ppg;
        public float[] color;
    }

    [System.Serializable]
    private class TeamList
    {
        public Team[] teams;
    }

    private TeamList teamList = new TeamList();


    private Team awayTeam;
    private Team homeTeam;

    private int awayScore;
    private int awayPPQ;

    private int homeScore;
    private int homePPQ;

    private int totalScore;

    private float overUnder;

    private bool bettedOver;

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        audioSource = gameObject.GetComponent<AudioSource>();

        teamList = JsonUtility.FromJson<TeamList>(textJson.text);

        int team1Index;
        do
        {
            team1Index = UnityEngine.Random.Range(0, teamList.teams.Length);
        } while (team1Index == 5);
        awayTeam = teamList.teams[team1Index];


        homeTeam = teamList.teams[5];

        overUnder = Mathf.FloorToInt(awayTeam.ppg + homeTeam.ppg) - 5.5f;

        scoreboard.SetActive(false);
        statusPopup.SetActive(false);

        betboard.transform.GetChild(0).GetChild(5).GetComponent<TMP_Text>().SetText(awayTeam.abbreviation + " vs " + homeTeam.abbreviation);
        betboard.transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>().SetText(overUnder.ToString());
    }

    public void OverClicked()
    {
        bettedOver = true;
        StartCoroutine(StartGame());
    }

    public void UnderClicked()
    {
        bettedOver = false;
        StartCoroutine(StartGame());
    }

    public void ContinueClicked()
    {
        SceneManager.LoadScene("Ohio");
    }

    IEnumerator StartGame()
    {
        betboard.SetActive(false);
        scoreboard.SetActive(true);

        audioSource.Stop();
        audioSource.clip = basketballMusic;
        audioSource.Play();

        scoreboard.GetComponent<BasketballScoreboard>().awayTeam = awayTeam.abbreviation;
        scoreboard.GetComponent<BasketballScoreboard>().homeTeam = homeTeam.abbreviation;

        scoreboard.GetComponent<BasketballScoreboard>().awayTeamColor = new Color(awayTeam.color[0] / 255, awayTeam.color[1] / 255, awayTeam.color[2] / 255, 1.0f);
        scoreboard.GetComponent<BasketballScoreboard>().homeTeamColor = new Color(homeTeam.color[0] / 255, homeTeam.color[1] / 255, homeTeam.color[2] / 255, 1.0f);

        scoreboard.GetComponent<BasketballScoreboard>().overUnder = overUnder;

        Team winningTeam = DetermineWinner(awayTeam, homeTeam);

        do
        {
                
            awayScore = Mathf.FloorToInt(awayTeam.ppg) + UnityEngine.Random.Range(-20, 20);
            homeScore = Mathf.FloorToInt(homeTeam.ppg) + UnityEngine.Random.Range(-20, 20);
        } while ((awayScore >= homeScore && winningTeam.abbreviation == homeTeam.abbreviation) || (awayScore <= homeScore && winningTeam.abbreviation == awayTeam.abbreviation));
        
        awayPPQ = Mathf.CeilToInt(awayScore / 4.0f);
        homePPQ = Mathf.CeilToInt(homeScore / 4.0f);

        totalScore = awayScore + homeScore;

        // Simulate 4 quarter
        for (int i = 1; i <= 4; i++)
        {
            DateTime beforeFirst = DateTime.Now;

            scoreboard.GetComponent<BasketballScoreboard>().timeMinutes = 12;
            scoreboard.GetComponent<BasketballScoreboard>().timeSeconds = 00;

            // Simulate quarter
            for (int j = 0; j < 720; j++)
            {
                SimulateTick();
  
                if (DateTime.Now.Subtract(beforeFirst).Seconds > 4f)
                {
                    scoreboard.GetComponent<BasketballScoreboard>().timeMinutes = 0;
                    scoreboard.GetComponent<BasketballScoreboard>().timeSeconds = 0;
                    break;
                }

                yield return new WaitForSeconds(0.005f);
            }
            if (scoreboard.GetComponent<BasketballScoreboard>().quarter < 4)
            {
                scoreboard.GetComponent<BasketballScoreboard>().quarter++;
            }
        }

        totalScore = scoreboard.GetComponent<BasketballScoreboard>().awayScore + scoreboard.GetComponent<BasketballScoreboard>().homeScore;

        if ((bettedOver && totalScore > overUnder) || (!bettedOver && totalScore < overUnder))
        {
            statusPopup.transform.GetChild(0).GetChild(0).gameObject.GetComponent<Image>().sprite = winner;
            PlayerPrefs.SetInt("money", PlayerPrefs.GetInt("money") + 50);
        }
        else
        {
            statusPopup.transform.GetChild(0).GetChild(0).gameObject.GetComponent<Image>().sprite = loser;
            PlayerPrefs.SetInt("money", PlayerPrefs.GetInt("money") - 50);
        }

        audioSource.Stop();
        statusPopup.SetActive(true);
    }

    void SimulateTick()
    {
        if (scoreboard.GetComponent<BasketballScoreboard>().timeMinutes <= 0 && scoreboard.GetComponent<BasketballScoreboard>().timeMinutes <= 0)
        {
            scoreboard.GetComponent<BasketballScoreboard>().timeMinutes = 0;
            scoreboard.GetComponent<BasketballScoreboard>().timeSeconds = 0;
            scoreboard.GetComponent<BasketballScoreboard>().shotClockSeconds = 0;
            return;
        }

        // Time
        scoreboard.GetComponent<BasketballScoreboard>().timeSeconds--;
        if (scoreboard.GetComponent<BasketballScoreboard>().timeSeconds <= 0)
        {
            scoreboard.GetComponent<BasketballScoreboard>().timeMinutes--;

            if (scoreboard.GetComponent<BasketballScoreboard>().timeMinutes != 0)
            {
                scoreboard.GetComponent<BasketballScoreboard>().timeSeconds = 60;
            }
        }

        // Shot clock
        scoreboard.GetComponent<BasketballScoreboard>().shotClockSeconds--;
        
        if (scoreboard.GetComponent<BasketballScoreboard>().shotClockSeconds < 0)
        {
            scoreboard.GetComponent<BasketballScoreboard>().shotClockSeconds = 24;
        }


        int rand = UnityEngine.Random.Range(0, 720);
        if (scoreboard.GetComponent<BasketballScoreboard>().awayScore < awayPPQ * scoreboard.GetComponent<BasketballScoreboard>().quarter && rand < 14)
        {
            if (UnityEngine.Random.value <= 0.392)
            {
                scoreboard.GetComponent<BasketballScoreboard>().awayScore += 3;
            }
            else
            {
                scoreboard.GetComponent<BasketballScoreboard>().awayScore += 2;
            }
        }
        else if (scoreboard.GetComponent<BasketballScoreboard>().homeScore < homePPQ * scoreboard.GetComponent<BasketballScoreboard>().quarter && rand >= 14 && rand < 28)
        {
            if (UnityEngine.Random.value <= 0.392)
            {
                scoreboard.GetComponent<BasketballScoreboard>().homeScore += 3;
            }
            else
            {
                scoreboard.GetComponent<BasketballScoreboard>().homeScore += 2;
            }
        }
    }


    Team DetermineWinner(Team team1, Team team2)
    {
        float team1Rate = team1.rate;
        float team2Rate = team2.rate;

        float winPercent = 0.5f + team1Rate - team2Rate;

        return UnityEngine.Random.value > winPercent ? team2 : team1;
    }
}

