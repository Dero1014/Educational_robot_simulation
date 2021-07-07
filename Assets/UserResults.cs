using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UserResults : MonoBehaviour
{
    TMP_Text _textBox;

    //              Singleton start               //
    public static UserResults current;
    private void Awake()
    {
        current = this;
    }
    //              Singleton end                 //

    private void Start()
    {
        _textBox = GetComponentInChildren<TMP_Text>();
    }

    public void ShowValues(float pwx, float pwy, float v1, float v2, float v3)
    {
        _textBox.text = "Results: \npwx = " + pwx + "\npwy = " + pwy + "\nV1 = " + v1 + "\nV2 = " + v2 + "\nV3 = " + v3;
    }
}
