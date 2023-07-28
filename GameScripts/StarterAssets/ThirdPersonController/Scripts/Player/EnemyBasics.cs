using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.StarterAssets.ThirdPersonController.Scripts
{
    public class EnemyBasics : MonoBehaviour
    {
        public int currentHealth;
        public int maxhealth = 100;

        Animator animator;

        private void Awake()
        {
            animator = GetComponentInChildren<Animator>();
        }
        private void Start()
        {
            currentHealth = maxhealth;
        }

        public void takeDamage(int damage)
        {
            currentHealth -= damage;
            animator.Play("ARPG_Warrior_Collide");
            if (currentHealth <= 0)
            {
                currentHealth = 0;
                animator.Play("ARPG_Warrior_Death");
            }
        }


    }
}
