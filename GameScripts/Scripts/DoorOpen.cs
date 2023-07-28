using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.StarterAssets.ThirdPersonController.Scripts;
using TMPro;
public class DoorOpen : MonoBehaviour
{
    private bool isTriggered;
    private bool isOpened;

    public void Start()
    {  
    }
    private void Update()
    {
        if (isTriggered)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {

                GetComponent<Animator>().SetTrigger("DoorOpen");
                isOpened = true;
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

            other.gameObject.GetComponent<ShowHint>().Show();
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (isOpened)
        {
            other.gameObject.GetComponent<ShowHint>().Hide();
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Out!");
            isTriggered= false;
            other.gameObject.GetComponent<ShowHint>().Hide();
        }
    }
}
