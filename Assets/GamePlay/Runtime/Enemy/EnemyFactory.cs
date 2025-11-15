using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float interval = 1f;
    [Min(1)]
    public int perSpawnTime = 1;

    float nextSpawnTime = 0;
    GameObject tempObj;
    float tempFloat;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.instance.isGamePlaying)
        {
            if(nextSpawnTime < Time.time && enemyPrefab)
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
}
