using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightWall : MonoBehaviour
{
    private bool isTriggered;
    private bool isOpened;
    private void Update()
    {
        if (isTriggered)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                Destroy(gameObject,0.2f);
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
            isTriggered = false;
            other.gameObject.GetComponent<ShowHint>().Hide();
        }
    }
}
