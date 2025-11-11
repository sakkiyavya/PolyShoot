using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartButton : Menu
{
    public void GameStart()
    {
        UIManager.instance.CloseUI();
        UIManager.instance.CloseUI();
        UIManager.instance.CloseUI();
        GameManager.instance.isGamePlaying = true;
    }
}
