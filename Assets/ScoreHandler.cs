using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreHandler : MonoBehaviour
{
    private int score;
    public Text scoreText;
    // Use this for initialization
    void Start()
    {
        score = 0;
        setText();
    }

    public void passCar()
    {
        score++;
        setText();
        if (score >= 10)
        {
            loose();
        }

    }

    public void getHit(){
        score--;
        setText();
        if(score<=5){
            loose();
        }

    }


    private void setText(){
        scoreText.text = "" + score;
    }

    private void win()
    {

    }

    private void loose(){

    }
}
