using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    AnimationManager animationManager;
    EnemyCombat combat;
    Rigidbody2D rb;

    Coroutine attackCR = null;

    void Start()
    {
        animationManager = GetComponent<AnimationManager>();
        combat = GetComponent<EnemyCombat>();
        rb = GetComponent<Rigidbody2D>();

        animationManager.ChangeAnimationState(AnimationManager.BOSS_IDLE);
    }

    void Update()
    {
        if (combat.isAttacking)
            if (attackCR == null)
                attackCR = StartCoroutine(AttackAnim());
        else
        {
            if (rb.velocity.magnitude < 0.1f)
                animationManager.ChangeAnimationState(AnimationManager.BOSS_IDLE);
            else
                animationManager.ChangeAnimationState(AnimationManager.BOSS_WALK);
        }
    }

    IEnumerator AttackAnim()
    {

        animationManager.ChangeAnimationState(AnimationManager.BOSS_ATTACK);
        yield return new WaitForSeconds(0.2f);
        animationManager.ChangeAnimationState(AnimationManager.BOSS_IDLE);

        attackCR = null;
    }
}
