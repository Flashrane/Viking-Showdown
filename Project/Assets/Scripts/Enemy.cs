using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy : MonoBehaviour
{
    private EnemyAI aiScript;

    public EnemyHealthBar healthBar;
    int maxHealth = 100;
    int currentHealth;

    float flashTime = 0.1f;
    private SpriteRenderer sprRenderer;
    private Shader shaderGUIText;
    private Shader shaderSpritesDefault;
    Color originalColor;

    float originalSpeed;

    public static int EnemiesRemaining = 0;

    void Start()
    {
        aiScript = GetComponent<EnemyAI>();
        sprRenderer = GetComponent<SpriteRenderer>();
        shaderGUIText = Shader.Find("GUI/Text Shader");
        shaderSpritesDefault = Shader.Find("Sprites/Default");
        originalColor = sprRenderer.color;

        originalSpeed = aiScript.speed;
        currentHealth = maxHealth;
        EnemiesRemaining++;
    }

    void Update()
    {
        healthBar.gameObject.SetActive(currentHealth < maxHealth);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetSize((float)currentHealth/100f); // normalize currentHealth value to stay between 0 and 1

        if (currentHealth <= 0)
            Die();
        else
        {
            StopMoving();
            FlashWhite();            
            if (currentHealth < 30)
            {
                healthBar.SetColor("FF6303");
            }
        }

        if (!aiScript.canSeePlayer)
        {
            aiScript.StopPatrolling();
            StartCoroutine(aiScript.EnableCanSeePlayer());
            aiScript.EnableSeeker();
        }
    }

    void FlashWhite()
    {
        sprRenderer.material.shader = shaderGUIText;
        sprRenderer.color = Color.white;
        Invoke("ResetColor", flashTime);
    }

    void ResetColor()
    {
        sprRenderer.material.shader = shaderSpritesDefault;
        sprRenderer.color = Color.white;
    }

    void StopMoving()
    {
        aiScript.speed = 0f;
        Invoke("ResetSpeed", 0.5f);
    }

    void ResetSpeed()
    {
        aiScript.speed = originalSpeed;
    }

    public void KnockBack(Vector2 hitDirection, float knockBackForce)
    {
        GetComponent<Rigidbody2D>().AddForce(hitDirection * knockBackForce/25, ForceMode2D.Impulse);
    }

    void Die()
    {
        EnemiesRemaining--;

        sprRenderer.color = Color.black;
        GetComponent<Collider2D>().enabled = false;
        GetComponent<EnemyAI>().enabled = false;
        aiScript.DisableSeeker();
        healthBar.gameObject.SetActive(false);
        this.enabled = false;
    }
}
