using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuOut : MonoBehaviour
{
    
    public GameObject canvasMenuPrincipal;
    public GameObject canvasMenuHistoria;
    public GameObject canvasMenuInstrucoes;
    public GameObject canvasMenuCreditos;
    public GameObject canvasDescricaoPoder;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        canvasMenuPrincipal.SetActive(true);
        canvasMenuInstrucoes.SetActive(false);
        canvasMenuCreditos.SetActive(false);
        canvasMenuHistoria.SetActive(false);
        canvasDescricaoPoder.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            Cursor.lockState = CursorLockMode.None;
        }
       
    }

    //Fun��o que leva o utilizador para o primeiro n�vel
    public void Jogar()
   {
        Debug.Log("asfgsfgh");
       SceneManager.LoadScene("SampleScene");
       Cursor.lockState = CursorLockMode.None;
   }

   //Fun��o que leve o utilizador a sair do jogo
   public void Sair()
   {
       Application.Quit();
       storeDocument(1);

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
   }

    //Interface que cont�m as intru��es do jogo
    public void Instrucoes()
    {
        canvasMenuInstrucoes.SetActive(true);
        // canvasMenuHistoria.SetActive(true);
        canvasMenuPrincipal.SetActive(false);
    }

    //Interface que cont�m a hist�ria do jogo
    public void Historia()
    {
        canvasMenuPrincipal.SetActive(false);
        //canvasMenuHistoria.SetActive(true);
    }

    //Interface que cont�m os cr�ditos do jogo
    public void Creditos()
    {
        canvasMenuPrincipal.SetActive(false);
        canvasMenuCreditos.SetActive(true);
    }

    //Para voltar para o Menu Principal, estando no Menu das Instru��es
    public void VoltarDeInstruçoes()
    {
        canvasMenuInstrucoes.SetActive(false);
        canvasMenuPrincipal.SetActive(true);
    }


    //Para voltar para o Menu Principal, estando no Menu da Hist�ria
    public void VoltarDeHistoria()
    {
        canvasMenuHistoria.SetActive(false);
        canvasMenuPrincipal.SetActive(true);
    }

    //Para ver a p�gina da Descricao do Poder, estando no Menu da Hist�ria
    public void HistoriaToPoder()
    {
        canvasMenuHistoria.SetActive (false);
        canvasDescricaoPoder.SetActive(true);
    }

    //Para voltar para o Menu da Hist�ria, estando na P�gina da Descri��o do Poder
    public void VoltarDePoder()
    {
        canvasDescricaoPoder.SetActive (false);
        canvasMenuHistoria.SetActive(true);
    }

    //Para voltar para o Menu Principal, estando no Menu dos Cr�ditos
    public void VoltarDeCreditos()
    {
        canvasMenuCreditos.SetActive (false);
        canvasMenuPrincipal.SetActive(true);
    }
    
    public void storeDocument(int id)
    {
        PlayerPrefs.SetInt("crystalId", 100);
        PlayerPrefs.Save();
    }

    public void readDocument()
    {
        int id = PlayerPrefs.GetInt("crystalId");
        Debug.Log(""+id);
    }

}