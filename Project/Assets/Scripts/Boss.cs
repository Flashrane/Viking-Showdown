using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    Animator anim;
    Enemy boss;
    EnemyAI aiScript;
    EnemyCombat combat;
    Rigidbody2D rb;

    void Start()
    {
        anim = GetComponent<Animator>();
        boss = GetComponent<Enemy>();
        combat = GetComponent<EnemyCombat>();
        aiScript = GetComponent<EnemyAI>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (boss.currentHealth <= 0)
        {
            GameManager.GameHasEnded = true;
            combat.isAttacking = false;
            aiScript.isRunning = false;
        }

        if (combat.isAttacking)
            anim.SetBool("isAttacking", true);
        else
            anim.SetBool("isAttacking", false);

        if (rb.velocity.magnitude < 0.1f)
            anim.SetBool("isRunning", false);
        else
            anim.SetBool("isRunning", true);
    }
}