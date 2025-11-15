using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public GameObject projectile;
    public bool isContactDamage = false;
    public float hitBackForce = 1;
    public float damage = 1f;

    public Rigidbody2D rigidBody;



    protected virtual void AI()
    {

    }
    protected virtual void Move()
    {

    }
}
