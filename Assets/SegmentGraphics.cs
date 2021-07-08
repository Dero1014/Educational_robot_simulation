using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SegmentGraphics : MonoBehaviour
{
    public Transform joint;
    public Transform body;


    // Update is called once per frame
    void Update()
    {
        joint.position = body.position - (body.up * (body.localScale.y/2));
    }
}
