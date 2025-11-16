using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResolutionSetting : Menu
{
    public Text value;
    Vector2[] Resolutions = new Vector2[]
    {
        new Vector2(2560,1440),
        new Vector2(1920,1080),
        new Vector2(1600,900),
        new Vector2(1280,720),
        new Vector2(800,450)
    };
    Vector2 resolution = new Vector2(1600, 900);
    int resolutionIndex = 2;
    private void Start()
    {
        Screen.SetResolution((int)resolution.x, (int)resolution.y, FullScreenMode.Windowed);
    }
    public void ResolutionChange()
    {
        resolutionIndex++;
        resolutionIndex = resolutionIndex % Resolutions.Length;
        resolution = Resolutions[resolutionIndex];
        Screen.SetResolution((int)resolution.x, (int)resolution.y, FullScreenMode.Windowed);
        if (value)
            value.text = $"·Ö±æÂÊ {(int)resolution.x} x {(int)resolution.y}";
    }
}
