using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowHint : MonoBehaviour
{
    /*
     * �󶨵������ϣ�������ʾ��Ļ��ʾ
     */
    public GameObject Hint;// ������ʾ��Ļ��ʾ

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
