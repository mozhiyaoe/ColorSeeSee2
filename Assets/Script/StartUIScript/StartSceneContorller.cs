using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using StarkSDKSpace;
public class StartSceneContorller : MonoBehaviour
{
    public Button FirstModelButton;
    public Button SecondModelButton;
    void Start()
    {

        FirstModelButton.onClick.AddListener(FirstModelStart);
       

        SecondModelButton.onClick.AddListener(SecondModelStart);
       


    }

    public void FirstModelStart()
    {
        SceneManager.LoadScene("FirstModel");
    }
    public void SecondModelStart()
    {
        SceneManager.LoadScene("SecondModel");
    }



   
}
