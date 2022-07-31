using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Scoreboard : MonoBehaviour
{
    public int awayScore = 0;
    public int homeScore = 0;
    public string awayTeam = "SD";
    public string homeTeam = "CLE";

    public int balls = 0;
    public int strikes = 0;
    public int outs = 0;

    public int inning = 1;
    public bool isTop = true;

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
        GameObject canvas = gameObject.transform.GetChild(1).gameObject;
        awayScoreLabel = canvas.transform.GetChild(0).gameObject;
        homeScoreLabel = canvas.transform.GetChild(1).gameObject;
        awayTeamLabel = canvas.transform.GetChild(2).gameObject;
        homeTeamLabel = canvas.transform.GetChild(3).gameObject;
        strikesLabel = canvas.transform.GetChild(4).gameObject;
        outsLabel = canvas.transform.GetChild(5).gameObject;
        inningLabel = canvas.transform.GetChild(6).gameObject;


        GameObject bases = gameObject.transform.GetChild(2).gameObject;
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
    }
}
