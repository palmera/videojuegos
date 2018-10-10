using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotMovement : MonoBehaviour {

    private bool moveEnabled = true;
    private BasicMovement bm;
    private int counterIn;
    private int counterOut;
    // Use this for initialization
    void Start () {
        bm = GetComponent<BasicMovement>();
        counterIn = 0;
        counterOut = 0;
        bm.currentLap = Random.Range(0, bm.lapTime);
    }

    IEnumerator Example()
    {
        yield return new WaitForSecondsRealtime(Random.Range(0, 6f));
        moveEnabled = true;
        if (Random.Range(-10f, 10f) > 5 || counterOut - counterIn > 3)
        {
            bm.MoveIn();
            counterIn++;
        }
        else {
            bm.MoveOut();
            counterOut++;
        }

    }

    // Update is called once per frame
    void Update () {
        if (moveEnabled)
        {
            moveEnabled = false;
            StartCoroutine(Example());
        }
    }
}
