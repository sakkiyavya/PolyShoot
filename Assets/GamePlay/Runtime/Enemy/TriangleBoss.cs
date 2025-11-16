using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleBoss : EnemyBase
{

    protected override void AI()
    {
        base.AI();
        if (GameManager.instance.player)
        {
            moveDir = RotateTowards(moveDir, GameManager.instance.player.transform.position - transform.position, Time.deltaTime * 30);
        }
    }

    protected override void Dead()
    {
        base.Dead();
        StartCoroutine(PlayDeadAnima());
        if (Random.Range(0, 100) < 20)
        {
            PlayerBuff.instance.AddBuff(new MoveSpeedBuff(0.2f));
        }

        if (Random.Range(0, 100) < 40)
        {
            EnemyFactory.instance.interval -= 0.1f;
            EnemyFactory.instance.interval = EnemyFactory.instance.interval > 0.5f ? EnemyFactory.instance.interval : 0.5f;
        }


    }

    IEnumerator PlayDeadAnima()
    {
        float t = 0;
        while (t < 0.5f)
        {
            t += Time.deltaTime;
            transform.localEulerAngles = new Vector3(0, Mathf.Lerp(0, 90, t / 0.5f), transform.localEulerAngles.z);
            yield return null;
        }

        Destroy(gameObject);
    }
}
