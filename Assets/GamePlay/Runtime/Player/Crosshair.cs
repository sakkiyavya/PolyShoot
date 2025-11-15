using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerShoot))]
public class Crosshair : MonoBehaviour
{
    public Transform crosshair;
    public Material mat;
    public PlayerShoot shoot;
    public float scaleParam = 1f;
    public float shakeTime = 0.3f;
    Vector3 mousePos;
    float remainShakeTime = 0;
    private void Awake()
    {
        crosshair = transform.Find("Crosshair");
        shoot = GetComponent<PlayerShoot>();
        if(crosshair)
            mat = crosshair.GetComponent<SpriteRenderer>().material;
    }
    void Update()
    {
        if(GameManager.instance.isGamePlaying)
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            mousePos -= transform.position;
            if (mousePos.magnitude < 1)
                mousePos = mousePos.normalized;

            remainShakeTime -= Time.deltaTime;
            remainShakeTime = Mathf.Max(remainShakeTime, 0);

            if(mat)
            {
                mat.SetVector("_Dir", mousePos.normalized);
                mat.SetFloat("_TransformScale", Mathf.Sqrt(mousePos.magnitude * 2.215f));
                mat.SetFloat("_Angle", shoot.shootAngle * (1 + remainShakeTime / 2));
            }
            if(crosshair)
            {
                crosshair.localScale = Vector3.one * mousePos.magnitude * 2.215f;
            }
        }

    }

    public void Shake()
    {
        remainShakeTime = shakeTime;
    }
}
