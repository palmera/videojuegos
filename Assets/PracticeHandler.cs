using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Application;

public class PracticeHandler : MonoBehaviour, IScoreHandler
{
    private int score;
    public Text scoreText;
    public SceneSwitcher switcher;
    // public AudioClip MusicClip;
    // public AudioSource MusicSource;
    // Use this for initialization
    public void Start()
    {
        // MusicSource.clip = MusicClip;
        score = 0;
        setText();
    }

    public void passCar()
    {
        score++;
        setText();
        if (score >= 2)
        {
            //MusicSource.Play();
            win();
        }

    }

    public void getHit()
    {
       
            score = 0;
            setText();



    }

    public void makeLap()
    {

    }


    private void setText()
    {
        scoreText.text = "" + score;
    }

    private void win()
    {

    }
    private void loose()
    {
        switcher.GotoMainScene();
        Debug.Log("Perdiste en " + score);
    }
}
