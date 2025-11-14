using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerState))]
public class PlayerShoot : MonoBehaviour
{
    public List<GameObject> preProjectiles = new List<GameObject>();
    public Queue<GameObject> projectiles = new Queue<GameObject>();
    public float shootSpeed = 1f;
    public float reloadSpeed = 1f;
    public float shootAngle = 10f;

    bool canShoot = true;
    public float reloadProgress = 0;
    float nextShootTime = 0;
    float reloadTime = 0;
    Transform hand;

    GameObject tempObj;
    Projectile tempProjectile;
    Vector3 mousePos;
    private void Awake()
    {
        if(!hand)
            hand = transform.Find("Hand");
    }
    private void Start()
    {
        for (int i = 0; i < preProjectiles.Count; i++)
        {
            projectiles.Enqueue(preProjectiles[i]);
        }
    }
    private void Update()
    {
        if(GameManager.instance.isGamePlaying)
        {
            Shoot();
        }
    }
    public void Shoot()
    {
        if(Input.GetKey(KeyCode.Mouse0) && canShoot)
        {
            if(projectiles.Count <= 0)
            {
                ReloadProjectiles();
                canShoot = false;
            }else
            {
                if(nextShootTime < Time.time)
                {
                    mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    mousePos.z = 0;
                    tempObj = Instantiate(projectiles.Dequeue());
                    tempProjectile = tempObj.GetComponent<Projectile>();
                    nextShootTime = Time.time + 1f / (tempProjectile.shootSpeed + shootSpeed);
                    tempProjectile.dir = Vector3.Normalize(mousePos - transform.position);
                    shootAngle = Mathf.Max(0, shootAngle);
                    tempProjectile.dir = Quaternion.AngleAxis(Random.Range(-shootAngle / 2, shootAngle / 2), Vector3.forward) * tempProjectile.dir;
                    tempProjectile.transform.position = hand.transform.position;
                }
            }
        }
    }
    public void ReloadProjectiles()
    {
        StartCoroutine(Reload());
    }
    IEnumerator Reload()
    {
        while(reloadTime < 1f / reloadSpeed)
        {
            reloadTime += Time.deltaTime;
            reloadProgress = reloadTime / (1f / reloadSpeed);
        }
        yield return new WaitForSeconds(1f / reloadSpeed);
        for(int i = 0;i < preProjectiles.Count;i++)
        {
            projectiles.Enqueue(preProjectiles[i]);
        }
        canShoot = true;
    }
}
