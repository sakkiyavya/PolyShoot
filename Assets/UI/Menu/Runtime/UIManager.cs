using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public GameObject gameStart;
    public GameObject gameSetting;
    public GameObject exit;

    public AudioClip UISelect;
    public AudioClip UIClick;
    public AudioSource audioSource;


    Stack<GameObject> UIs = new Stack<GameObject>();
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void Start()
    {
        if (!instance)
            instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
        OpenBaseMenu();
    }

    public void OpenUI(GameObject UI)
    {
        UIAppeare(UI);
        UIs.Push(UI);
    }
    public void OpenUIEndure(GameObject UI)
    {
        UIAppeare(UI);
    }
    void UIAppeare(GameObject UI)
    {
        StartCoroutine(IUIAppeare(UI));
    }
    IEnumerator IUIAppeare(GameObject UI)
    {
        UI.SetActive(true);
        float t = 0;
        while(t < UI.GetComponent<Menu>().disappeareTime)
        {
            t += Time.deltaTime;
            UI.GetComponent<Menu>().ShowAndHide(t / UI.GetComponent<Menu>().disappeareTime);
            yield return null;
        }
    }

    public void CloseUI()
    {
        if(UIs.Count > 0)
        {
            GameObject UI = UIs.Pop();
            UIDisappeare(UI);
        }
    }
    void UIDisappeare(GameObject UI)
    {
        StartCoroutine(IUIDisappeare(UI));
    }
    IEnumerator IUIDisappeare(GameObject UI)
    {
        float t = UI.GetComponent<Menu>().disappeareTime;
        while (t > 0)
        {
            t -= Time.deltaTime;
            t = Mathf.Max(0, t);
            UI.GetComponent<Menu>().ShowAndHide(t / UI.GetComponent<Menu>().disappeareTime);
            yield return null;
        }
        UI.SetActive (false);
        ReAppeare();
    }

    void OpenBaseMenu()
    {
        if (gameStart)
            OpenUI(gameStart);
        if (gameSetting)
            OpenUI(gameSetting);
        if (exit)
            OpenUI(exit);
    }

    void ReAppeare()
    {
        if(UIs.Count > 0)
        {
            Stack<GameObject> uis = new Stack<GameObject> ();
            int c = UIs.Count;
            for(int i = 0;i < c; i++)
                uis.Push(UIs.Pop());

            c = uis.Count;
            for(int i = 0;i < c; i++)
                OpenUI(uis.Pop());
        }
    }
}
