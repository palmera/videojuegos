using System;
namespace Application
{
    public class CurrentGame
    {
       
        private int currentMode;
        private static CurrentGame instance;
        public CurrentGame()
        {
            currentMode = 0;
        } 

        public static CurrentGame GetInstance(){
            if(instance == null){
                instance = new CurrentGame();
            }
            return instance;
        }

        public void SetPractice()
        {
            currentMode = 0;
        }
        public Boolean GetPractice()
        {
            return currentMode == 0;
        }

        public void SetRace()
        {
            currentMode = 1;
        }
        public Boolean GetRace()
        {
            return currentMode == 1;
        }

        public void SetCrash()
        {
            currentMode = 2;
        }
        public Boolean GetCrash()
        {
            return currentMode == 2;
        }

        private bool lapDone = false;
        public void JustMadeLap(){
            lapDone = true;
        }
        public void AddTimeForLap(){
            lapDone = false;
        }
        public bool LapForTime(){
            return lapDone;
        }
    }
}
