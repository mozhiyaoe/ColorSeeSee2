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
    void Start()
    {
        FirstModelButton.onClick.AddListener(FirstModelStart);
    }
    public void FirstModelStart()
    {
        SceneManager.LoadScene("FirstModel");
    }
    void LoginDy()
    {

        StarkSDK.API.GetAccountManager().Login(OnLoginSuccessCallback, FailedCallback, true);

        void FailedCallback(string errMsg)
        {
            Debug.Log("DY --> 登录失败: " + errMsg);
        }
    }
}
