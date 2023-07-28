using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.StarterAssets.ThirdPersonController.Scripts;
using UnityEngine.UI;
public class ChestOpen : MonoBehaviour
{
    private bool isAfterOpen=false;
    private bool isTriggered=false;
    public Sprite ContentUI;
    public Weapons Content;
    public int ContentID;
    public GameObject TextBox;
    public GameObject TextContent;
    public string ContentName;
    private void Start()
    {
        if (TextBox != null)
        {
            TextContent = TextBox.transform.GetChild(1).gameObject;
        }
    }
    private void Update()
    {
        if (isAfterOpen)
        {
            Destroy(gameObject,4);
        }

        if (isTriggered)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                GetComponent<Animator>().SetTrigger("isOpen");
                if (Content != null)
                {
                    Content.isAcquired = true;
                    GameObject.Find("MenuJogador").GetComponent<backPack>().showImage(ContentID);
                    Invoke("showText", 1f);
                    Invoke("hideText", 2f);
                    
                }
                else
                {
                    if (ContentID == 8)
                    {
                        GameObject.Find("PlayerArmature").GetComponent<PlayerInventory>().haveKey = true;
                        GameObject.Find("MenuJogador").GetComponent<backPack>().showImage(8);
                    }
                    else if (ContentID == 7)
                    {
                        GameObject.Find("PlayerArmature").GetComponent<PlayerInventory>().haveDrink = true;
                        GameObject.Find("MenuJogador").GetComponent<backPack>().showImage(7);
                    }
                    
                }
                isAfterOpen = true;
                //UI
                



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
    private void OnTriggerStay(Collider other)
    {
        if (isAfterOpen)
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
    private void showText()
    {
        TextBox.SetActive(true);
        TextContent.GetComponent<Text>().text = "恭喜！您获得了: " + ContentName;
    }
    private void hideText()
    {
        TextBox.SetActive(false);
    }
}
