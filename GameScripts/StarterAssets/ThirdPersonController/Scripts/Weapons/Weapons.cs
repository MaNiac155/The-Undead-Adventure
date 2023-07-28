//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using UnityEngine;
//using UnityEngine.Rendering.Universal;

//namespace Assets.StarterAssets.ThirdPersonController.Scripts
//{
//    [CreateAssetMenu(menuName = "Items/Weapon Item")]
//    public class Weapons : Item
//    {
//        public GameObject modelPrefab;
//        public bool isUnarmed;
//        public int kind;
//        // 1 for sword
//        // 2 for axe
//        public bool isAcquired;
//    }
//}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Assets.StarterAssets.ThirdPersonController.Scripts
{
    [CreateAssetMenu(menuName = "Items/Weapon Item")]
    public class Weapons : Item
    {
        public GameObject modelPrefab;
        public bool isUnarmed;
        public int kind;
        // 1 for sword
        // 2 for axe
        public int ID;
        // 1 for sword
        // 2 for mace
        // 3 for helberd
        // 4 for bigsword
        // 5 for axe
        public bool isAcquired;
        public int baseDamage;
        public int baseStamin;
        public Damages damage;

        private void Awake()
        {
            switch (ID)
            {
                case 0: baseDamage = 10; break;
                case 1: baseDamage = 15; break;
                case 2: baseDamage = 20; break;
                case 3: baseDamage = 35; break;
                case 4: baseDamage = 25; break;
                case 5: baseDamage = 35; break;
            }

            switch (ID)
            {
                case 0: baseStamin = 10; break;
                case 1: baseStamin = 15; break;
                case 2: baseStamin = 20; break;
                case 3: baseStamin = 35; break;
                case 4: baseStamin = 25; break;
                case 5: baseStamin = 35; break;
            }
        }
    }
}
