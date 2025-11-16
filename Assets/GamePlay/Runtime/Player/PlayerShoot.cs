using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerState))]
public class PlayerShoot : MonoBehaviour
{
    public static PlayerShoot instance;
    public List<GameObject> originMagazine = new List<GameObject>();
    public Queue<GameObject> Magazine = new Queue<GameObject>();
    public float shootDamage = 0;
    public float projectileSpeed = 0;
    public float shootSpeed = 1f;
    public float reloadSpeed = 1f;
    public float shootAngle = 10f;
    public float shootScale = 1f;
    public AudioClip shootSoundClip;
    public AudioClip reloadSoundClip;
    public AudioSource[] shootSoundSource = null;
    public Rigidbody2D rigidBody;
    public Crosshair crosshair;
    public PlayerBuff playerBuff;

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
        instance = this;

        if(!hand)
            hand = transform.Find("Hand");

        shootSoundSource = GetComponents<AudioSource>();
        if (!rigidBody)
            rigidBody = GetComponent<Rigidbody2D>();
        if(!crosshair)
            crosshair = GetComponent<Crosshair>();
        if(!playerBuff)
            playerBuff = GetComponent<PlayerBuff>();
    }
    private void Start()
    {
        for (int i = 0; i < originMagazine.Count; i++)
        {
            Magazine.Enqueue(originMagazine[i]);
        }
        MagazineInformation.instance.UpdateProjectiles();
    }
    private void Update()
    {
        if(GameManager.instance.isGamePlaying)
        {
            Shoot();
            if(Input.GetKeyDown(KeyCode.R))
                ReloadProjectiles();
        }
    }
    public void Shoot()
    {
        if(Input.GetKey(KeyCode.Mouse0) && canShoot)
        {
            if(Magazine.Count <= 0)
            {
                ReloadProjectiles();
                canShoot = false;
            }else
            {
                if(nextShootTime < Time.time)
                {
                    if(shootSoundClip)
                    {
                        shootSoundSource[0].clip = shootSoundClip;
                        shootSoundSource[0].volume = VolumeSlider.instance.volume;
                        shootSoundSource[0].Play();
                    }
                    mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    mousePos.z = 0;
                    tempObj = Instantiate(Magazine.Dequeue());
                    tempObj.transform.localScale *= shootScale;
                    tempProjectile = tempObj.GetComponent<Projectile>();

                    nextShootTime = Time.time + 1f / (tempProjectile.shootSpeed + shootSpeed);
                    tempProjectile.damage += shootDamage;
                    tempProjectile.speed += shootSpeed;

                    if (tempProjectile.buff != null && playerBuff)
                        playerBuff.AddBuff(tempProjectile.buff);

                    tempProjectile.dir = Vector3.Normalize(mousePos - transform.position);
                    shootAngle = Mathf.Max(0, shootAngle);
                    tempProjectile.dir = Quaternion.AngleAxis(Random.Range(-shootAngle / 2, shootAngle / 2), Vector3.forward) * tempProjectile.dir;

                    rigidBody.AddForce(- tempProjectile.dir.normalized * tempProjectile.recoil * 50);

                    tempProjectile.transform.position = hand.transform.position;

                    MagazineInformation.instance.UpdateProjectileInformation();
                    crosshair.Shake();
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
        if (reloadSoundClip)
        {
            shootSoundSource[1].clip = reloadSoundClip;
            shootSoundSource[1].volume = VolumeSlider.instance.volume;
            shootSoundSource[1].PlayDelayed(0.2f);
        }
        while (reloadTime < 1f / reloadSpeed)
        {
            reloadTime += Time.deltaTime;
            reloadProgress = reloadTime / (1f / reloadSpeed);
        }
        yield return new WaitForSeconds(1f / reloadSpeed);
        Magazine.Clear();
        for(int i = 0;i < originMagazine.Count;i++)
        {
            Magazine.Enqueue(originMagazine[i]);
        }
        if(playerBuff)
        {
            playerBuff.ClearBuff();
        }
        canShoot = true;
        MagazineInformation.instance.UpdateProjectileInformation();
    }

    public void ChangeMagezine(GameObject projectile)
    {
        if (projectile.GetComponent<Projectile>())
        {
            originMagazine.Add(projectile);
            MagazineInformation.instance.UpdateProjectiles();
        }
    }
    public void ChangeMagezine(List<GameObject> newMagazine)
    {
        bool invaild = true;
        foreach(var p in newMagazine)
        {
            if(!p.GetComponent<Projectile>())
                invaild = false;
        }
        if(invaild)
        {
            originMagazine.Clear();
            originMagazine = newMagazine;
            MagazineInformation.instance.UpdateProjectiles();
        }
    }
}
