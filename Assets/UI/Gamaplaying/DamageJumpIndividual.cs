using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageJumpIndividual : MonoBehaviour
{
    public Vector3 pos = Vector3.zero;
    public float lifetime = 0.5f;
    public float speed = 1f;

    private void Update()
    {
        if(lifetime >= 0)
        {
            lifetime -= Time.deltaTime;
            pos += Vector3.up * speed * Time.deltaTime;
            transform.position = Camera.main.WorldToScreenPoint(pos);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
