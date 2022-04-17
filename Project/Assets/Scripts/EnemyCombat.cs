using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    public Transform attackPoint;
    [SerializeField] int attackPower = 10;
    [SerializeField] float attackRange = 1.5f;
    [SerializeField] float attackSpeed = 0.333f;
    float nextAttackTime = 0f;
    public bool isAttacking = false;

    public LayerMask playerLayer;

    public void Attack()
    {
        isAttacking = true;

        Collider2D hitPlayer = Physics2D.OverlapCircle(attackPoint.position, attackRange, playerLayer);
        if (hitPlayer != null)
        {
            Debug.Log("player got hit");

        }
        else
        {
            Debug.Log("player safe");

        }
        Invoke("AttackCompleted", 1f / attackSpeed);
    }

    void AttackCompleted()
    {
        isAttacking = false;
    }
}
