using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healthbar : MonoBehaviour
{
    float maxHealth;
    float currentHealth;
    public float healthPercent
    {
        get
        {
            return currentHealth / maxHealth;
        }
    }

    public void Init(float currentHealth, float maxHealth)
    {
        this.maxHealth = maxHealth;
        this.currentHealth = currentHealth;
    }

    public void SetHealth(float newHealth)
    {
        currentHealth = newHealth;
        transform.localScale = new Vector3(healthPercent, 1);
    }
}
