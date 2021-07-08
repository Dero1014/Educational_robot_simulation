using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomLib;

public class RobotGraphics : MonoBehaviour
{
    public Transform[] body;


    //              Singleton end                 //    
    public static RobotGraphics current;
    public void Awake()
    {
        current = this;
    }
    //              Singleton end                 //

    private void Start()
    {

        for (int i = 0; i < 3; i++)
        {
            body[i].localScale = new Vector3(10, Robot.currentRobot.Robot_Segments[i].a, 0);

        }
    }

    public void UpdateGraphics()
    {
        Segment[] segs = Robot.currentRobot.Robot_Segments;
        Conversion c = new Conversion();

        for (int i = 0; i < segs.Length; i++)
        {
            Vector2 upWard = new Vector2(Mathf.Cos(c.Angle2Rad(segs[i].gAngle)), Mathf.Sin(c.Angle2Rad(segs[i].gAngle)));
            body[i].up = upWard;
            body[i].position = segs[i].startPoint + (upWard*segs[i].a/2);
        }
        
    }
}
