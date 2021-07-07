using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomLib;

[System.Serializable]
public struct RequiredValues
{
    public Vector2 toolPosition;
    public float phi;

    public float a1;
    public float a2;
    public float a3;
};

public class Calculation : MonoBehaviour
{
    public RequiredValues IVs;

    private Conversion _conv = new Conversion();

    //              Singleton start               //
    public static Calculation current;
    private void Awake()
    {
        current = this;
    }
    //              Singleton end                 //

    // Get the importante values that are needed for calculation
    public void GetValues(Vector2 toolPos)
    {
        IVs.toolPosition = toolPos;
        IVs.a1 = Robot.currentRobot.Robot_Segments[0].a;
        IVs.a2 = Robot.currentRobot.Robot_Segments[1].a;
        IVs.a3 = Robot.currentRobot.Robot_Segments[2].a;
        IVs.phi = _conv.Rad2Angle(Mathf.Acos(IVs.toolPosition.x / IVs.toolPosition.magnitude));

        CalculateValues();
    }

    // Calculate the necessery values
    void CalculateValues()
    {
        float pwx, pwy, c;
        float v1, v2, v3;
        float alpha, beta;

        pwx = IVs.toolPosition.x - IVs.a3 * Mathf.Cos(_conv.Angle2Rad(IVs.phi));
        pwy = IVs.toolPosition.y - IVs.a3 * Mathf.Sin(_conv.Angle2Rad(IVs.phi));

        float pwx2 = Mathf.Pow(pwx, 2);
        float pwy2 = Mathf.Pow(pwy, 2);
        float a12  = Mathf.Pow(IVs.a1, 2);
        float a22  = Mathf.Pow(IVs.a2, 2);

        v2 = (pwx2 + pwy2 - a12 - a22) / (2 * IVs.a1 * IVs.a2);
        v2 = _conv.Rad2Angle(Mathf.Acos(v2));

        c = Mathf.Sqrt(pwx2 + pwy2);
        alpha = _conv.Rad2Angle(Mathf.Acos(pwx / c));

        beta = (pwx2 + pwy2 + a12 - a22) / (2 * IVs.a1 * c);
        beta = _conv.Rad2Angle(Mathf.Acos(beta));
        v1 = alpha - beta;
        v3 = IVs.phi - v1 - v2;

        // I need to send the info back to the robot
        // It needs to know about the local and global angles which are 
        // calculated by adding angles before them, for v2 and v3 its:
        // gv2 = v1 + v2
        // gv3 = v1 + v2 + v3

        float[] localAngles = { v1, v2, v3 };
        float[] globalAngles = { 0, 0, 0 };

        for (int i = 0; i < 3; i++)
        {
            globalAngles[i] = _conv.GlobalAngle(i, localAngles);
        }

        Robot.currentRobot.NewSegmentValues(localAngles, globalAngles);

        UserResults.current.ShowValues(pwx, pwy, v1, v2, v3);
    }

}
