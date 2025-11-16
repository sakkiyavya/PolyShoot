using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerAnima))]
public class PlayerState : MonoBehaviour
{
    public static PlayerState instance;
    public int money = 100;
    public enum State
    {
        alive = 0,
        dead = 1,
    }
    public State state = 0;

    public float MaxHP = 20f;
    public float currentHP = 20f;
    public float HPPercent
    {
        get
        {
            return currentHP / MaxHP;
        }
    }

    public Rigidbody2D rigidBody;

    bool isInvicinble = false;
    Vector2 lastHit = Vector2.right;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        currentHP = MaxHP;
        rigidBody = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (GameManager.instance.isGamePlaying)
        {
            if(currentHP <= 0)
            {
                EventCenter.CallGameOver();
                GetComponent<PlayerAnima>().StartDeadAnima(lastHit);
            }
        }
    }

    public void GetDamage(float d)
    {
        if(!isInvicinble)
        {
            currentHP -= d;
            DamageJump.instance.CreatDamageJump((int)d, transform.position);
            HPRoll.instance.HPChange(HPPercent);
            StartCoroutine(GetDamageInvincible());
        }
    }

    public void GetHeal(float h)
    {
        currentHP += h;
        DamageJump.instance.CreatDamageJump((int)h, transform.position, Color.green);
        HPRoll.instance.HPChange(HPPercent);
    }

    public void GetMoney(int m)
    {
        money += m;
    }
    public bool LoseMoney(int m)
    {
        if(m <= money)
        {
            money -= m;
            return true;
        }
        else
            return false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!isInvicinble)
        {
            if(collision.gameObject.layer == 9)
            {
                if(collision.GetComponent<Projectile>())
                {
                    GetDamage(collision.GetComponent<Projectile>().damage);
                    rigidBody.AddForce(collision.GetComponent<Projectile>().recoil * collision.GetComponent<Projectile>().dir.normalized * 50);
                    lastHit = collision.GetComponent<Projectile>().dir.normalized;
                }
            }

            if(collision.gameObject.layer == 10)
        {
            if(collision.GetComponent<EnemyBase>() && collision.GetComponent<EnemyBase>().isContactDamage)
            {
                GetDamage(collision.GetComponent<EnemyBase>().damage);
                rigidBody.AddForce(collision.GetComponent<EnemyBase>().hitBackForce * 50 * (transform.position - collision.transform.position).normalized);
                lastHit = (transform.position - collision.transform.position).normalized;

            }
        }
        }
    }
    IEnumerator GetDamageInvincible()
    {
        isInvicinble = true;
        float t = 0.5f;
        while (t > 0)
        {
            t -= Time.deltaTime;
            yield return null;
        }
        isInvicinble = false;
    }

}
