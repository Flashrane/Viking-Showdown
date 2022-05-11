using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    PlayerCombat playerCombatInfo;
    public Transform attackPoint;
    [SerializeField] int attackPower = 10;
    public float attackRange = 2.5f;
    [SerializeField] float attackSpeed = 1f;
    public bool isAttacking = false;

    public LayerMask playerLayer;

    void Start()
    {
        playerCombatInfo = GameObject.Find("Player").GetComponent<PlayerCombat>();    
    }

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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
