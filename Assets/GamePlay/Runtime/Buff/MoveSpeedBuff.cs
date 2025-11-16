using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSpeedBuff : Buff
{
    public override bool isLastingBuff
    {
        get { return true; }
    }
    public override string description
    {
        get { return $"增加{addSpeed}移速，最多增加至5"; }
    }
    int maxSpeed = 5;
    float addSpeed = 0.2f;
    public override void ApplyBuff(PlayerControl playerControl, PlayerShoot playerShoot, PlayerState playerState)
    {
        addSpeed = Mathf.Min(addSpeed, maxSpeed - playerControl.runSpeed);
        playerControl.runSpeed += addSpeed;
    }
    public override void RemoveBuff(PlayerControl playerControl, PlayerShoot playerShoot, PlayerState playerState)
    {
        playerControl.runSpeed -= addSpeed;
    }
    public override Buff DeepCopy()
    {
        MoveSpeedBuff buff = new MoveSpeedBuff(addSpeed);
        return buff;
    }
    public MoveSpeedBuff(float a)
    {
        addSpeed = a;
        maxSpeed = 5;
    }
    public MoveSpeedBuff()
    {
        new MoveSpeedBuff(0.2f);
    }
}
