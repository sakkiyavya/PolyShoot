using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : Menu
{
    public void ExitGame()
    {
        Debug.Log("exit");
        Application.Quit();
    }
}
