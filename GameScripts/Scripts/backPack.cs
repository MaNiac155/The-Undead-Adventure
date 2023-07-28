using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class backPack : MonoBehaviour
{
    public GameObject holder1;
    public GameObject holder2;
    public GameObject holder3;
    public GameObject holder4;
    public GameObject holder5;
    public GameObject holder6;
    public GameObject holder7;
    public GameObject holder8;
    public GameObject holder9;
    
    void Start()
    {
        Debug.Log("The image"+holder1.transform.Find("Icon").GetComponent<Image>().sprite);
        holder1.transform.Find("Icon").GameObject().SetActive(false);
        holder2.transform.Find("Icon").GameObject().SetActive(false);
        holder3.transform.Find("Icon").GameObject().SetActive(true);
        holder4.transform.Find("Icon").GameObject().SetActive(false);
        holder5.transform.Find("Icon").GameObject().SetActive(false);
        holder6.transform.Find("Icon").GameObject().SetActive(true);
        holder7.transform.Find("Icon").GameObject().SetActive(true);
        holder8.transform.Find("Icon").GameObject().SetActive(false);
        holder9.transform.Find("Icon").GameObject().SetActive(false);
        
    }

    public void showImage(int item)
    {
        switch (item)
        {
            case 1:
                holder1.transform.Find("Icon").GameObject().SetActive(true);
                break;
            case 2:
                holder2.transform.Find("Icon").GameObject().SetActive(true);
                break;
            case 3:
                holder3.transform.Find("Icon").GameObject().SetActive(true);
                break;
            case 4:
                holder4.transform.Find("Icon").GameObject().SetActive(true);
                break;
            case 5:
                holder5.transform.Find("Icon").GameObject().SetActive(true);
                break;
            case 6:
                holder6.transform.Find("Icon").GameObject().SetActive(true);
                break;
            case 7:
                holder7.transform.Find("Icon").GameObject().SetActive(true);
                break;
            case 8:
                holder8.transform.Find("Icon").GameObject().SetActive(true);
                break;
            case 9:
                holder9.transform.Find("Icon").GameObject().SetActive(true);
                break;
        }
    }
    
    public void hideImage(int item)
    {
        switch (item)
        {
            case 1:
                holder1.transform.Find("Icon").GameObject().SetActive(false);
                break;
            case 2:
                holder2.transform.Find("Icon").GameObject().SetActive(false);
                break;
            case 3:
                holder3.transform.Find("Icon").GameObject().SetActive(false);
                break;
            case 4:
                holder4.transform.Find("Icon").GameObject().SetActive(false);
                break;
            case 5:
                holder5.transform.Find("Icon").GameObject().SetActive(false);
                break;
            case 6:
                holder6.transform.Find("Icon").GameObject().SetActive(false);
                break;
            case 7:
                holder7.transform.Find("Icon").GameObject().SetActive(false);
                break;
            case 8:
                holder8.transform.Find("Icon").GameObject().SetActive(false);
                break;
            case 9:
                holder9.transform.Find("Icon").GameObject().SetActive(false);
                break;
        }
    }
}