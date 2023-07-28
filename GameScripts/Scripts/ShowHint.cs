using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowHint : MonoBehaviour
{
    /*
     * 绑定到人身上，用来显示屏幕提示
     */
    public GameObject Hint;// 用来显示屏幕提示

    public void Show()
    {
        Hint.SetActive(true);
    }
    public void Hide()
    {
        Hint.SetActive(false);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "Elevator")
        {
            transform.parent=hit.gameObject.transform;/////////////////////////////
        }
       
    }
}
