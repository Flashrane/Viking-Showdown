using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    public Transform attackPoint;
    [SerializeField] LayerMask enemyLayers;
    [SerializeField] LayerMask obstacleLayer;
    PlayerController movementInfo;
    AnimationManager playerAnimator;
    AnimationManager axeAnimator;
    Rigidbody2D rbPlayer;
    public CameraShake camShake;
    StaminaBar staminaBar;
    [SerializeField] PlayerHealthBar healthBar;

    SpriteRenderer sprRenderer;
    float flashTime = 3f;

    [SerializeField] float attackRange;
    [SerializeField] float attackSpeed;
    [SerializeField] int attackPower;
    [SerializeField] float criticalHitChance;
    [SerializeField] float criticalHitMultiplier;
    public bool isAttacking = false;
    public bool isInCombat = false;
    float nextOutOfCombatTime = 0f;

    void Start()
    {
        rbPlayer = GetComponent<Rigidbody2D>();
        movementInfo = GetComponent<PlayerController>();
        playerAnimator = movementInfo.playerAnimator;
        axeAnimator = movementInfo.axeAnimator;
        staminaBar = movementInfo.staminaBar;
        sprRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!isAttacking)
            {
                if (staminaBar.stamina < StaminaBar.Cost.ATTACK)
                    Debug.Log("Not enough stamina");
                else
                {
                    Attack();
                    staminaBar.UseStamina(StaminaBar.Cost.ATTACK);
                }
            }
        }

        if (movementInfo.isDodging && isAttacking)
        {
            CancelInvoke("AttackCompleted");
            AttackCompleted();
        }

        if (Time.time >= nextOutOfCombatTime)
        {
            isInCombat = false;
        }
    }

    void Attack()
    {
        isAttacking = true;
        playerAnimator.ChangeAnimationState(AnimationManager.PLAYER_ATTACK);
        axeAnimator.ChangeAnimationState(AnimationManager.AXE_ATTACK);

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        if (hitEnemies.Length != 0)
        {
            // check if there is an obstacle in front of the enemies inside the attackRange range
            if (HitObstacle(hitEnemies))
            {
                Invoke("AttackCompleted", 1f / attackSpeed);
                return;
            }

            int closestIdx = 0;
            if (hitEnemies.Length > 1)
                closestIdx = FindClosestEnemy(hitEnemies);

            isInCombat = true;
            nextOutOfCombatTime = Time.time + 5f;

            LookAtEnemy(hitEnemies[closestIdx]);

            Rigidbody2D rbEnemy = hitEnemies[closestIdx].GetComponent<Rigidbody2D>();

            // unfreeze movement of enemy when hit by the player
            rbEnemy.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;

            int damage = Random.Range((int)(attackPower * .75f), attackPower);
            if (Random.Range(0f,1f) > (1 - criticalHitChance)) // if true then it's a crit
            {
                damage = (int)(damage * criticalHitMultiplier);

                Debug.Log("CRITICAL HIT!");
                StartCoroutine(camShake.Shake(.05f, .1f));
            }
            Debug.Log("Damage dealt: " + damage);

            Enemy enemyScript = hitEnemies[closestIdx].GetComponent<Enemy>();
            enemyScript.TakeDamage(damage);
            enemyScript.KnockBack(rbEnemy.position - rbPlayer.position, attackPower * (rbPlayer.velocity.magnitude + 1f));

            //Debug.Log(hitEnemies[closestIdx].name + " got hit.");
        }

        Invoke("AttackCompleted", 1f / attackSpeed);
    }

    bool HitObstacle(Collider2D[] enemiesInRange)
    {
        foreach (Collider2D enemy in enemiesInRange)
        {
            RaycastHit2D hitObstacle = Physics2D.Linecast(attackPoint.position, enemy.transform.position, obstacleLayer);
            //RaycastHit2D hitObstacle = Physics2D.Raycast(attackPoint.position, hitDirection, attackRange, obstacleLayer);
            
            if (hitObstacle)
            {
                Debug.Log(hitObstacle.collider.name + " is in the way");
                return true;
            }
        }
        return false;
    }

    void AttackCompleted()
    {
        isAttacking = false;
        playerAnimator.ChangeAnimationState(AnimationManager.PLAYER_IDLE);
        axeAnimator.ChangeAnimationState(AnimationManager.AXE_IDLE);
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
    }

    public void TakeDamage(int damage)
    {
        isInCombat = true;
        nextOutOfCombatTime = Time.time + 3f;

        healthBar.health -= damage;
        healthBar.healthBar.value = healthBar.health;
        FlashRed();

        if (healthBar.health <= 0)
            Die();
        else
        {
            SlowDown();
        }
    }


    void Die()
    {
        playerAnimator.ChangeAnimationState(AnimationManager.PLAYER_IDLE);
        axeAnimator.ChangeAnimationState(AnimationManager.AXE_IDLE);
        movementInfo.enabled = false;
        this.enabled = false;
    }

    void FlashRed()
    {
        sprRenderer.color = Color.red;
        Invoke("ResetColor", flashTime);
    }

    void ResetColor()
    {
        sprRenderer.color = Color.white;
    }

    void SlowDown()
    {
        movementInfo.slowingStrength = 5f;
        Invoke("ResetSlowingEffect", flashTime);
    }

    void ResetSlowingEffect()
    {
        movementInfo.slowingStrength = 1f;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
