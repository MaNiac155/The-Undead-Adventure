using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorControl : MonoBehaviour
{
    Animator animator;
    GameObject Press;

    public bool isUp;
    public bool isDown;
    private Transform juese;
    void Start()
    {
        animator = GetComponent<Animator>();
        Press=this.gameObject.transform.GetChild(0).gameObject;
    }

    void Update()
    {

        if(isUp||isDown) 
            Press.GetComponent<Press>().ElevatorStable = true;
        else 
            Press.GetComponent<Press>().ElevatorStable = false;

        if (isDown && Press.GetComponent<Press>().isPressed)
        {
            animator.SetTrigger("NeedGoUp");
            animator.ResetTrigger("NeedGoDown");

        }
        if (isUp && Press.GetComponent<Press>().isPressed)
        {
            animator.SetTrigger("NeedGoDown");
            animator.ResetTrigger("NeedGoUp");
        }


        
       
    }

}
