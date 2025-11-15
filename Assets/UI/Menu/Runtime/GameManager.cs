using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool isGamePlaying = false;


    private void OnEnable()
    {
        EventCenter.GameStart += GameStart;
        EventCenter.GameOver += GameOver;
    }

    private void OnDisable()
    {
        EventCenter.GameStart -= GameStart;
        EventCenter.GameOver -= GameOver;
    }

    private void Start()
    {
        if (!instance)
            instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    public void GameStart()
    {
        isGamePlaying = true;
    }

    public void GameOver()
    {
        isGamePlaying = false;
    }
}
