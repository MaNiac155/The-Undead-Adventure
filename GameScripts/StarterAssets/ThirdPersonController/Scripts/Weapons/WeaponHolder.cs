using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.StarterAssets.ThirdPersonController.Scripts
{   
    public class WeaponHolder : MonoBehaviour
    {
        public Transform parentOverride;
        public GameObject currentWeapon;
        public bool isright;
        public bool isleft;

        public void UnloadWeapon()
        {
            if(currentWeapon != null)
            {
                currentWeapon.SetActive(false);
            }
        }

        public void DestroyWeapon()
        {
            if (currentWeapon != null)
            {
                Destroy(currentWeapon);
            }
        }

        public void LoadWeapon(Weapons weapons)
        {
            DestroyWeapon();
            if(weapons == null)
            {
                UnloadWeapon();
                return;
            }
            GameObject model = Instantiate(weapons.modelPrefab) as GameObject;
            if(model != null)
            {
                if(parentOverride != null)
                {
                    model.transform.parent = parentOverride;
                }
                else
                {
                    model.transform.parent = transform;
                }

                model.transform.localScale = Vector3.one;
                model.transform.localPosition = Vector3.zero;
                model.transform.localRotation = Quaternion.identity;
            }

            currentWeapon = model;
        }
    }
}
