using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealthRegen : MonoBehaviour
{
    EnemyHealthBar healthBar;
    [SerializeField] Enemy healthInfo;

    WaitForSeconds regenTick = new WaitForSeconds(.15f);

    bool isRegenCRRunning = false;

    void Start()
    {
        healthBar = GetComponent<EnemyHealthBar>();
    }

    void Update()
    {
        if (healthInfo.currentHealth < healthInfo.maxHealth && !healthInfo.isInCombat) // not the best practice
        {
            if (isRegenCRRunning)
                return;
            StartCoroutine(RegenHealth());
        }
    }

    IEnumerator RegenHealth()
    {
        isRegenCRRunning = true;
        while (healthInfo.currentHealth < healthInfo.maxHealth && !healthInfo.isInCombat)
        {
            healthInfo.currentHealth += 10;
            healthBar.SetSize((float)healthInfo.currentHealth / healthInfo.maxHealth);
            yield return regenTick;
        }
        isRegenCRRunning = false;
    }
}
