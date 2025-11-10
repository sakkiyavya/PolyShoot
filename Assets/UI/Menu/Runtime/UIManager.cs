using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    Stack<GameObject> UIs = new Stack<GameObject>();

    private void Start()
    {
        if (!instance)
            instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public void OpenUI(GameObject UI)
    {
        UIAppeare(UI);
        UIs.Push(UI);
    }

    public void CloseUI()
    {
        if(UIs.Count > 0)
        {
            GameObject obj = UIs.Pop();
            obj.SetActive(false);
        }
    }

    public void UIAppeare(GameObject UI)
    {
        UI.SetActive(true);
    }

    public void UIDisappeare(GameObject UI)
    {
        UI.SetActive(false);
    }

    IEnumerator IUIAppeare()
    {
        yield return null;
    }
}
