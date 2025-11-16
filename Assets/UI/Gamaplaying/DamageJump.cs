using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageJump : MonoBehaviour
{
    public static DamageJump instance;
    public GameObject damageJumpPrefab;
    public Color defaultCol;

    GameObject tempObj;
    private void Awake()
    {
        if (!instance)
            instance = this;
        else
            Destroy(gameObject);
    }
    public void CreatDamageJump(int damage, Vector3 pos, Color col)
    {
        tempObj = Instantiate(damageJumpPrefab, transform);
        //tempObj.transform.position = pos;
        tempObj.GetComponent<DamageJumpIndividual>().pos = pos;
        tempObj.GetComponent<Text>().text = damage.ToString();
        tempObj.GetComponent<Text>().color = col;
    }
    public void CreatDamageJump(int damage, Vector3 pos) => CreatDamageJump((int)damage, pos, defaultCol);
}
