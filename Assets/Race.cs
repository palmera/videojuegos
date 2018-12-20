using UnityEngine;
using System;
using System.Collections;

public class Race : MonoBehaviour
{
    public Transform cars;
    public ScoreHandler scoreHandler;
    public AudioClip pingSound;

    private AudioSource source;

    private int previousCarsInFront = 0;
    private int previousCarsInBack = 0;
    // Use this for initialization
    void Start()
    {
        Debug.Log("race");
        source = GetComponent<AudioSource>();
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
