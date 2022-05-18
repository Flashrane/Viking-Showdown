using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealthRegen : MonoBehaviour
{
    EnemyHealthBar healthBar;
    [SerializeField] Enemy healthInfo;

    WaitForSeconds regenTick = new WaitForSeconds(.3f);
    Coroutine regen;

    void Start()
    {
        healthBar = GetComponent<EnemyHealthBar>();
    }

    void Update()
    {
        if (healthInfo.currentHealth < healthInfo.maxHealth)
        {
            if (regen == null)
                regen = StartCoroutine(RegenHealth());
        }
    }

    IEnumerator RegenHealth()
    {
        while (healthInfo.currentHealth < healthInfo.maxHealth)
        {
            healthInfo.currentHealth += 2;
            healthBar.SetSize((float)healthInfo.currentHealth / healthInfo.maxHealth);
            yield return regenTick;
        }
        regen = null;
    }
}
