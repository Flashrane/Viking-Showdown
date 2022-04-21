using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy : MonoBehaviour
{
    public GameObject warriorPrefab;
    private EnemyAI aiScript;

    HealthBar healthBar;
    public int maxHealth = 100;
    int health;

    float flashTime = 0.1f;
    private SpriteRenderer sprRenderer;
    private Shader shaderGUIText;
    private Shader shaderSpritesDefault;
    Color originalColor;

    float originalSpeed;

    void Start()
    {
        aiScript = GetComponent<EnemyAI>();
        
        healthBar = transform.GetChild(0).gameObject.GetComponent<HealthBar>();

        sprRenderer = GetComponent<SpriteRenderer>();
        shaderGUIText = Shader.Find("GUI/Text Shader");
        shaderSpritesDefault = Shader.Find("Sprites/Default");
        originalColor = sprRenderer.color;

        originalSpeed = aiScript.speed;
        health = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        healthBar.SetSize((float)health/100); // normalize argument

        if (health <= 0)
            Die();
        else
        {
            StopMoving();
            FlashWhite();            
            if (health < 30)
            {
                healthBar.SetColor("FF6303");
            }
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
        Debug.Log(gameObject.name + " died.");

        sprRenderer.color = Color.black;
        GetComponent<Collider2D>().enabled = false;
        GetComponent<EnemyAI>().enabled = false;
        GetComponent<Seeker>().enabled = false;
        this.enabled = false;
    }
}
