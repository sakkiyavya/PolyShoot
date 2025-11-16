using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleBuff : Buff
{
    public float scale = 1;
    public override bool isLastingBuff
    {
        get { return false; }
    }
    public override string description
    {
        get { return $"Ôö¼Ó´ËÂÖÉä»÷{scale}·¶Î§"; }
    }

    public override void ApplyBuff(PlayerControl playerControl, PlayerShoot playerShoot, PlayerState playerState)
    {
        playerShoot.shootScale += scale;
    }
    public override void RemoveBuff(PlayerControl playerControl, PlayerShoot playerShoot, PlayerState playerState)
    {
        playerShoot.shootScale -= scale;
    }
    public override Buff DeepCopy()
    {
        ScaleBuff powerBuff = new ScaleBuff(scale);

        return powerBuff;
    }

    public ScaleBuff(float i)
    {
        scale = i;
    }
    public ScaleBuff()
    {
        new ScaleBuff(0.2f);
    }
}
