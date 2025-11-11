using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Menu : MonoBehaviour 
{
    public Vector3 showPos;
    public Vector3 hidePos;
    public AnimationCurve bezierCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    public float disappeareTime = 1f;
    public RectTransform rTran;

    protected virtual void Awake()
    {
        rTran = GetComponent<RectTransform>();
    }
    public virtual void ShowAndHide(float p)
    {
        if (rTran)
            rTran.localPosition = hidePos * (1 - bezierCurve.Evaluate(p)) + showPos * bezierCurve.Evaluate(p);
        else
            Debug.Log(name + "rTrab²»´æÔÚ"); 
    }
}
