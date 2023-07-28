using Assets.StarterAssets.ThirdPersonController.Scripts.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.StarterAssets.ThirdPersonController.Scripts
{
    public class DamagesOpen : MonoBehaviour
    {
        Collider damage;
        public int currentDamage = 2;

        private void Awake()
        {
            damage = GetComponent<Collider>();

            damage.gameObject.SetActive(true);
            damage.enabled = true;
            damage.isTrigger = true;
            currentDamage = 5;
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
            if (collision.tag == "Player")
            {
                PlayerBasics playerBasics = collision.GetComponent<PlayerBasics>();
                if (playerBasics != null)
                {
                    //If the player is blocking, make no damage
                    if (playerBasics._animator.GetCurrentAnimatorStateInfo(0).IsName("Blocking"))
                    {
                        Debug.Log("Blocked !");
                    }
                    //else make the damage
                    else
                    {
                        playerBasics.takeDamage(currentDamage);
                    }
                }
            }

        }
    }
}
