using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace StarterAssets
{
    public delegate void AddtoList(int ID);
    public class CrystalLit : MonoBehaviour
    {
        [SerializeField] public int ID;
        public AddtoList addtoList;
        public bool isLightUp = false;
        private bool isTriggered;
        public bool UIisShow=false;

        void Update()
        {
            if (isTriggered && Input.GetKeyDown(KeyCode.F))
            {
                if (!isLightUp)
                {
                    GetComponent<Animator>().SetTrigger("isLit");
                    isLightUp = true;
                    addtoList(ID);
                    GameObject.Find("PlayerArmature").GetComponent<ThirdPersonController>().lastID=ID;
                }
                else
                {
                    if (!transform.parent.gameObject.GetComponent<CrystalData>().UIisShow)
                    {
                        transform.parent.gameObject.GetComponent<CrystalData>().ShowCrystalUI();
                        transform.parent.gameObject.GetComponent<CrystalData>().UIisShow = true;
                    }
                    else
                    {
                        transform.parent.gameObject.GetComponent<CrystalData>().HideCrystalUI();
                        transform.parent.gameObject.GetComponent<CrystalData>().UIisShow = false;
                    }

                    
                }


            }
        }
        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("´¥·¢½øÈë");
            Debug.Log(other.gameObject.name);
            if (other.gameObject.tag == "Player")
            {
                Debug.Log("Entered!");
                isTriggered = true;
                //
                other.gameObject.GetComponent<ShowHint>().Show();
                //
            }
        }
        private void OnTriggerStay(Collider other)
        {
            //if (isLightUp)
            //{
            //    other.gameObject.GetComponent<ShowHint>().Hide();
            //}   
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                Debug.Log("Out!");
                isTriggered = false;
                //
                other.gameObject.GetComponent<ShowHint>().Hide();
                //
            }
        }


    }
}