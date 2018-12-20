using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreHandler : MonoBehaviour
{
    private int score;
    public Text scoreText;

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
        score = 0;
        setText();

    }


    private void setText(){
        scoreText.text = "" + score;
    }

    private void win()
    {

    }
}
