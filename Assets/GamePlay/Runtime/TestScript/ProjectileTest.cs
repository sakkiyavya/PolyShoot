using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileTest : MonoBehaviour
{
    public GameObject projectile;

    public float interval = 1f;

    float nextShootTime = 0;
    GameObject tempObj;
    private void Update()
    {
        if(GameManager.instance.isGamePlaying)
        {
            if(Time.time > nextShootTime)
            {
                nextShootTime = Time.time + interval;
                Shoot();
            }
        }
    }
    void Shoot()
    {
        tempObj = Instantiate(projectile);
        if(tempObj.GetComponent<Projectile>())
        {
            Vector2 p = new Vector2(3, Random.Range(-90f, 90f));
            p = new Vector2(p.x * Mathf.Cos(p.y), p.x * Mathf.Sin(p.y));
            tempObj.transform.position = new Vector3(p.x, p.y, 0) + PlayerControl.instance.transform.position;
            tempObj.GetComponent<Projectile>().dir = -p.normalized;
        }
        else
        {
            Destroy(tempObj);
        }    
    }
}
