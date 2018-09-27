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
    
	// Use this for initialization
	void Start () {
        lanes = new IPath[track.childCount];
        loadLanes();

        this.path = lanes[startLane];
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
            int next = i + 1;
            if (next == lane.childCount)
            {
                next = 0;
            }
            Transform vec2 = lane.GetChild(next);
            float x = vec.transform.position.x;
            float y = vec.transform.position.y;
            float x2 = vec2.transform.position.x;
            float y2 = vec2.transform.position.y;
            IPath lPath = new LinearPath(new Point(x, y), new Point(x2, y2));
            cPath.AddPathSegment(lPath);
        }
        return cPath;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            // Get movement of the finger since last frame
            Vector2 touchDeltaPosition = Input.GetTouch(0).position;
            Debug.Log("touch position: " + touchDeltaPosition);
            currentLane = currentLane == 0 ? 1 : 0;
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
            //s = 0;
            currentLap = currentLap - lapTime;
            s = currentLap / lapTime;
            pos = this.path.GetPos(s);
            //currentLap = 0;
            lapTime = lapTime * (float)0.9;
        }
        // Vector2 lookAt = new Vector2(car.ve);
        //Debug.Log("look at: " + car.velocity);
        //this.transform.LookAt(car.velocity);
        //Vector3 dx = new Vector3(0, this.transform.position.x - (float)pos.X, this.transform.position.y - (float)pos.Y);
        //Debug.Log("dx.x: " + dx.x + "dx.y: " + dx.y + "dx.z: " + dx.z);
        //float tan = dx.y / dx.z;
        //Debug.Log("tan: " + tan);
        //float angle = Mathf.Atan(tan);
        //Debug.Log("tan angle: " + angle);
        //float angle2 = Mathf.Atan2(dx.z, dx.y);
        
        //Point nextPoint = path.GetCurrentEndPoint();
        //Debug.Log("nextPoint: " + nextPoint);
        //Vector3 nextTarget = new Vector3((float)nextPoint.X+90, (float)nextPoint.Y);
        //this.transform.LookAt(nextTarget);
        if (pos.X == 0 && pos.Y == 0)
        {
            Debug.Log("path: " + this.path.name);
            Debug.Log("s: " + s);
        }
        this.transform.position = new Vector3((float)pos.X, (float)pos.Y);
        //this.transform.Rotate(0, 0, angle);

    }
}
