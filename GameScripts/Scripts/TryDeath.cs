using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TryDeath : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.O))
        {
            Debug.Log("start teleport");
            transform.position = new Vector3(357.92f, 21.022f, 60.55f);
            Debug.Log("end teleport");
        }
    }
}
