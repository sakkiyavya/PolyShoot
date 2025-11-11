using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSetting : Menu
{
    public List<GameObject> settings = new List<GameObject>();
    public void OpenSetting()
    {
        if(settings.Count > 0)
        {
            foreach(GameObject setting in settings)
            {
                UIManager.instance.OpenUI(setting);
            }
        }
    }
}
