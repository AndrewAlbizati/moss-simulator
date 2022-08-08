using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class Scoreboard : MonoBehaviour
{
    [Header("Team Settings")]
    public int awayScore = 0;
    public int homeScore = 0;
    public string awayTeam = "SD";
    public string homeTeam = "CLE";

    [Header("Balls/Strikes/Outs")]
    public int balls = 0;
    public int strikes = 0;
    public int outs = 0;

    [Header("Inning Settings")]
    public int inning = 1;
    public bool isTop = true;

    [Header("Base Settings")]
    public bool onFirstBase = false;
    public bool onSecondBase = false;
    public bool onThirdBase = false;

    private GameObject awayScoreLabel;
    private GameObject homeScoreLabel;
    private GameObject awayTeamLabel;
    private GameObject homeTeamLabel;
    private GameObject strikesLabel;
    private GameObject outsLabel;
    private GameObject inningLabel;

    private GameObject firstBase;
    private GameObject secondBase;
    private GameObject thirdBase;

    // Start is called before the first frame update
    void Start()
    {
        GameObject canvas = gameObject.transform.GetChild(0).gameObject;
        awayScoreLabel = canvas.transform.GetChild(1).gameObject;
        homeScoreLabel = canvas.transform.GetChild(2).gameObject;
        awayTeamLabel = canvas.transform.GetChild(3).gameObject;
        homeTeamLabel = canvas.transform.GetChild(4).gameObject;
        strikesLabel = canvas.transform.GetChild(5).gameObject;
        outsLabel = canvas.transform.GetChild(6).gameObject;
        inningLabel = canvas.transform.GetChild(7).gameObject;


        GameObject bases = canvas.transform.GetChild(10).gameObject;
        firstBase = bases.transform.GetChild(0).gameObject;
        secondBase = bases.transform.GetChild(1).gameObject;
        thirdBase = bases.transform.GetChild(2).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        awayScoreLabel.GetComponent<TMP_Text>().SetText(awayScore.ToString());
        homeScoreLabel.GetComponent<TMP_Text>().SetText(homeScore.ToString());

        awayTeamLabel.GetComponent<TMP_Text>().SetText(awayTeam);
        homeTeamLabel.GetComponent<TMP_Text>().SetText(homeTeam);

        strikesLabel.GetComponent<TMP_Text>().SetText(balls + "-" + strikes);

        outsLabel.GetComponent<TMP_Text>().SetText(outs + " OUT" + (outs == 1 ? "" : "S"));

        string ordinal;
        switch (inning)
        {
            case 1:
                ordinal = "st";
                break;
            case 2:
                ordinal = "nd";
                break;
            case 3:
                ordinal = "rd";
                break;

            default:
                ordinal = "th";
                break;
        }
        inningLabel.GetComponent<TMP_Text>().SetText((isTop ? "▲" : "▼") + inning + ordinal);

        firstBase.GetComponent<Image>().color = onFirstBase ? new Color(255, 255, 0) : new Color(255, 255, 255);
        secondBase.GetComponent<Image>().color = onSecondBase ? new Color(255, 255, 0) : new Color(255, 255, 255);
        thirdBase.GetComponent<Image>().color = onThirdBase ? new Color(255, 255, 0) : new Color(255, 255, 255);
    }

    public void IncrementAwayScore()
    {
        awayScore++;
    }

    public void IncrementHomeScore()
    {
        homeScore++;
    }

    public void SetAwayTeam(string name)
    {
        awayTeam = name;
    }

    public void SetHomeTeam(string name)
    {
        homeTeam = name;
    }

    public void IncrementBalls()
    {
        if (balls < 4)
        {
            balls++;
        }
    }

    public void ResetBalls()
    {
        balls = 0;
    }

    public void IncrementStrikes()
    {
        if (strikes < 3)
        {
            strikes++;
        }
    }

    public void ResetStrikes()
    {
        strikes = 0;
    }

    public void IncrementOuts()
    {
        if (outs < 3)
        {
            outs++;
        }
    }

    public void ResetOuts()
    {
        outs = 0;
    }

    public void IncrementInning()
    {
        if (isTop)
        {
            isTop = false;
        }
        else
        {
            inning++;
            isTop = true;
        }
    }

    public void ToggleFirstBase()
    {
        onFirstBase = !onFirstBase;
    }

    public void ToggleSecondBase()
    {
        onSecondBase = !onSecondBase;
    }

    public void ToggleThirdBase()
    {
        onThirdBase = !onThirdBase;
    }

    public void Reset()
    {
        awayScore = 0;
        homeScore = 0;
        awayTeam = "SD";
        homeTeam = "CLE";

        balls = 0;
        strikes = 0;
        outs = 0;

        inning = 1;
        isTop = true;

        onFirstBase = false;
        onSecondBase = false;
        onThirdBase = false;
    }
}
