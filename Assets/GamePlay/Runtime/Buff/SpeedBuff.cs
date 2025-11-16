using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBuff : Buff
{
    public float speed = 0.2f;
    public override bool isLastingBuff
    {
        get { return false; }
    }
    public override string description
    {
        get { return $"提高{speed}射击速度"; }
    }

    public override void ApplyBuff(PlayerControl playerControl, PlayerShoot playerShoot, PlayerState playerState)
    {
        playerShoot.shootSpeed += speed;
    }

    public override void RemoveBuff(PlayerControl playerControl, PlayerShoot playerShoot, PlayerState playerState)
    {
        playerShoot.shootSpeed -= speed;
    }

    public override Buff DeepCopy()
    {
        SpeedBuff buff = new SpeedBuff(0.2f);
        return buff;
    }

    public SpeedBuff(float speed)
    {
        this.speed = speed;
    }
}
