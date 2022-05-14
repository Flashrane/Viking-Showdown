using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    new AudioManager audio;
    PlayerCombat playerCombatInfo;
    public Transform attackPoint;
    [SerializeField] int attackPower = 10;
    public float attackRange = 2.5f;
    public float attackSpeed = 1f;
    public bool isAttacking = false;

    public LayerMask playerLayer;

    void Start()
    {
        audio = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        playerCombatInfo = GameObject.Find("Player").GetComponent<PlayerCombat>();
    }

    public void Attack()
    {
        isAttacking = true;

        if (gameObject.transform.parent.name == "Boss")
            audio.Play("SlapSwing");
        else
            audio.Play("AxeSwing");

        Collider2D hitPlayer = Physics2D.OverlapCircle(attackPoint.position, attackRange, playerLayer);
        if (hitPlayer != null)
        {
            Debug.Log("player got hit");

            playerCombatInfo.TakeDamage(attackPower);

            if (gameObject.transform.parent.name == "Boss")
                audio.Play("SlapImpact");
            else
                audio.Play("AxeImpactEnemy");
        }


        Invoke("AttackCompleted", attackSpeed);
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
