using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Transform attackPoint;
    public LayerMask enemyLayers;
    public AnimationManager animator;
    
    GameObject playerAttackPoint;
    Rigidbody2D rbPlayer;

    public float attackRange = 2.3f;
    public float attackSpeed = 3f;
    public int attackPower = 25;
    float nextAttackTime = 0f;
    float lastHitTime = 0f;

    void Start()
    {
        rbPlayer = this.GetComponent<Rigidbody2D>();
        playerAttackPoint = GameObject.Find("Player").transform.GetChild(0).gameObject;
    }

    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackSpeed;
            }
        }
        else if (Time.time >= lastHitTime + 0.1f)
            rbPlayer.rotation = 0; // look forward
    }

    void Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        if (hitEnemies.Length != 0)
        {
            int closestIdx = 0;
            if (hitEnemies.Length > 1)
                closestIdx = FindClosestEnemy(hitEnemies);

            LookAtEnemy(hitEnemies[closestIdx]);

            Rigidbody2D rbEnemy = hitEnemies[closestIdx].GetComponent<Rigidbody2D>();

            // unfreeze movement of enemy when hit by the player
            rbEnemy.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;

            Enemy enemyScript = hitEnemies[closestIdx].GetComponent<Enemy>();
            enemyScript.TakeDamage(attackPower);
            enemyScript.KnockBack(rbEnemy.position - rbPlayer.position, attackPower * (rbPlayer.velocity.magnitude + 1f));

            animator.ChangeAnimationState(animator.PLAYER_ATTACK);

            Debug.Log(hitEnemies[closestIdx].name + " got hit.");
        }
    }

    int FindClosestEnemy(Collider2D[] hitEnemies)
    {
        float minDist = attackRange + 1;
        int closestIdx = 0;

        int nEnemy = hitEnemies.Length;
        for (int i = 1; i < nEnemy; i++)
        {
            float dist = (hitEnemies[i].GetComponent<Rigidbody2D>().position - rbPlayer.position).magnitude;
            if (dist < minDist)
            {
                minDist = dist;
                closestIdx = i;
            }
        }
        return closestIdx;
    }

    void LookAtEnemy(Collider2D enemy)
    {
        Vector2 lookDir = enemy.GetComponent<Rigidbody2D>().position - rbPlayer.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rbPlayer.rotation = angle;
        lastHitTime = Time.time;
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
