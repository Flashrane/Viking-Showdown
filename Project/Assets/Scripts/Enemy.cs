using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject warriorPrefab;

    public int maxHealth = 100;
    int currentHealth;

    Color hurtLevelColor;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
            Die();
        else
        {
            if (currentHealth <= 25)
                hurtLevelColor = Color.yellow;
            else if (currentHealth <= 50)
                hurtLevelColor = Color.blue;
            else if (currentHealth <= 75)
                hurtLevelColor = Color.cyan;

            gameObject.GetComponent<SpriteRenderer>().color = hurtLevelColor;
        }
    }

    public void KnockBack(Vector2 hitDirection, float knockBackForce)
    {
        gameObject.GetComponent<Rigidbody2D>().AddForce(hitDirection * knockBackForce, ForceMode2D.Impulse);
    }

    void Die()
    {
        Debug.Log(gameObject.name + " died.");
        
        gameObject.GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 0f);
        gameObject.GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
    }
}
