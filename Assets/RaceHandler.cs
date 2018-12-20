using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Application;
using System;

public class RaceHandler : MonoBehaviour, IScoreHandler
{
    public int numberOfLapsMade = 0;
    public float [] timePerLaps = new float[]{5.0f,4.5f, 4.0f,3.5f, 3.3f, 3.0f}; 
    private float timeRemaining;
    public Text scoreText;
    public SceneSwitcher switcher;
    // public AudioClip MusicClip;
    // public AudioSource MusicSource;
    // Use this for initialization
    public void Start()
    {
        // MusicSource.clip = MusicClip;
        timeRemaining = 15.0f;
        setText();
    }

    public void passCar()
    {

    }

    public void getHit()
    {

    }

    public void makeLap()
    {
        numberOfLapsMade++;
        if (numberOfLapsMade > 5)
            numberOfLapsMade = 5;
        timeRemaining += timePerLaps[numberOfLapsMade];
    }


    private void setText()
    {
        String textToShow = timeRemaining.ToString("0.00");
        scoreText.text = textToShow;
    }

    private void win()
    {

    }
    private void loose()
    {
        switcher.GotoMainScene();
    }

    void Update(){
        if(CurrentGame.GetInstance().LapForTime()){
            CurrentGame.GetInstance().AddTimeForLap();
            makeLap();
        }
        timeRemaining -= Time.deltaTime;
        if (timeRemaining < 0)
            loose();
        setText();
    }
    private float Truncate(float value, int digits)
    {
        double mult = Math.Pow(10.0, digits);
        double result = Math.Truncate(mult * value) / mult;
        return (float)result;
    }
}
