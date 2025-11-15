using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleEnemy : EnemyBase
{


    protected override void AI()
    {
        base.AI();
        if(GameManager.instance.player)
        {
            moveDir = RotateTowards(moveDir, GameManager.instance.player.transform.position - transform.position, Time.deltaTime * 30);
        }
    }

    protected override void Dead()
    {
        StartCoroutine(PlayDeadAnima());
    }

    IEnumerator PlayDeadAnima()
    {
        float t = 0;
        while(t < 0.5f)
        {
            t += Time.deltaTime;
            transform.localEulerAngles = new Vector3(0, Mathf.Lerp(0, 90, t / 0.5f), transform.localEulerAngles.z);
            yield return null;
        }

        Destroy(gameObject);
    }
}
