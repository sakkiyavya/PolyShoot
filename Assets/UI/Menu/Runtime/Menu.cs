using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour 
{
    public Vector3 showPos;
    public Vector3 hidePos;
    public AnimationCurve bezierCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    public float disappeareTime = 1f;
}
