using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerControl), typeof(PlayerShoot), typeof(PlayerState))]
public class PlayerBuff : MonoBehaviour
{
    public static PlayerBuff instance;
    public List<Buff> BuffList = new List<Buff>();
    public List<Buff> LastingBuffList = new List<Buff>();

    public PlayerControl playerControl;
    public PlayerShoot playerShoot;
    public PlayerState playerState;

    Buff tempBuff;
    private void Awake()
    {
        instance = this;
        playerControl = GetComponent<PlayerControl>();
        playerShoot = GetComponent<PlayerShoot>();
        playerState = GetComponent<PlayerState>();
    }
    public void AddBuff(Buff buff)
    {
        tempBuff = buff.DeepCopy();
        if(tempBuff.isLastingBuff)
        {
            LastingBuffList.Add(tempBuff);
            tempBuff.ApplyBuff(playerControl, playerShoot, playerState);
        }
        else
        {
            BuffList.Add(tempBuff);
            tempBuff.ApplyBuff(playerControl, playerShoot, playerState);
        }
    }

    public void RemoveBuff(Buff buff)
    {
        BuffList.Remove(buff);
        buff.RemoveBuff(playerControl, playerShoot, playerState);
    }
    public void RemoveBuff(int index)
    {
        if(index >= 0 && index < BuffList.Count)
        {
            BuffList[index].RemoveBuff(playerControl, playerShoot, playerState);
            BuffList.RemoveAt(index);
        }
    }

    public void ClearBuff()
    {
        for (int i = BuffList.Count - 1; i >= 0; i--)
        {
            BuffList[i].RemoveBuff(playerControl, playerShoot, playerState);
            BuffList.RemoveAt(i);
        }
    }
    public void ClearAllBuff()
    {
        ClearBuff();
        for (int i = LastingBuffList.Count - 1; i >= 0; i--)
        {
            LastingBuffList[i].RemoveBuff(playerControl, playerShoot, playerState);
            LastingBuffList.RemoveAt(i);
        }
    }
}
