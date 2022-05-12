using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    Animator anim;
    EnemyCombat combat;
    EnemyAI aiScript;
    Rigidbody2D rb;

    void Start()
    {
        anim = GetComponent<Animator>();
        combat = GetComponent<EnemyCombat>();
        aiScript = GetComponent<EnemyAI>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
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