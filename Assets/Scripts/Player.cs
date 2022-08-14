using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    protected override void Start()
    {
        base.Start();
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

    public void DamageBoost()
    {
        dmg += 5;
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
        Destroy(gameObject);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Debug.Log("CLICKED");
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit.collider != null && hit.collider.CompareTag("Enemy"))
            {
                Debug.Log($"{hit.collider.gameObject.name}");
                SetAttackTarget(hit.collider.gameObject.GetComponent(typeof(NPC)) as NPC);
            }
        }
    }
}
