using Application;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public void GotoMainScene()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void StartPractice()
    {
        CurrentGame.GetInstance().SetPractice();
        SceneManager.LoadScene("SampleScene");
    }
    public void StartRace()
    {
        CurrentGame.GetInstance().SetRace();
        SceneManager.LoadScene("SampleScene");
    }
    public void StartCrash()
    {
        CurrentGame.GetInstance().SetCrash();
        SceneManager.LoadScene("SampleScene");
    }
}