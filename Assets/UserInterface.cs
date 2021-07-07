using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UserInterface : MonoBehaviour
{
    public TMP_InputField XInput;
    public TMP_InputField YInput;


    public void TestValues() 
    {
        Vector2 x = new Vector2(float.Parse(XInput.text), float.Parse(YInput.text));

        Calculation.current.GetValues(x);
    }


}
