using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventCenter
{
    public static event Action GameStart;
    public static void CallGameStart()
    {
        GameStart?.Invoke();
    }

    public static event Action GameStartAnimaPlay;
    public static void CallGameStartAnimaPlay()
    {
        GameStartAnimaPlay?.Invoke();
    }

    public static event Action GameOver;
    public static void CallGameOver()
    {
        GameOver?.Invoke();
    }
}
