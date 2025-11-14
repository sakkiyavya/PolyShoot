using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff : MonoBehaviour
{
    public virtual void ApplyBuff()
    {
        Debug.Log("ApplyBuff");
    }

    public virtual void UndoBuff()
    {
        Debug.Log("UndoBuff");
    }
}
