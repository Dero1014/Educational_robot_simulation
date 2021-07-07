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

    //Simpelton start
    [HideInInspector]
    public static Robot currentRobot;
    public void Awake()
    {
        currentRobot = this;
    }
    //Simpeltoon end

    void Start()
    {
        // Set a starting position for robot
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

            Robot_Segments[i].startPoint = (i == 0) ? Vector2.zero : Robot_Segments[i - 1].endPoint;

            Vector2 segUpLenght = angleVector * Robot_Segments[i].a;

            Robot_Segments[i].endPoint = Robot_Segments[i].startPoint + segUpLenght;

            Robot_Segments[i].currentLenght = (Robot_Segments[i].endPoint - Robot_Segments[i].startPoint).magnitude;

        }
    }

    // This function recives the calculated values from Calculation.cs and applies the values to the segments
    // the only values that matter are the angles since they will be used to move the segments
    // 1) Save the angles in an array 
    // 2) Apply the angles to each segment 
    // 3) Give the global angle to the segments
    public void NewSegmentValues(float pwx, float pwy, float v1, float v2, float v3)
    {
        Conversion c = new Conversion(); // Lib

        float[] localAngles = { v1, v2, v3 };

        int rLength = Robot_Segments.Length;

        // Add info to segments for local and global variables
        for (int i = 0; i < rLength; i++)
        {
            Robot_Segments[i].theta  = localAngles[i];
            Robot_Segments[i].gAngle = c.GlobalAngle(i, localAngles);
        }

        // connect the segments
        ConnectSegments();
    }

}


