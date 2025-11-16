using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueSpeedProjectile : Projectile
{
    public override Buff buff
    {
        get {  return new SpeedBuff(0.2f); }
    }
    public override string ProjectileName
    {
        get { return "À¶É«ÉäËÙ×Óµ¯"; }
    }

}
