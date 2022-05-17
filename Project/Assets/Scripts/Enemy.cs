using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy : MonoBehaviour
{
    private EnemyAI aiScript;
    new AudioManager audio;

    public EnemyHealthBar healthBar;
    public int maxHealth = 100;
    [HideInInspector] public int currentHealth;

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
        audio = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        sprRenderer = GetComponent<SpriteRenderer>();
        shaderGUIText = Shader.Find("GUI/Text Shader");
        shaderSpritesDefault = Shader.Find("Sprites/Default");
        originalColor = sprRenderer.color;

        originalSpeed = aiScript.speed;
        currentHealth = maxHealth;
        if (gameObject.transform.parent.gameObject.name != "Boss")
            EnemiesRemaining++;
    }

    void Update()
    {
        healthBar.gameObject.SetActive(currentHealth >= 0 && currentHealth < maxHealth);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetSize((float)currentHealth/maxHealth); // normalize currentHealth value to stay between 0 and 1

        if (currentHealth <= 0)
        {
            Die();
            audio.Play("GuardDeath");
        }
        else
        {
            StopMoving();
            FlashWhite();            
            if (currentHealth < (maxHealth * 0.3))
            {
                healthBar.SetColor("FF6303");
            }

            audio.Play("GuardInjure");
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
        
        if (gameObject.transform.parent.name != "Boss")
            Invoke("DestroyParent", 7f);
    }

    void DestroyParent()
    {
        Destroy(gameObject.transform.parent.gameObject);        
    }
}
