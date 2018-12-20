using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    private BasicMovement bm;
    public AudioClip crashSound;
    public AudioClip tiresSound;

    private AudioSource source;
    public ScoreHandler scoreHandler;
    // Use this for initialization
    void Start () {
        bm = GetComponent<BasicMovement>();
        bm.isUser = true;
        source = GetComponent<AudioSource>();
    }

    private bool movedIn()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPosition = touch.position;
            return touchPosition.x < Screen.width / 2;
        }

        return Input.GetKeyDown(KeyCode.LeftArrow);
    }
    private bool movedOut()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPosition = touch.position;
            return touchPosition.x > Screen.width / 2;
        }
        return Input.GetKeyDown(KeyCode.RightArrow);
    }

    private bool moved()
    {
        return movedIn() || movedOut();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("type" + collision.GetType());
        Debug.Log("trigger in bm!!" + collision.ToString());
        source.PlayOneShot(crashSound);
        if (!bm.collided){
            scoreHandler.getHit();
        }

        //  bm.speed = bm.speed / 2;
        bm.collided = true;
    }
   
    // Update is called once per frame
    void Update () {

        if (moved())
        {
            source.PlayOneShot(tiresSound);
            if (movedIn())
            {
                bm.MoveIn();
            }
            else if (movedOut())
            {
                bm.MoveOut();
            }
        }
    }
}
