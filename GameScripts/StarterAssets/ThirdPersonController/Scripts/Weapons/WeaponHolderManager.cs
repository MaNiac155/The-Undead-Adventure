using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.StarterAssets.ThirdPersonController.Scripts
{   
    public class WeaponHolderManager : MonoBehaviour
    {   
        WeaponHolder rightHand;
        WeaponHolder leftHand;

        Damages leftHandDamage;
        Damages rightHandDamage;

        private void Awake()
        {
            WeaponHolder[] weaponholders = GetComponentsInChildren<WeaponHolder>();
            foreach (WeaponHolder weaponholder in weaponholders)
            {
                if (weaponholder.isright)
                {
                    rightHand = weaponholder;
                }
                else if (weaponholder.isleft)
                {
                    leftHand = weaponholder;
                }
            }
        }


        public void LoadWeapon(Weapons weapons, bool isLeft)
        {
            if (!isLeft)
            {   
                //Load right weapon item
                rightHand.LoadWeapon(weapons);
                //Load right damage collider
                rightHandDamage = rightHand.currentWeapon.GetComponentInChildren<Damages>();
            }
            else
            {   //Load left weapon item
                leftHand.LoadWeapon(weapons);
                //Load left damage collider
                leftHandDamage = leftHand.currentWeapon.GetComponentInChildren<Damages>();
            }
            
        }

        public void OpenRightDamage()
        {
            rightHandDamage.EnableDamage();
        }
        public void OpenLeftDamage()
        {
            leftHandDamage.EnableDamage();
        }
        public void CloseRightDamage()
        {
            rightHandDamage.DisableDamage();
        }
        public void CloseLeftDamage()
        {
            leftHandDamage.DisableDamage();
        }

    }
}
