using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : Menu
{
    public static VolumeSlider instance;
    public Text value;
    public float volume = 0.5f;
    protected override void Awake()
    {
        base.Awake();

        if(!instance)
            instance = this;
        else
            Destroy(gameObject);

        volume = slider.value / 100f;
    }

    private void Update()
    {
        volume = slider.value / 100f;
        if(value)
            value.text = ((int)slider.value).ToString();
    }
}
