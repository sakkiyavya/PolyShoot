using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenScaleProjectile : Projectile
{
    public override Buff buff
    {
        get { return new ScaleBuff(0.2f); }
    }
    public override string ProjectileName
    {
        get { return "ÂÌÉ«·¶Î§×Óµ¯"; }
    }
}
