using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(Robot))]
public class RobotEditor : Editor
{
    private void OnSceneGUI()
    {
        Robot robo = (Robot)target;

        Segment[] segments = robo.Robot_Segments;

        int rLenght = segments.Length;

        for (int i = 0; i < rLenght; i++)
        {
            Handles.color = Color.green;
            Handles.DrawLine(segments[i].startPoint, segments[i].endPoint);
            Handles.SphereCap(1, segments[i].startPoint, Quaternion.identity, robo.SphereSize);
            Handles.SphereCap(2, segments[i].endPoint, Quaternion.identity, robo.SphereSize);
        }

    }
    
    //Very interesting way to create your own inspectors
    //override public void OnInspectorGUI()
    //{
    //    base.OnInspectorGUI();
    //    var myScript = target as Robot;
    //    Segment[] segments = myScript.Robot_Segments;
    //    int rLenght = segments.Length;

    //    myScript.DebugMode = GUILayout.Toggle(myScript.DebugMode, "Debug Mode");

    //    if (myScript.DebugMode)
    //    {
    //        for (int i = 0; i < rLenght; i++)
    //        {
    //            segments[i].currentLenght = EditorGUILayout.FloatField(segments[i].currentLenght);
    //        }
    //    }

    //}
}
