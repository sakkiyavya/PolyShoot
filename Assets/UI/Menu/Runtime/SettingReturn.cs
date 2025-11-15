using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingReturn : Menu
{
    public Transform Settings;
    public void SettingReturnToMenu()
    {
        for (int i = 0; i < Settings.childCount; i++)
        {
            UIManager.instance.CloseUI();
        }
    }
    
}
