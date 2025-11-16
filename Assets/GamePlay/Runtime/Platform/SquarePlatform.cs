using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquarePlatform : MonoBehaviour
{
    public SpriteRenderer sr;

    void Update()
    {
        sr.material.SetVector("_XYScale", transform.localScale);
    }
}
