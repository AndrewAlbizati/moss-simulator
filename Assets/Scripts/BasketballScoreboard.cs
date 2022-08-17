using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class BasketballScoreboard : MonoBehaviour
{
    [Header("Team Settings")]
    public int awayScore = 0;
    public int homeScore = 0;

    public string awayTeam = "LAC";
    public Color awayTeamColor;

    public string homeTeam = "CLE";
    public Color homeTeamColor;

    [Header("Quarter/Time/Shotclock")]
    public int quarter = 1;
    public int timeMinutes = 12;
    public int timeSeconds = 0;
    public int shotClockSeconds = 24;

    [Header("Over/Under")]
    public float overUnder;

    private GameObject awayScoreLabel;
    private GameObject homeScoreLabel;

    private GameObject awayTeamLabel;
    private GameObject homeTeamLabel;

    private GameObject awayTeamBox;
    private GameObject homeTeamBox;

    private GameObject quarterLabel;
    private GameObject timeLabel;
    private GameObject shotClock;

    private GameObject overUnderLabel;

    // Start is called before the first frame update
    void Start()
    {
        GameObject canvas = gameObject.transform.GetChild(0).gameObject;
        awayScoreLabel = canvas.transform.GetChild(1).GetChild(2).gameObject;
        homeScoreLabel = canvas.transform.GetChild(2).GetChild(2).gameObject;

        awayTeamLabel = canvas.transform.GetChild(1).GetChild(1).gameObject;
        homeTeamLabel = canvas.transform.GetChild(2).GetChild(1).gameObject;

        awayTeamBox = canvas.transform.GetChild(1).GetChild(0).gameObject;
        homeTeamBox = canvas.transform.GetChild(2).GetChild(0).gameObject;


        quarterLabel = canvas.transform.GetChild(3).gameObject;
        timeLabel = canvas.transform.GetChild(4).gameObject;
        shotClock = canvas.transform.GetChild(5).gameObject;
        overUnderLabel = canvas.transform.GetChild(6).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        awayScoreLabel.GetComponent<TMP_Text>().SetText(awayScore.ToString());
        homeScoreLabel.GetComponent<TMP_Text>().SetText(homeScore.ToString());

        awayTeamLabel.GetComponent<TMP_Text>().SetText(awayTeam);
        homeTeamLabel.GetComponent<TMP_Text>().SetText(homeTeam);

        awayTeamBox.GetComponent<Image>().color = awayTeamColor;
        homeTeamBox.GetComponent<Image>().color = homeTeamColor;

        quarterLabel.GetComponent<TMP_Text>().SetText(quarter + (quarter == 1 ? "ST" : quarter == 2 ? "ND" : quarter == 3 ? "RD" : "TH"));


        string time = timeMinutes == 0 ? "00" : timeMinutes < 10 ? timeMinutes.ToString().Replace("0", "") : timeMinutes.ToString();
        time += ":";
        time += timeSeconds == 0 ? "00" : timeSeconds < 10 ? "0" + timeSeconds : timeSeconds;

        timeLabel.GetComponent<TMP_Text>().SetText(time);
        shotClock.GetComponent<TMP_Text>().SetText(shotClockSeconds.ToString());

        overUnderLabel.GetComponent<TMP_Text>().SetText("O/U: " + overUnder + ", C: " + (awayScore + homeScore));
    }
}
