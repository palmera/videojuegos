using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Application;

public class ScoreHandler : MonoBehaviour
{
    private int score;
    public Text scoreText;
    public SceneSwitcher switcher;
    // public AudioClip MusicClip;
    // public AudioSource MusicSource;
    // Use this for initialization
    void Start () {
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

    public void getHit(){
        if(CurrentGame.GetInstance().GetCrash()){
            loose();
        }
        else{
            score = 0;
            setText();
        }


    }


    private void setText(){
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
