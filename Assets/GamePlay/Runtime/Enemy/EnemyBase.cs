using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyBase : MonoBehaviour
{
    public GameObject projectile;
    public bool isContactDamage = false;
    public float MaxHP = 30;
    public float speed = 1;
    public float hitBackForce = 1;
    public float damage = 1f;

    public Rigidbody2D rigidBody;

    public Vector2 moveDir = Vector2.up;
    protected float angle = 0;
    protected float currentHP = 30;
    protected virtual void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }
    protected virtual void Update()
    {
        if(GameManager.instance.isGamePlaying)
        {
            if (currentHP <= 0)
                Dead();
            AI();
        }
    }
    protected virtual void FixedUpdate()
    {
        if (GameManager.instance.isGamePlaying)
        {
            Move();
        }
    }
    protected virtual void AI()
    {

    }
    protected virtual void Move()
    {
        rigidBody.velocity = moveDir.normalized * speed;
        angle = Angle360(Vector2.up, moveDir);
        transform.eulerAngles = new Vector3(0, 0, angle);
    }

    protected float Angle360(Vector2 from, Vector2 to)
    {
        float angle = Vector2.SignedAngle(from, to);
        return (angle + 360f) % 360f;
    }

    protected Vector2 RotateTowards(Vector2 A, Vector2 B, float maxDeltaAngle)
    {
        if (A == Vector2.zero || B == Vector2.zero)
            return B; 

        float current = Mathf.Atan2(A.y, A.x) * Mathf.Rad2Deg;
        float target = Mathf.Atan2(B.y, B.x) * Mathf.Rad2Deg;
        float delta = Mathf.DeltaAngle(current, target);
        float rotate = Mathf.Clamp(delta, -maxDeltaAngle, maxDeltaAngle);
        float newAngle = current + rotate;

        float rad = newAngle * Mathf.Deg2Rad;
        return new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));
    }
    protected virtual void GetDamage(float d)
    {
        currentHP -= d;
    }
    protected virtual void GetHeal(float h)
    {
        currentHP += h;
    }
    protected virtual void Dead()
    {

    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 11)
        {
            if(collision.GetComponent<Projectile>())
            {
                GetDamage(collision.GetComponent<Projectile>().damage);
            }
        }
    }
}
