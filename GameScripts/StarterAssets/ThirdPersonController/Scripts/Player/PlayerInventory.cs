using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.StarterAssets.ThirdPersonController.Scripts
{
    public class PlayerInventory : MonoBehaviour
    {
        public WeaponHolderManager weaponHolderManager;
        public Weapons right_weapon;
        public Weapons left_weapon;
        public Weapons unarmed_weapon;
        public bool haveKey;
        public bool haveDrink;
        //The weapon slot in both hands
        public Weapons[] right_weapons_slot = new Weapons[1];
        public Weapons[] left_weapons_slot = new Weapons[1];

        //The index for weapon currently loaded
        public int cur_right_index = 0;
        public int cur_left_index = 0;

        private void Awake()
        {
            weaponHolderManager = GetComponent<WeaponHolderManager>();
            foreach (Weapons weapons in right_weapons_slot)
            {
                weapons.isAcquired = false;

            }
            right_weapons_slot[0].isAcquired = true;
            haveDrink = true;
            haveKey = false;
        }

        private void Start()
        {   
            right_weapon = right_weapons_slot[cur_right_index];
            left_weapon = left_weapons_slot[cur_left_index];
            weaponHolderManager.LoadWeapon(right_weapon,false);
            weaponHolderManager.LoadWeapon(left_weapon, true);

        }

        //change the right weapon

        //public void changeRightWeapon()
        //{
        //    cur_right_index++;
        //    while (!right_weapons_slot[cur_right_index].isAcquired)
        //    {
        //        cur_right_index++;
        //        if (cur_right_index >= right_weapons_slot.Length)
        //        {
        //            cur_right_index = -1;
        //            right_weapon = unarmed_weapon;
        //            weaponHolderManager.LoadWeapon(unarmed_weapon, false);
        //            break;
        //        }
        //    }
        //    if (cur_right_index != -1)
        //    {
        //        if (cur_right_index >= right_weapons_slot.Length)
        //        {
        //            cur_right_index = -1;
        //            right_weapon = unarmed_weapon;
        //            weaponHolderManager.LoadWeapon(unarmed_weapon, false);
        //        }
        //        else
        //        {
        //            if (right_weapons_slot[cur_right_index] != null)
        //            {
        //                right_weapon = right_weapons_slot[cur_right_index];
        //                weaponHolderManager.LoadWeapon(right_weapons_slot[cur_right_index], false);
        //            }
        //            else
        //            {
        //                cur_right_index++;
        //            }
        //        }
        //    }
        //}
        public void changeRightWeapon()
        {
            cur_right_index++;
            if (cur_right_index >= right_weapons_slot.Length)
            {
                cur_right_index = -1;
                right_weapon = unarmed_weapon;
                weaponHolderManager.LoadWeapon(unarmed_weapon, false);
                return;
            }
            while (!right_weapons_slot[cur_right_index].isAcquired)
            {
                cur_right_index++;
                if (cur_right_index >= right_weapons_slot.Length)
                {
                    cur_right_index = -1;
                    right_weapon = unarmed_weapon;
                    weaponHolderManager.LoadWeapon(unarmed_weapon, false);
                    break;
                }
            }
            if (cur_right_index != -1)
            {
                if (cur_right_index >= right_weapons_slot.Length)
                {
                    cur_right_index = -1;
                    right_weapon = unarmed_weapon;
                    weaponHolderManager.LoadWeapon(unarmed_weapon, false);
                }
                else
                {
                    if (right_weapons_slot[cur_right_index] != null)
                    {
                        right_weapon = right_weapons_slot[cur_right_index];
                        weaponHolderManager.LoadWeapon(right_weapons_slot[cur_right_index], false);
                    }
                    else
                    {
                        cur_right_index++;
                    }
                }
            }
        }
    }
}
