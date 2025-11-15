using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagazineInformation : Menu
{
    public static MagazineInformation instance;
    public GameObject projectileInformationPrefab;
    public List<GameObject> projectiles = new List<GameObject>();
    public List<GameObject> projectileInformations = new List<GameObject>();


    protected override void Awake()
    {
        base.Awake();
        if(!instance)
            instance = this;
        else
            Destroy(gameObject);
    }
    public void UpdateProjectiles()
    {
        projectiles = PlayerShoot.instance.originMagazine;
        for(int i = 0;i < projectiles.Count;i++)
        {
            projectileInformations.Add(Instantiate(projectileInformationPrefab,transform));
            projectileInformations[i].GetComponent<RectTransform>().localPosition = new Vector3(-20, -20 - i * 30, 0);
        }
        UpdateProjectileInformation();
    }
    public void UpdateProjectileInformation()
    {
        if(GameManager.instance.isGamePlaying)
        {
            int a = PlayerShoot.instance.originMagazine.Count - PlayerShoot.instance.Magazine.Count - 1;
            for(int i = 0;i < projectileInformations.Count && i < PlayerShoot.instance.originMagazine.Count; i++)
            {
                if (i <= a)
                {
                    Color c = projectileInformations[i].GetComponent<Image>().color;
                    c.a = 0.2f;
                    projectileInformations[i].GetComponent<Image>().color = c;
                }
                else
                    projectileInformations[i].GetComponent<Image>().color = PlayerShoot.instance.originMagazine[i].GetComponent<Projectile>().color;
            }
        }

    }

}
