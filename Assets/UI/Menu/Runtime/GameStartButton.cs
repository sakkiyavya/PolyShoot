using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartButton : Menu
{
    public List<GameObject> startShowUI = new List<GameObject>();
    public void GameStart()
    {
        UIManager.instance.CloseUI();
        UIManager.instance.CloseUI();
        UIManager.instance.CloseUI();
        for (int i = 0; i < startShowUI.Count; i++)
        {
            UIManager.instance.OpenUIEndure(startShowUI[i]);
        }
        EventCenter.CallGameStartAnimaPlay();
    }
}
