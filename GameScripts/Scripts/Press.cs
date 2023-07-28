using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Press : MonoBehaviour
{
    public bool isPressed = false;
    public bool ElevatorStable = true;
    private void OnTriggerEnter(Collider other)
    {
        if (ElevatorStable)
        {
            Debug.Log("Press Pressed");
            isPressed = true;
            Invoke("PressUp", 0.2f);
        }
    }
    void PressUp()
    {
        if (isPressed) isPressed = false;
    }
    private void OnTriggerExit(Collider other)
    {
        isPressed = false;
    }

}
