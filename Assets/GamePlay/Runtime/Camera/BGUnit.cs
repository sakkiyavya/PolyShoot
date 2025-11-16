using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGUnit : MonoBehaviour
{
    float speed;
    float lifeTime;
    float size;
    float originSize;
    Vector3 dir;
    float remainLifetime;

    private void Start()
    {
        speed = Random.Range(1.5f, 2.5f);
        lifeTime = Random.Range(10f, 15f);
        size = Random.Range(0.7f, 0.9f);
        originSize = transform.localScale.x;
        remainLifetime = lifeTime;
        dir = - new Vector3(Random.Range(-1f,1f), Random.Range(-1f, 1f), 0).normalized;
    }

    private void Update()
    {
        if(remainLifetime < 0)
            Destroy(gameObject);

        remainLifetime -= Time.deltaTime;
        transform.localScale = Vector3.one * size * originSize * remainLifetime / lifeTime;
        //dir -= new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0).normalized * 0.1f;
        dir = dir.normalized;
        transform.position += dir * speed * Time.deltaTime;
    }
}
