using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerBuff : Buff
{
    public int damage = 1;
    public override bool isLastingBuff
    {
        get { return false; }
    }
    public override string description
    {
        get { return $"增加此轮射击{damage}点伤害"; }
    }
    
    public override void ApplyBuff(PlayerControl playerControl, PlayerShoot playerShoot, PlayerState playerState)
    {
        playerShoot.shootDamage += damage;
    }
    public override void RemoveBuff(PlayerControl playerControl, PlayerShoot playerShoot, PlayerState playerState)
    {
        playerShoot.shootDamage -= damage;
    }
    public override Buff DeepCopy()
    {
        PowerBuff powerBuff = new PowerBuff(damage);

        return powerBuff;
    }

    public PowerBuff(int i)
    {
        damage = i;
    }
    public PowerBuff()
    {

    }
}
