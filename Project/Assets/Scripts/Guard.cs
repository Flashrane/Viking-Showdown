using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : MonoBehaviour
{
    Animator anim;
    Enemy guard;
    EnemyAI aiScript;
    EnemyCombat combat;
    Rigidbody2D rb;

    void Start()
    {
        anim = GetComponent<Animator>();
        guard = GetComponent<Enemy>();
        combat = GetComponent<EnemyCombat>();
        aiScript = GetComponent<EnemyAI>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (guard.currentHealth <= 0)
        {
            combat.isAttacking = false;
            aiScript.isRunning = false;
        }

        if (combat.isAttacking)
            anim.SetBool("isAttacking", true);
        else
            anim.SetBool("isAttacking", false);

        if ((rb.velocity.magnitude == 0f && aiScript.isRunning) ||
            rb.velocity.magnitude >= 0.1f)
            anim.SetBool("isRunning", true);
        else
            anim.SetBool("isRunning", false);
    }
}
