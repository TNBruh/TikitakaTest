using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] protected AnimationCurve attackAnimationCurve;
    protected Vector3 originPos;
    protected Vector3 targetPos = new Vector3();
    [SerializeField] protected float health;
    float maxHealth;
    [SerializeField] protected float dmg;
    [SerializeField] protected float atkInterval;

    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Healthbar healthbar;

    protected Character attackTarget;
    protected Coroutine attackRoutine;
    protected Coroutine awaitDeathRoutine;

    protected float attackAnimTime = 1;

    public bool IsAttacking {  get { return attackAnimTime < 1; } }

    protected virtual void Start()
    {
        maxHealth = health;
        healthbar.Init(health, maxHealth);
        healthbar.SetHealth(health);
        originPos = transform.position;
    }

    public virtual void SetAttackTarget(Character target)
    {
        attackTarget = target;
        targetPos = target.transform.position;
    }

    public virtual void TakeDamage(float amt)
    {
        health -= amt;
        health = Mathf.Clamp(health, 0, maxHealth);
        healthbar.SetHealth(health);
        if (health == 0)
        {
            CleanUp();
        }
    }

    protected IEnumerator DamageTarget()
    {
        if (attackTarget != null)
        {
            attackAnimTime = 0;
            yield return new WaitUntil(() => attackAnimTime == 1);
            attackTarget.TakeDamage(dmg);
        }
    }

    protected virtual IEnumerator AwaitDeath()
    {
        yield return new WaitUntil(() => health == 0);
    }

    protected virtual void CleanUp()
    {

    }

    protected virtual IEnumerator AttackCycle()
    {
        yield return null;
    }

    private void FixedUpdate()
    {
        attackAnimTime = Mathf.Clamp01(attackAnimTime + 1 * Time.deltaTime);
        if (attackTarget != null)
        {
            transform.position = new Vector3
            {
                x = Mathf.LerpUnclamped(originPos.x, targetPos.x, attackAnimationCurve.Evaluate(attackAnimTime)),
                y = Mathf.LerpUnclamped(originPos.y, targetPos.y, attackAnimationCurve.Evaluate(attackAnimTime))
            };
        }
    }
}
