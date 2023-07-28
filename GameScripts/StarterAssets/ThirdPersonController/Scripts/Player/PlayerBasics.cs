using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.StarterAssets.ThirdPersonController.Scripts.Player
{
    public class PlayerBasics : MonoBehaviour
    {
        public Animator _animator;
        public bool _hasAnimator;

        public BarSlide healthBar;
        public int currentHealth = 100;
        public int maxHealth = 100;
        public int currentStamin = 100;

        public bool isHit = false;

        private void Awake()
        {
            healthBar = FindObjectOfType<BarSlide>();
            healthBar.SetMaxHealth(maxHealth);
            healthBar.SetMaxExperience(maxHealth);
            healthBar.SetCurrentExperience(0);
        }
        private void Start()
        {
            currentHealth = maxHealth;
            _hasAnimator = TryGetComponent(out _animator);
        }

        private void Update()
        {
            _hasAnimator = TryGetComponent(out _animator);
        }

        public void takeDamage(int damage)
        {
   
            //If the player is rolling or doging, ignore the damage
            if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Rolling") || _animator.GetCurrentAnimatorStateInfo(0).IsName("Doge"))
            {
                return;
            }

            //If the player is guarding, the damage is smaller
            if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Guarding"))
            {
                currentHealth -= damage/2;
            }
            
            else currentHealth -= damage;

            healthBar.SetCurrentHealth(currentHealth);
            isHit = true;
        }

        public void recoverHealth()
        {
            currentHealth = maxHealth;
            healthBar.SetCurrentHealth(currentHealth);
        }


    }
}
