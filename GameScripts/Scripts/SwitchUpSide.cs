using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchUpSide : MonoBehaviour
{
    public GameObject Elevator;
    private bool isTriggered;// 被激活，按键有响应
    private void Update()
    {
        if (isTriggered)
        {
            if (Input.GetKeyDown(KeyCode.F) && Elevator.GetComponent<ElevatorControl>().isDown)
            {
                GetComponent<Animator>().SetTrigger("SwitchPulled");
                Elevator.transform.GetChild(0).gameObject.GetComponent<Press>().isPressed = true;
                Invoke("PressUp", 0.2f);
                Invoke("StopSwitch", 2.1f);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("触发进入");
        Debug.Log(other.gameObject.name);
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Entered!");
            isTriggered = true;
            other.gameObject.GetComponent<ShowHint>().Show();
        }
    }
    //private void OnTriggerStay(Collider other)
    //{
    //    if (Elevator.GetComponent<ElevatorControl>().isUp)
    //    {
    //        other.gameObject.GetComponent<ShowHint>().Hide();
    //    }
    //}
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Out!");
            isTriggered = false;
            other.gameObject.GetComponent<ShowHint>().Hide();
        }
    }
    void StopSwitch()
    {
        GetComponent<Animator>().ResetTrigger("SwitchPulled");
    }
    void PressUp()
    {
        if (Elevator.transform.GetChild(0).gameObject.GetComponent<Press>().isPressed)
            Elevator.transform.GetChild(0).gameObject.GetComponent<Press>().isPressed = false;
        
    }
}
