using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedPowerProjectile : Projectile
{
    public override Buff buff
    {
        get { return new PowerBuff(1); }
    }
    public override string ProjectileName
    {
        get { return "红色力量子弹"; }
    }

}
