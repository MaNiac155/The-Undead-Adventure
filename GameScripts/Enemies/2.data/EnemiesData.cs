using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New EnemiesData", menuName = "EnemiesData" )]
public class EnemiesData : ScriptableObject
{
    [Header("Stats Info")] 
    public int maxHealth;
    public int currentHealth;
    public int attackDamage;
    
}
