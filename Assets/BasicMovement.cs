using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.U2D.TriangleNet.Geometry;

public class BasicMovement : MonoBehaviour {

    private IPath path;
    private float currentLap;
    public float lapTime = 10;
    public Transform track;
    private IPath[] lanes;
    public Rigidbody2D car;
    public int startLane = 0;
    private int currentLane = 0;
    public bool isBot = true;
    
	// Use this for initialization
	void Start () {
        lanes = new IPath[track.childCount];
        loadLanes();

        this.path = lanes[startLane];
        currentLane = startLane;
        this.currentLap = 0;
    }

    void loadLanes() { 
        for (int i = 0; i < track.childCount; i++)
        {
            Transform lane = track.GetChild(i);
            ComplexPath lanPath = loadLane(lane);
            lanPath.name = "track" + i;
            lanes.SetValue(lanPath, i);
        }
    }

    ComplexPath loadLane(Transform lane) {
        ComplexPath cPath = new ComplexPath();
        for (int i = 0; i < lane.childCount; i++)
        {
            Transform vec = lane.GetChild(i);
            IPath nextPath;
            int next = i + 1;
            if (next == lane.childCount)
            {
                next = 0;
            }

            if (vec.childCount > 1)
            {
                // bezier path
                Transform start = vec.GetChild(0);
                Point startPoint = new Point(start.transform.position.x, start.transform.position.y);
                //Debug.Log("startPoint - x " + startPoint.X + " y: " + startPoint.Y);

                Transform ch1 = vec.GetChild(1);
                Point ch1Point = new Point(ch1.transform.position.x, ch1.transform.position.y);
                //Debug.Log("ch1Point  - x " + ch1Point.X + " y: " + ch1Point.Y);

                Transform ch2 = vec.GetChild(2);
                Point ch2Point = new Point(ch2.transform.position.x, ch2.transform.position.y);
                //Debug.Log("ch2Point - x " + ch2Point.X + " y: " + ch2Point.Y);

                Transform end = vec.GetChild(3);
                Point endPoint = new Point(end.transform.position.x, end.transform.position.y);
                //Debug.Log("endPoint - x " + endPoint.X + " y: " + endPoint.Y);

                nextPath = new BezierPath(startPoint, ch1Point, ch2Point, endPoint);
            } else {
                //lineal path
                Transform vec2 = lane.GetChild(next);
                if (vec2.childCount > 1)
                {
                    vec2 = vec2.GetChild(0);
                }
                float x = vec.transform.position.x;
                float y = vec.transform.position.y;
                float x2 = vec2.transform.position.x;
                float y2 = vec2.transform.position.y;
                nextPath = new LinearPath(new Point(x, y), new Point(x2, y2));
            }


            cPath.AddPathSegment(nextPath);
        }
        return cPath;
    }

    // Update is called once per frame

    private bool moveIn() {
        //Debug.Log("move in: ");
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            //Debug.Log("move in touch");
            Touch touch = Input.GetTouch(0);
            Vector2 touchPosition = touch.position;
            return touchPosition.x < Screen.width / 2;
        }

        return Input.GetKeyDown(KeyCode.LeftArrow);
    }
    private bool moveOut() {
        //Debug.Log("move out: --------------------------------");
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            //Debug.Log("move out touch");
            Touch touch = Input.GetTouch(0);
            Vector2 touchPosition = touch.position;
            return touchPosition.x > Screen.width / 2;
        }
        return Input.GetKeyDown(KeyCode.RightArrow);
    }

	void Update () {
        if (!isBot)
        {
            if (moveIn())
            {
                if (currentLane > 0){
                    currentLane--;

                }
            }
            else if (moveOut())
            {

                int maxLanes = track.childCount -1;
                if (currentLane != maxLanes)
                    currentLane++;
            }

            this.path = lanes[currentLane];
        }



        Point pos;
        currentLap += Time.deltaTime;
        float s;
        if (currentLap < lapTime)
        {
            s = currentLap / lapTime;
            pos = this.path.GetPos(s);
        }
        else
        {
            currentLap = currentLap - lapTime;
            s = currentLap / lapTime;
            pos = this.path.GetPos(s);
            if (!isBot)
            {
                lapTime = lapTime * (float)0.9;
            }
        }
        if (pos.X == 0 && pos.Y == 0)
        {
            Debug.Log("path: " + this.path.name);
            Debug.Log("s: " + s);
        }
        Debug.Log(this.transform.rotation);
        Vector3 nextPos = new Vector3((float)pos.X, (float)pos.Y);

        Vector3 diff = nextPos - transform.position;
        diff.Normalize();

        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        this.transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
        this.transform.position = nextPos;

    }
}
