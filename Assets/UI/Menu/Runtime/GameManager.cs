using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject playerPrefab;
    public GameObject player;
    public bool isGamePlaying = false;

    private void Awake()
    {
        if (!instance)
            instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }
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
    public void GameStart()
    {
        isGamePlaying = true;
        if(player)
            Destroy(player);
        player = Instantiate(playerPrefab);
    }

    public void GameOver()
    {
        isGamePlaying = false;
    }
}
