using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public EnemiesData data;
    public int currentHealth;

    private void Awake()
    {
        currentHealth = data.maxHealth;
    }

    public int MaxHealth
    {
        get
        {
            if (data != null)
            {
                return data.maxHealth;
            }
            else
            {
                return 0;
            }
        }
        set
        {
            data.maxHealth = value;
        }
    }

    internal int CurrentHealth
    {
        get => data != null ? data.currentHealth : 0;
        set => currentHealth = value;
    }

    public int AttackDamage
    {
        get
        {
            if (data != null)
            {
                return data.attackDamage;
            }
            else
            {
                return 0;
            }
        }
        set
        {
            data.attackDamage = value;
        }
    }
}
