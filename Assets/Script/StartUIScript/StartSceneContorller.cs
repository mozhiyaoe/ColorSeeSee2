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
        FirstModelButton.onClick.AddListener(StartVideo);

        SecondModelButton.onClick.AddListener(SecondModelStart);
        SecondModelButton.onClick.AddListener(StartVideo);


    }

    public void FirstModelStart()
    {
        SceneManager.LoadScene("FirstModel");
    }
    public void SecondModelStart()
    {
        SceneManager.LoadScene("SecondModel");
    }



    void StartVideo(){
        bool isStart=StarkSDK.API.GetStarkGameRecorder().StartRecord(true,200);
    }
}
