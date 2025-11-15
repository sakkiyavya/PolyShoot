using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Menu : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Vector3 showPos;
    public Vector3 hidePos;
    public AnimationCurve bezierCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    public float disappeareTime = 1f;
    public RectTransform rTran;
    public Button button;
    public Slider slider;

    public bool isInitialActive = true;
    public bool isPlayAudio = true;

    protected virtual void Awake()
    {
        rTran = GetComponent<RectTransform>();

        button = GetComponent<Button>();
        slider = GetComponent<Slider>();
        if (button)
            button.onClick.AddListener(PlayClickSound);

        gameObject.SetActive(isInitialActive);
    }
    public virtual void ShowAndHide(float p)
    {
        if (rTran)
            rTran.anchoredPosition = hidePos * (1 - bezierCurve.Evaluate(p)) + showPos * bezierCurve.Evaluate(p);
        else
            Debug.Log(name + "rTran²»´æÔÚ"); 
    }
    public virtual void PlayClickSound()
    {
        UIManager.instance.audioSource.clip = UIManager.instance.UIClick;
        UIManager.instance.audioSource.volume = VolumeSlider.instance.volume;
        UIManager.instance.audioSource.Play();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(isPlayAudio)
        {
            UIManager.instance.audioSource.clip = UIManager.instance.UISelect;
            UIManager.instance.audioSource.volume = VolumeSlider.instance.volume;
            UIManager.instance.audioSource.Play();
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        EventSystem.current.SetSelectedGameObject(null);
    }
}
