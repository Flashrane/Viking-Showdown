using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheat : MonoBehaviour
{
    public static bool toggled = false;

    [SerializeField] Objective objective;

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                if (!toggled)
                {
                    toggled = true;
                    Debug.Log("Cheating enabled");
                }
                else
                {
                    toggled = false;
                    Debug.Log("Cheating disabled");
                }
            }
        }

        if (!toggled)
            return;

        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            KillAllGuards();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            PlayerController.movementSpeed += 100f;
            Debug.Log("Movement Speed: " + PlayerController.movementSpeed);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            PlayerCombat.attackPower += 50;
            Debug.Log("Attack Power: " + PlayerCombat.attackPower);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            objective.NextObjective();
        }
    }

    void KillAllGuards()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            if (enemy.transform.parent.name != "Boss")
            {
                Enemy script = enemy.GetComponent<Enemy>();
                script.TakeDamage(script.maxHealth);
            }
        }
        Enemy.EnemiesRemaining = 0;
    }
}
