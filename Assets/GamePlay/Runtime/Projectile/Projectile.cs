using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Buff buff;
    public Color color;
    public float shootSpeed = 1f;
    public float damage = 5f;
    public float speed = 5f;
    public float lifeTime = 2f;
    public float recoil = 1f;
    public int penetrateTime = 1;

    public delegate void OnShoot();
    public OnShoot shoot;

    public Vector2 dir;

    protected Material material;
    protected void Awake()
    {
        shoot += ApplyBuff;
        material = GetComponent<SpriteRenderer>().material;
    }
    protected void Start()
    {
        material.SetColor("_Color1", color);
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
        transform.position += new Vector3(dir.x, dir.y, 0).normalized * Time.deltaTime * speed;
    }
    protected void LifeManage()
    {
        lifeTime -= Time.deltaTime;
        if(lifeTime < 0)
            Destroy(gameObject);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 8)
        {
            Destroy(gameObject);
        }
        else
        {
            penetrateTime--;
            if(penetrateTime <= 0)
            {
                Destroy(gameObject);
            }
        }
        
    }
}
