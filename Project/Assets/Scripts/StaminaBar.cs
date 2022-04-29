using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    public class Cost
    {
        public static int ATTACK = 5;
        public static int DODGE = 20;
    }

    Slider staminaBar;
    int maxStamina = 100;
    [HideInInspector] public int stamina;

    WaitForSeconds regenTick = new WaitForSeconds(.1f);
    Coroutine regen;

    void Start()
    {
        staminaBar = GetComponent<Slider>();

        stamina = maxStamina;
        staminaBar.maxValue = maxStamina;
        staminaBar.value = maxStamina;
    }

    public void UseStamina(int amount)
    {
        if (stamina - amount >= 0)
        {
            stamina -= amount;
            staminaBar.value = stamina;

            if (regen != null)
                StopCoroutine(regen);

            regen = StartCoroutine(RegenStamina());
        }
    }

    IEnumerator RegenStamina()
    {
        yield return new WaitForSeconds(1);

        while (stamina < maxStamina)
        {
            stamina += 5;
            staminaBar.value = stamina;
            yield return regenTick;
        }
        regen = null;
    }
}
