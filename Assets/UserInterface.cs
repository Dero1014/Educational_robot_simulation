using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UserInterface : MonoBehaviour
{
    public TMP_InputField XInput;
    public TMP_InputField YInput;

    private bool _v2Neg = false;

    // Button
    public void TestValues() 
    {
        Vector2 x = new Vector2(float.Parse(XInput.text), float.Parse(YInput.text));

        Calculation.current.GetValues(x, _v2Neg);
    }

    public void NegV2()
    {
        _v2Neg = !_v2Neg;
    }

}
