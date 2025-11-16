using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    public static EnemyFactory instance;
    public GameObject enemyPrefab;
    public float interval = 2f;
    [Min(1)]
    public int perSpawnTime = 1;

    bool isSpwan = false;
    float nextSpawnTime = 0;
    GameObject tempObj;
    float tempFloat;
    private void Awake()
    {
        instance = this;
    }

    private void OnEnable()
    {
        EventCenter.GameOver += GameOver;
    }
    private void OnDisable()
    {
        EventCenter.GameOver -= GameOver;
    }

    void Update()
    {
        if(GameManager.instance.isGamePlaying)
        {
            if(GameManager.instance.player.transform.position.x >= 13)
                isSpwan = true;

            if(nextSpawnTime < Time.time && enemyPrefab && isSpwan)
            {
                nextSpawnTime = Time.time + interval;
                for(int i = 0; i < perSpawnTime;i++)
                {
                    tempObj = Instantiate(enemyPrefab);
                    tempObj.transform.position = transform.position + Vector3.right * Random.Range(-1f, 1f);
                    tempFloat = Random.Range(0, 360f);
                    tempObj.GetComponent<EnemyBase>().moveDir = new Vector2(Mathf.Cos(tempFloat), Mathf.Sin(tempFloat));
                    tempObj.transform.position += Vector3.back * tempObj.transform.position.z;
                }
            }
        }
    }

    public void GameOver()
    {
        isSpwan = false;
        interval = 2f;
    }
}
