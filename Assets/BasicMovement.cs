using Assets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement : MonoBehaviour {

    private bool hasMoved = false;
    public bool collided = false;
    public bool recoveringFromCollision = false;
    private float collisionRecoveryTime = 0;
    private float speedAtCollision = 0;
    private float twinkleTime = 0.2f;
    private float twinkleDelta = 0;

    public GameObject point;

    private IPath path;
    public float currentLap = 0;
    public float speed = 10;
    public float lapTime = 10;
    public Transform track;
    private IPath[] lanes;
    public Rigidbody2D car;
    public int startLane = 0;
    private int currentLane = 0;
    public bool isBot = true;
    public bool debugging = false;
    public float previousS;

    private int pointDensity = 500;

    
	// Use this for initialization
	void Start () {
        lanes = new IPath[track.childCount];
        LoadLanes();
        if (!isBot) previousS = 0;

        path = lanes[startLane];
        currentLane = startLane;
    }

    void LoadLanes() { 
        for (int i = 0; i < track.childCount; i++)
        {
            Transform lane = track.GetChild(i);
            ComplexPath lanPath = loadLane(lane);
            lanPath.name = "track" + i;
            lanes.SetValue(lanPath, i);
            LoadLanePoints(lanPath);
        }
    }

    private void LoadLanePoints(ComplexPath lanPath)
    {
        float s = (float)1 / pointDensity;
        float sCount = s;
        for (float i = 0; i < pointDensity; i++)
        {
            Vector2 p = lanPath.GetPos(sCount);
            lanPath.points.Add(new KeyValuePair<float, Vector2>(sCount, p));
            if(debugging) {
                point.transform.position = p;
                Instantiate(point);
            }
            sCount = sCount + s;
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
                Vector2 startPoint = new Vector2(start.transform.position.x, start.transform.position.y);
                //Debug.Log("startPoint - x " + startPoint.X + " y: " + startPoint.Y);

                Transform ch1 = vec.GetChild(1);
                Vector2 ch1Point = new Vector2(ch1.transform.position.x, ch1.transform.position.y);
                //Debug.Log("ch1Point  - x " + ch1Point.X + " y: " + ch1Point.Y);

                Transform ch2 = vec.GetChild(2);
                Vector2 ch2Point = new Vector2(ch2.transform.position.x, ch2.transform.position.y);
                //Debug.Log("ch2Point - x " + ch2Point.X + " y: " + ch2Point.Y);

                Transform end = vec.GetChild(3);
                Vector2 endPoint = new Vector2(end.transform.position.x, end.transform.position.y);
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
                nextPath = new LinearPath(new Vector2(x, y), new Vector2(x2, y2));
            }


            cPath.AddPathSegment(nextPath);
        }
        return cPath;
    }

	void Update ()
    {
        currentLap += Time.deltaTime;
        Vector2 pos = GetNextPos();
        MoveCar(pos);

    }

    public void MoveOut()
    {
        int maxLanes = track.childCount - 1;
        if (currentLane != maxLanes)
        {
            currentLane++;
            path = lanes[currentLane];
            hasMoved = true;
        }
    }

    public void MoveIn()
    {
        if (currentLane > 0)
        {
            currentLane--;
            path = lanes[currentLane];
            hasMoved = true;
        }
    }

    private Vector2 GetNextPos()
    {
        float time = Time.deltaTime;

        if (collided && !recoveringFromCollision)
        {
            collisionRecoveryTime = 0; // SI HAY ALGO RARO BORRAR
            GetComponent<Renderer>().enabled = false;
            speedAtCollision = speed;
            speed /= 3;
            collided = false;
            recoveringFromCollision = true;
        }
        if(recoveringFromCollision){

            twinkleDelta += time;
            if(twinkleDelta > twinkleTime){
                twinkleDelta = 0;
                if(GetComponent<Renderer>().enabled == false)
                    GetComponent<Renderer>().enabled = true;
                else GetComponent<Renderer>().enabled = false;
            }
            collisionRecoveryTime += time;
            if(speed < speedAtCollision){
                speed+= collisionRecoveryTime * collisionRecoveryTime;
            }
            else{
                GetComponent<Renderer>().enabled = true;
                collisionRecoveryTime = 0;
                recoveringFromCollision = false;
            }
        }
        float advance = time * speed;
        float pathLength = path.GetLength();
        float advancedPercentageInFrame = advance / pathLength;
        float percentageOfLap = previousS + advancedPercentageInFrame;
        if (percentageOfLap > 1)
            percentageOfLap -= 1;
        Vector2 pos = path.GetPos(percentageOfLap);
        previousS = percentageOfLap;
        return pos;

      
    }

    private void MoveCar(Vector2 pos)
    {
        Vector3 nextPos = new Vector3(pos.x, pos.y);
        Vector3 diff = nextPos - transform.position;
        diff.Normalize();
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
        transform.position = nextPos;
    }

    private KeyValuePair<float, Vector2> GetClosestPoint(int nextLane)
    {

        List<KeyValuePair<float, Vector2>> points = this.lanes[nextLane].points;
        Vector2 currentPosition = this.transform.position;
        KeyValuePair<float, Vector2> closest = new KeyValuePair<float, Vector2>(-1, new Vector2(100000, 100000));
        foreach (var item in points)
        {
            Vector2 v = item.Value;
            Vector2 currentClosest = closest.Value;
            Vector2 diff = currentPosition - v;
            Vector2 currentDiff = currentPosition - currentClosest;
            if (diff.magnitude < currentDiff.magnitude)
            {
                closest = item;
            }
        }

        return closest;
    }
}
