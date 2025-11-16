using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ProjectileView : Menu
{
    public List<GameObject> projectilePrefabs = new List<GameObject>();
    public GameObject hitProjectilePrefab;

    public Text Name;
    public Text description;
    public Text value;

    private void Start()
    {
        if(!hitProjectilePrefab)
            RandowProjecttile();
    }
    private void Update()
    {
        if(Name)
        {
            Name.text = hitProjectilePrefab.GetComponent<Projectile>().ProjectileName;
            Name.color = hitProjectilePrefab.GetComponent<Projectile>().color;
        }
        if(description)
        {
            description.text = hitProjectilePrefab.GetComponent<Projectile>().buff.description;
            description.color = hitProjectilePrefab.GetComponent<Projectile>().color;
        }
        if(value)
        {
            value.text = hitProjectilePrefab.GetComponent<Projectile>().value.ToString();
            value.color = hitProjectilePrefab.GetComponent<Projectile>().color;
        }
    }

    public void Purchase()
    {
        bool success = PlayerState.instance.LoseMoney(hitProjectilePrefab.GetComponent<Projectile>().value);
        if (success)
        {
            PlayerShoot.instance.ChangeMagezine(hitProjectilePrefab);
            RandowProjecttile();
        }
        else
        {
            PurchaseFalse();
        }

    }
    public void RandowProjecttile()
    {
        hitProjectilePrefab = projectilePrefabs[Random.Range(0, projectilePrefabs.Count)];
    }

    public void PurchaseFalse()
    {
        StartCoroutine(Shake());
    }

    IEnumerator Shake()
    {
        float t = 0.5f;
        Vector2 oriPos = rTran.anchoredPosition;
        while(t > 0)
        {
            t -= Time.deltaTime;
            rTran.anchoredPosition += new Vector2(Mathf.Cos(t * 500), Mathf.Sin(t * 600));
            Name.color = Color.red;
            description.color = Color.red;
            value.color = Color.red;
            yield return null;
        }
        rTran.anchoredPosition = oriPos;
        Name.color = hitProjectilePrefab.GetComponent<Projectile>().color;
        description.color = hitProjectilePrefab.GetComponent<Projectile>().color;
        value.color = hitProjectilePrefab.GetComponent<Projectile>().color;
    }
}
