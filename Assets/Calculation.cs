using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct RequiredValues
{
    public Vector2 toolPosition;
    public float phi;

    public float a1;
    public float a2;
    public float a3;
};


[System.Serializable]
public struct CalculatedValues
{
    public float pwx, pwy;
    public float v1, v2, v3;
    public float alpha, beta;
};

public class Calculation : MonoBehaviour
{

    public RequiredValues ImpValues;
    public CalculatedValues CalValues;

    [HideInInspector]
    public static Calculation current;

    private void Awake()
    {
        current = this;
    }

    // Get the importante values that are needed for calculation
    public void GetValues(Vector2 toolPos)
    {
        ImpValues.toolPosition = toolPos;
        ImpValues.a1 = Robot.currentRobot.Robot_Segments[0].a;
        ImpValues.a2 = Robot.currentRobot.Robot_Segments[1].a;
        ImpValues.a3 = Robot.currentRobot.Robot_Segments[2].a;
        ImpValues.phi = Mathf.Acos(ImpValues.toolPosition.x / ImpValues.toolPosition.magnitude) * (180 / Mathf.PI);

        CalculateValues();
    }

    // Calculate the necessery values
    void CalculateValues()
    {
        float pwx, pwy, c;
        float v1, v2, v3;
        float alpha, beta;

        pwx = ImpValues.toolPosition.x - ImpValues.a3 * Mathf.Cos(ImpValues.phi * (Mathf.PI / 180));
        pwy = ImpValues.toolPosition.y - ImpValues.a3 * Mathf.Sin(ImpValues.phi * (Mathf.PI / 180));

        //V2 = ((pwx)^2+(pwy)^2-(a(1))^2-(a(2))^2)/(2*a(1)*a(2))
        v2 = (Mathf.Pow(pwx, 2) + Mathf.Pow(pwy, 2) - Mathf.Pow(ImpValues.a1, 2) - Mathf.Pow(ImpValues.a2, 2)) / (2 * ImpValues.a1 * ImpValues.a2);
        v2 = Mathf.Acos(v2) * (180 / Mathf.PI);

        c = Mathf.Sqrt(Mathf.Pow(pwx, 2) + Mathf.Pow(pwy, 2));
        print(c);
        alpha = Mathf.Acos(pwx / c) * (180 / Mathf.PI);

        //beta = ((pwx)^2+(pwy)^2+(a(1))^2-(a(2))^2)/(2*a(1)*sqrt((pwx)^2+(pwy)^2))
        beta = (Mathf.Pow(pwx, 2) + Mathf.Pow(pwy, 2) + Mathf.Pow(ImpValues.a1, 2) - Mathf.Pow(ImpValues.a2, 2)) / (2 * ImpValues.a1 * c);
        beta = Mathf.Acos(beta) * (180 / Mathf.PI);
        v1 = alpha - beta;
        v3 = ImpValues.phi - v1 - v2;

        CalValues.pwx = pwx;
        CalValues.pwy = pwy;
        CalValues.v1 = v1;
        CalValues.v2 = v2;
        CalValues.v3 = v3;
        CalValues.alpha = alpha;
        CalValues.beta = beta;

        // I need to send the info back to the robot
        // It needs to know about pwx and pwy cuz thats the startpoint of segment 3
        // end point is tool end point
        // local but also global angle which for v2 and v3 which are 
        // gv2 = v1 + v2
        // gv3 = v1 + v2 + v3
        Robot.currentRobot.NewSegmentValues(pwx, pwy, v1, v2, v3);

        UserResults.current.ShowValues(pwx, pwy, v1, v2, v3);

    }


}
