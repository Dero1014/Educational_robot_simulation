using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using CustomLib;

[System.Serializable]
public struct Segment
{
    public Vector2 startPoint;
    public Vector2 endPoint;
    public float a;             // segment length
    public float d;             // distance between joint
    public float theta;         // local angle
    public float gAngle;        // global angle
    public float alpha;         // twist angle
    public float currentLenght; // for debuging length
};


public class Robot : MonoBehaviour
{
    [Header("Settings")]
    public float SphereSize;
    [Space(10)]
    public Segment[] Robot_Segments;

    [HideInInspector]
    public static Robot currentRobot;

    public void Awake()
    {
        currentRobot = this;
    }

    void Start()
    {
        // Set starting position for robot
        ConnectSegments(true);
    }

    // This function connects the segments with the new found values
    // 1) Start from the first segment
    // 2) Create a normal vector in the path of the new global angle
    // 3) Push the endPoint of the segment on the line of the new global angle
    // 4) Set start point of the next segment on the endPoint of the previous one
    void ConnectSegments(bool upWardCondition = false)
    {
        Conversion c = new Conversion();
        int rLength = Robot_Segments.Length;

        for (int i = 0; i < rLength; i++)
        {
            Vector2 angleVector = upWardCondition ? Vector2.up : new Vector2(Mathf.Cos(c.AngleToRad(Robot_Segments[i].gAngle)), Mathf.Sin(c.AngleToRad(Robot_Segments[i].gAngle)));
         // Vector2 angleVector = upWardCondition ? Vector2.up : new Vector2(Mathf.Cos(Robot_Segments[i].gAngle * (Mathf.PI / 180)), Mathf.Sin(Robot_Segments[i].gAngle * (Mathf.PI / 180)));

            if (i == 0)
            {

                Robot_Segments[i].startPoint = Vector2.zero;

                Vector2 segUpLenght = angleVector * Robot_Segments[i].a;

                Robot_Segments[i].endPoint = Robot_Segments[i].startPoint + segUpLenght;
            }
            else
            {
                Robot_Segments[i].startPoint = Robot_Segments[i - 1].endPoint;

                Vector2 segUpLenght = angleVector * Robot_Segments[i].a;

                Robot_Segments[i].endPoint = Robot_Segments[i].startPoint + segUpLenght;
            }

            Robot_Segments[i].currentLenght = (Robot_Segments[i].endPoint - Robot_Segments[i].startPoint).magnitude;

        }
    }


    public void NewSegmentValues(float pwx, float pwy, float v1, float v2, float v3)
    {
        Conversion c = new Conversion();

        int rLength = Robot_Segments.Length;
        float[] localAngles = { v1, v2, v3 };

        Robot_Segments[2].endPoint.x = pwx;
        Robot_Segments[2].endPoint.y = pwy;

        //add info to local and global variables

        for (int i = 0; i < rLength; i++)
        {
            Robot_Segments[i].theta = localAngles[i];
            Robot_Segments[i].gAngle = v1 + v2 * c.BoolToInt((i > 0)) + v3 * c.BoolToInt((i > 1));
        }

        // connect the segments
        ConnectSegments();
    }

}


