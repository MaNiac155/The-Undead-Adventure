//using Assets.StarterAssets.ThirdPersonController.Scripts.Player;
//using System.Collections;
//using System.Collections.Generic;
//using Unity.VisualScripting.Dependencies.Sqlite;
//using UnityEngine;
//using UnityEngine.UI;

//namespace Assets.StarterAssets.ThirdPersonController.Scripts
//{
//    public class Damages : MonoBehaviour
//    {
//        Collider damage;
//        public int currentDamage = 25;

//        private void Awake()
//        {
//            damage = GetComponent<Collider>();

//            damage.gameObject.SetActive(true);
//            damage.enabled = false;
//            damage.isTrigger = true;
//        }

//        public void EnableDamage()
//        {
//            damage.enabled = true;
//        }

//        public void DisableDamage()
//        {
//            damage.enabled = false;
//        }

//        private void OnTriggerEnter(Collider collision)
//        {


//            Debug.Log(1);
//            if (collision.tag == "Enemies")
//            {
//                Debug.Log(2);
//                EnemyStats enemyBasics = collision.GetComponent<EnemyStats>();

//                if (enemyBasics != null)
//                {
//                    Debug.Log(collision.GetComponent<EnemyStats>().CurrentHealth);
//                    enemyBasics.CurrentHealth -= 2;

//                }
//            }

//        }
//    }
//}

using Assets.StarterAssets.ThirdPersonController.Scripts.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.StarterAssets.ThirdPersonController.Scripts
{
    public class Damages : MonoBehaviour
    {
        public Collider damage;
        public int currentDamage;
        public int actualDamage;
        public PlayerBasics basics;
        public GameObject player;
        public Weapons weapons;
        public PlayerInventory playerInventory;
        public BarSlide playerSlider;

        private void Awake()
        {
            damage = GetComponent<Collider>();
            basics = GetComponentInParent<PlayerBasics>();
            damage.gameObject.SetActive(true);
            damage.enabled = false;
            damage.isTrigger = true;
            player = GameObject.FindGameObjectWithTag("Player");
            basics = player.GetComponent<PlayerBasics>();
            playerInventory = player.GetComponent<PlayerInventory>();
            weapons = playerInventory.right_weapon;
            currentDamage = weapons.baseDamage;
            playerSlider = FindObjectOfType<BarSlide>();
        }

        public void EnableDamage()
        {
            damage.enabled = true;
        }

        public void DisableDamage()
        {
            damage.enabled = false;
        }

        private void OnTriggerEnter(Collider collision)
        {
            actualDamage = currentDamage;
            if (basics._animator.GetCurrentAnimatorStateInfo(0).IsName("HeavySword1") || basics._animator.GetCurrentAnimatorStateInfo(0).IsName("HeavyAxe") || basics._animator.GetCurrentAnimatorStateInfo(0).IsName("HeavyFist"))
            {
                actualDamage = 2 * currentDamage;
            }
            else if (basics._animator.GetCurrentAnimatorStateInfo(0).IsName("SwordSprint") || basics._animator.GetCurrentAnimatorStateInfo(0).IsName("AxeSprint") || basics._animator.GetCurrentAnimatorStateInfo(0).IsName("FistSprint"))
            {
                actualDamage = 3 * currentDamage / 2;
            }
            Debug.Log(1);
            if (collision.tag == "Enemies")
            {
                Debug.Log(2);
                EnemyStats enemyBasics = collision.GetComponent<EnemyStats>();

                if (enemyBasics != null)
                {
                    Debug.Log(collision.GetComponent<EnemyStats>().currentHealth);
                    enemyBasics.currentHealth -= actualDamage;
                    if (enemyBasics.currentHealth < 0)
                    {
                        playerSlider.SetCurrentExperience(playerSlider.sliderEX.value + 20);
                    }

                }
            }

        }
    }
}

