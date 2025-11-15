using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPRoll : Menu
{
    public static HPRoll instance;
    public Color healthyColor;
    public Color deadColor;
    public Image image;
    public Text hp;

    float H1;
    float S1;
    float V1;
    
    float H2;
    float S2;
    float V2;

    Color tempCol;
    protected override void Awake()
    {
        base.Awake();
        instance = this;
    }
    public override void ShowAndHide(float p)
    {
        if (p >= 1)
            EventCenter.CallGameStart();
        HPChange(p);
    }
    public void HPChange(float p)
    {
        Color.RGBToHSV(healthyColor,out H1,out S1,out V1);
        Color.RGBToHSV(deadColor, out H2, out S2, out V2);
        tempCol = Color.HSVToRGB(Mathf.Lerp(H2, H1, p), Mathf.Lerp(S2, S1, p), Mathf.Lerp(V2, V1, p));

        if(hp)
        {
            hp.text = $"{PlayerState.instance.currentHP} / {PlayerState.instance.MaxHP}";
            hp.color = tempCol;
        }

        if(image)
        {
            image.color = tempCol;
            rTran.sizeDelta = new Vector2(200 * p, 20);
        }

    }
}
