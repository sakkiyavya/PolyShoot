using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : Menu
{
    public void EndThisGame()
    {
        UIManager.instance.CloseUI();
        UIManager.instance.CloseUI();

        EventCenter.CallGameOver();
    }
}
