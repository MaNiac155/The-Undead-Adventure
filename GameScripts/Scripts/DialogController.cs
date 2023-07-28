using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
public class DialogController : MonoBehaviour
{
    public DialogData dialogEmpty;
    public DialogData dialogFinish;

    public List<string> dialogEmptyList = new List<string>();
    public List<string> dialogFinishList = new List<string>();

    public GameObject TextBox;
    public GameObject TextContent;
    
    public bool isTalking;
    public bool isTriggered;
    public bool finishedTalking;
    private void Start()
    {
        dialogEmptyList = dialogEmpty.dialogList;
        dialogFinishList=dialogFinish.dialogList;
        finishedTalking = false;
        if (TextBox != null)
        {
            TextContent = TextBox.transform.GetChild(1).gameObject;
        }

    }

    public int talkPhase = 0;
    private void Update()
    {
        if (isTriggered)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                GetComponent<Animator>().SetTrigger("EnterTalk");
                TextBox.SetActive(true);
                isTalking = true;
            }

            if (isTalking)
            {
                
                if (finishedTalking)
                {
                    talkPhase = 14;
                    if (Input.GetKeyUp(KeyCode.Space))
                    {
                        TextBox.SetActive(false);
                        isTalking = false;
                    }
                }
                if (talkPhase<=13&&Input.GetKeyUp(KeyCode.Space))
                {
                    talkPhase++;
                    Invoke("DiaglogTalk",0f);
                    if (talkPhase > 13)
                    {
                        TextBox.SetActive(false);
                        isTalking = false;
                        finishedTalking = true;
                    }
                }

                
            }
            
        }



    }

    private void DiaglogTalk()
    {
        
        TextContent.GetComponent<Text>().text = dialogEmptyList[talkPhase];
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isTriggered = true;
            other.gameObject.GetComponent<ShowHint>().Show();
            Invoke("DiaglogTalk", 0f);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isTriggered = false;
            other.gameObject.GetComponent<ShowHint>().Hide();
            TextBox.SetActive(false);
            isTalking = false;
        }
    }
}
