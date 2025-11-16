using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGCreator : MonoBehaviour
{
    public List<GameObject> BGS = new List<GameObject>();
    public float interval = 0.5f;
    public int perSpawnTime = 3;

    float nextSpawnTime = 0;

    GameObject tempObj;
    private void Update()
    {
        if(nextSpawnTime < Time.time)
        {
            nextSpawnTime = Time.time + interval;
            {
                for(int i = 0; i < perSpawnTime; i++)
                {
                    tempObj = Instantiate(BGS[Random.Range(0, BGS.Count)]);
                    tempObj.transform.position = transform.position;
                    tempObj.transform.position -= Vector3.forward * tempObj.transform.position.z;
                }
            }
        }
    }

}
