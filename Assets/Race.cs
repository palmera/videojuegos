using UnityEngine;
using System;
using System.Collections;
using Application;

public class Race : MonoBehaviour
{
    public Transform cars;
    public IScoreHandler scoreHandler;
    public PracticeHandler practiceHandler;
    public CrashHandler crashHandler;
    public RaceHandler raceHandler;
    public AudioClip pingSound;

    private AudioSource source;

    private int previousCarsInFront = 0;
    private int previousCarsInBack = 0;
    // Use this for initialization
    void Start()
    {
        if (CurrentGame.GetInstance().GetPractice())
        {
            scoreHandler = practiceHandler;
        }
        else if (CurrentGame.GetInstance().GetCrash())
        {
            scoreHandler = crashHandler;
        }
        else scoreHandler = raceHandler;
        Debug.Log("race");
<<<<<<< HEAD
        //practice ya esta
        //chocas y perdes
        //time trial

=======
        source = GetComponent<AudioSource>();
>>>>>>> 697d2e2771d84a5626dcf317a27df39230085927
    }

    // Update is called once per frame
    void Update()
    {
        int carsInFront = 0;
        int carsInBack = 0;
        foreach (Transform car in cars)
        {

            BasicMovement userBm = car.GetComponent<BasicMovement>();
            if(userBm.isUser){
                foreach (Transform secondCar in cars)
                {
                    BasicMovement enemyBm = secondCar.GetComponent<BasicMovement>();
                    if (!enemyBm.isUser){

                        if (userBm.distanceMade > enemyBm.distanceMade){
                            float difference = (float) Math.Floor(userBm.distanceMade - enemyBm.distanceMade);
                            carsInBack++;
                            for (int i = 0; i < difference; i++)
                            {
                                carsInBack++;
                            }
                        }
                        else carsInFront++;
                    }
                }
                if(carsInBack > previousCarsInBack)
                {
                    source.PlayOneShot(pingSound);
                    int carsPassed = carsInBack - previousCarsInBack;
                    for (int i = 0; i < carsPassed;i++){
                        scoreHandler.passCar();
                    }
                    userBm.speed +=2 ;
                }
                previousCarsInBack = carsInBack;
                previousCarsInFront = carsInFront;
            }
        }
    }
}
