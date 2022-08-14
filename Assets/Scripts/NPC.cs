using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Character
{
    Player player;
    protected override void Start()
    {
        base.Start();
        player = FindObjectOfType(typeof(Player)) as Player;
        SetAttackTarget(player);
        attackRoutine = StartCoroutine(AttackCycle());
        awaitDeathRoutine = StartCoroutine(AwaitDeath());
    }

    protected override IEnumerator AttackCycle()
    {
        while (true)
        {
            yield return new WaitForSeconds(atkInterval);
            StartCoroutine(DamageTarget());
        }
    }

    protected override IEnumerator AwaitDeath()
    {
        yield return new WaitUntil(() => health == 0);
        yield return null;
        CleanUp();
    }

    protected override void CleanUp()
    {
        if (attackRoutine != null) StopCoroutine(attackRoutine);
        if (awaitDeathRoutine != null) StopCoroutine(awaitDeathRoutine);
        player.DamageBoost();
        Destroy(gameObject);
    }


}
