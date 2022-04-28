using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    [SerializeField] PlayerCombat combatInfo;

    public Slider healthBar;
    int maxHealth = 100;
    [HideInInspector] public int health;

    WaitForSeconds regenTick = new WaitForSeconds(.1f);
    Coroutine regen;

    void Start()
    {
        healthBar = GetComponent<Slider>();

        health = maxHealth;
        healthBar.maxValue = maxHealth;
        healthBar.value = maxHealth;
    }

    void Update()
    {
        if (health < maxHealth && !combatInfo.isInCombat)
        {
            if (regen == null)
                regen = StartCoroutine(RegenHealth());
        }
    }

    IEnumerator RegenHealth()
    {
        while (health < maxHealth && !combatInfo.isInCombat)
        {
            health += 1;
            healthBar.value = health;
            yield return regenTick;
        }
        regen = null;
    }
}
