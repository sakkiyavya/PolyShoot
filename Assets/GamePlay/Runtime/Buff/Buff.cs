using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff
{
    public virtual bool isLastingBuff
    {
        get 
        {
            return false;
        }
    }
    public virtual string description
    {
        get
        {
            return "ÎÞ¼Ó³É";
        }
    }
    public virtual void ApplyBuff(PlayerControl playerControl, PlayerShoot playerShoot, PlayerState playerState)
    {
        //Debug.Log("ApplyBuff");
    }

    public virtual void RemoveBuff(PlayerControl playerControl, PlayerShoot playerShoot, PlayerState playerState)
    {
        //Debug.Log("RemoveBuff");
    }

    public virtual Buff DeepCopy()
    {
        Buff buff = new Buff();

        return buff;
    }
}
