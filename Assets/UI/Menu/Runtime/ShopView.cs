using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopView : Menu
{
    public GameObject[] projectileView = new GameObject[6];

    bool isShopping = false;
    private void Update()
    {
        if(!isShopping && GameManager.instance.isGamePlaying && Input.GetKeyDown(KeyCode.Q))
        {
            EventCenter.CallGamePause();
            for(int i = 0; i < projectileView.Length; i++)
            {
                UIManager.instance.OpenUI(projectileView[i]);
                isShopping = true;
            }
            return;
        }

        if(isShopping && (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Q)))
        {
            EventCenter.CallGameResume();
            for (int i = 0; i < projectileView.Length; i++)
            {
                UIManager.instance.CloseUI();
                isShopping = false;
            }
            return;
        }
    }
}
