using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.StarterAssets.ThirdPersonController.Scripts;
using UnityEngine.UI;
public class DoorNeedKey : MonoBehaviour
{
    private bool isTriggered;
    private bool isOpened;
    private bool CanOpen;
    public GameObject TextBox;
    public GameObject Content;
    public void Start()
    {
        if (TextBox != null)
        {
            Content = TextBox.transform.GetChild(1).gameObject;
        }

    }
    private void Update()
    {
        if (isTriggered)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (CanOpen)
                {

                    GetComponent<Animator>().SetTrigger("DoorOpen");
                    isOpened = true;
                }
                else
                {
                    TextBox.SetActive(true);
                    Content.GetComponent<Text>().text = "需要钥匙";

                }
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

            if (other.gameObject.GetComponent<PlayerInventory>().haveKey)
            {
                CanOpen = true;
            }

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

            TextBox.SetActive(false);
        }
    }
}
