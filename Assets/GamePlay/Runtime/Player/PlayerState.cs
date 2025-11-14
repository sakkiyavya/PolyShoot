using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public enum State
    {
        alive = 0,
        dead = 1,
    }
    public State state = 0;

    public float MaxHP = 20;

}
