using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Buff buff;
    public float shootSpeed = 1f;
    public float damage = 5f;
    public float speed = 5f;
    public float lifeTime = 2f;

    public delegate void OnShoot();
    public OnShoot shoot;

    public Vector2 dir;
    protected void Awake()
    {
        shoot += ApplyBuff;
    }
    protected void Update()
    {
        if(GameManager.instance.isGamePlaying)
        {
            Move();
            LifeManage();
        }
    }
    public virtual void ApplyBuff()
    {
        if(buff)
            buff.ApplyBuff();
    }
    public virtual void UndoBuff()
    {
        if(buff)
            buff.UndoBuff();
    }
    protected void OnDestroy()
    {
        shoot -= ApplyBuff;
    }
    protected void Move()
    {
        transform.position += new Vector3(dir.x, dir.y, 0) * Time.deltaTime * speed;
    }
    protected void LifeManage()
    {
        lifeTime -= Time.deltaTime;
        if(lifeTime < 0)
            Destroy(gameObject);
    }
}
