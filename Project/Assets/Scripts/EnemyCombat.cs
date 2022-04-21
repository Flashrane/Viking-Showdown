using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    public PlayerCombat playerCombatInfo;
    public Transform attackPoint;
    [SerializeField] int attackPower = 10;
    [SerializeField] float attackRange = 2.5f;
    [SerializeField] float attackSpeed = 1f;
    public bool isAttacking = false;

    public LayerMask playerLayer;

    public void Attack()
    {
        isAttacking = true;

        Collider2D hitPlayer = Physics2D.OverlapCircle(attackPoint.position, attackRange, playerLayer);
        if (hitPlayer != null)
        {
            Debug.Log("player got hit");

            playerCombatInfo.TakeDamage(attackPower);
        }
        else
        {
            Debug.Log("enemy hit missed");

        }
        Invoke("AttackCompleted", 1f / attackSpeed);
    }

    void AttackCompleted()
    {
        isAttacking = false;
    }
}
