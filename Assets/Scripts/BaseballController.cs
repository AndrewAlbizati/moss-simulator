using System.Collections;
using System.Collections.Generic;
using System;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class BaseballController : MonoBehaviour
{
    public TextAsset textJson;
    public GameObject scoreboard;
    public GameObject betboard;
    public GameObject statusPopup;
    public AudioClip baseballMusic;

    public Sprite winner;
    public Sprite loser;

    [System.Serializable]
    private class Team
    {
        public string name;
        public string city;
        public string abbreviation;
        public float rate;
    }

    [System.Serializable]
    private class TeamList
    {
        public Team[] teams;
    }

    private TeamList teamList = new TeamList();


    private Team awayTeam;
    private Team homeTeam;

    private Team bettedTeam;
    private Team winningTeam;

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
        } while (team1Index == 7);
        awayTeam = teamList.teams[team1Index];


        homeTeam = teamList.teams[7];


        scoreboard.SetActive(false);
        statusPopup.SetActive(false);

        betboard.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMP_Text>().SetText(awayTeam.abbreviation);
        betboard.transform.GetChild(0).GetChild(2).GetChild(0).GetComponent<TMP_Text>().SetText(homeTeam.abbreviation);
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void AwayTeamClicked()
    {
        bettedTeam = awayTeam;
        StartCoroutine(StartGame());
    }

    public void HomeTeamClicked()
    {
        bettedTeam = homeTeam;
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
        audioSource.clip = baseballMusic;
        audioSource.Play();

        scoreboard.GetComponent<Scoreboard>().Reset();
        scoreboard.GetComponent<Scoreboard>().SetAwayTeam(awayTeam.abbreviation);
        scoreboard.GetComponent<Scoreboard>().SetHomeTeam(homeTeam.abbreviation);


        int awayScore;
        int homeScore;

        winningTeam = DetermineWinner(awayTeam, homeTeam);

        do
        {
            awayScore = UnityEngine.Random.Range(0, 9);
            homeScore = UnityEngine.Random.Range(0, 9);
        } while ((awayScore >= homeScore && winningTeam.abbreviation == homeTeam.abbreviation) || (awayScore <= homeScore && winningTeam.abbreviation == awayTeam.abbreviation));

        // Simulate 9 innings
        for (int i = 1; i < 10; i++)
        {
            // Simulate balls and strikes
            for (int j = 0; j < 100; j++)
            {
                DateTime before = DateTime.Now;
                SimulateTick();
                if (scoreboard.GetComponent<Scoreboard>().outs == 3)
                {
                    break;
                }
                DateTime after = DateTime.Now;

                if (after.Subtract(before).Seconds < 0.01f)
                {
                    yield return new WaitForSeconds(0.01f - after.Subtract(before).Seconds);
                }
            }


            // Add away score
            if (UnityEngine.Random.Range(0, 2) == 0 && awayScore > 0)
            {
                scoreboard.GetComponent<Scoreboard>().IncrementAwayScore();
                awayScore--;
            }
            if (i == 9)
            {
                for (int j = 0; j < awayScore; j++)
                {
                    scoreboard.GetComponent<Scoreboard>().IncrementAwayScore();
                }
            }

            scoreboard.GetComponent<Scoreboard>().strikes = 3;
            scoreboard.GetComponent<Scoreboard>().outs = 3;
            scoreboard.GetComponent<Scoreboard>().balls = UnityEngine.Random.Range(0, 4);

            // End game if home team is up at the middle of the 9th
            if (i == 9 && scoreboard.GetComponent<Scoreboard>().homeScore > scoreboard.GetComponent<Scoreboard>().awayScore)
            {
                break;
            }

            scoreboard.GetComponent<Scoreboard>().IncrementInning();
            scoreboard.GetComponent<Scoreboard>().ResetBalls();
            scoreboard.GetComponent<Scoreboard>().ResetStrikes();
            scoreboard.GetComponent<Scoreboard>().ResetOuts();

            scoreboard.GetComponent<Scoreboard>().onFirstBase = false;
            scoreboard.GetComponent<Scoreboard>().onSecondBase = false;
            scoreboard.GetComponent<Scoreboard>().onThirdBase = false;

            // Simulate balls and strikes
            for (int j = 0; j < 100; j++)
            {
                DateTime before = DateTime.Now;
                SimulateTick();
                if (scoreboard.GetComponent<Scoreboard>().outs == 3)
                {
                    break;
                }
                DateTime after = DateTime.Now;

                if (after.Subtract(before).Seconds < 0.01f)
                {
                    yield return new WaitForSeconds(0.01f - after.Subtract(before).Seconds);
                }
            }


            // Add home score
            if (UnityEngine.Random.Range(0, 2) == 0 && homeScore > 0)
            {
                scoreboard.GetComponent<Scoreboard>().IncrementHomeScore();
                homeScore--;
            }
            if (i == 9)
            {
                for (int j = 0; j < homeScore; j++)
                {
                    scoreboard.GetComponent<Scoreboard>().IncrementHomeScore();
                }
            }

            scoreboard.GetComponent<Scoreboard>().strikes = 3;
            scoreboard.GetComponent<Scoreboard>().outs = 3;
            scoreboard.GetComponent<Scoreboard>().balls = UnityEngine.Random.Range(0, 4);
   
            if (i != 9)
            {
                scoreboard.GetComponent<Scoreboard>().IncrementInning();
                scoreboard.GetComponent<Scoreboard>().ResetBalls();
                scoreboard.GetComponent<Scoreboard>().ResetStrikes();
                scoreboard.GetComponent<Scoreboard>().ResetOuts();

                scoreboard.GetComponent<Scoreboard>().onFirstBase = false;
                scoreboard.GetComponent<Scoreboard>().onSecondBase = false;
                scoreboard.GetComponent<Scoreboard>().onThirdBase = false;
            }
        }

        if (winningTeam.abbreviation == bettedTeam.abbreviation)
        {
            statusPopup.transform.GetChild(0).GetChild(0).gameObject.GetComponent<Image>().sprite = winner;
            PlayerPrefs.SetInt("money", PlayerPrefs.GetInt("money") + 10);
        }
        else
        {
            statusPopup.transform.GetChild(0).GetChild(0).gameObject.GetComponent<Image>().sprite = loser;
            PlayerPrefs.SetInt("money", PlayerPrefs.GetInt("money") - 10);
        }

        audioSource.Stop();
        statusPopup.SetActive(true);
    }

    void SimulateTick()
    {
        if (scoreboard.GetComponent<Scoreboard>().balls == 4)
        {
            scoreboard.GetComponent<Scoreboard>().ResetBalls();
        }

        if (scoreboard.GetComponent<Scoreboard>().strikes == 3)
        {
            scoreboard.GetComponent<Scoreboard>().ResetStrikes();
        }

        int rand = UnityEngine.Random.Range(0, 50);
        if (rand <= 5)
        {
            scoreboard.GetComponent<Scoreboard>().IncrementBalls();
        }

        if (rand > 5 && rand <= 10)
        {
            scoreboard.GetComponent<Scoreboard>().IncrementStrikes();
        }

        if (rand > 10 && rand <= 15)
        {
            scoreboard.GetComponent<Scoreboard>().IncrementOuts();
        }



        if (rand == 30)
        {
            scoreboard.GetComponent<Scoreboard>().ToggleFirstBase();
        }

        if (rand == 31)
        {
            scoreboard.GetComponent<Scoreboard>().ToggleSecondBase();
        }

        if (rand == 32)
        {
            scoreboard.GetComponent<Scoreboard>().ToggleThirdBase();
        }
    }


    Team DetermineWinner(Team team1, Team team2)
    {
        float team1Rate = team1.rate;
        float team2Rate = team2.rate;

        if (PlayerPrefs.GetInt("money") <= -20)
        {
            if (team1.abbreviation == bettedTeam.abbreviation)
            {
                team1Rate += 0.3f;
            }
            else
            {
                team2Rate += 0.3f;
            }
        }

        float winPercent = 0.5f + team1Rate - team2Rate;

        return UnityEngine.Random.value > winPercent ? team2 : team1;
    }
}
