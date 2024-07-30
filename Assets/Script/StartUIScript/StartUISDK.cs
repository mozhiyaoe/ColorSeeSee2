using System;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using LitJson;
using Random = UnityEngine.Random;

using StarkSDKSpace;


public class StartUISDK : MonoBehaviour
{
    private bool isLoaginDy = false;
    void Start()
    {
        LoginDy();

    }
    void LoginDy()
    {

        StarkSDK.API.GetAccountManager().Login(OnLoginSuccessCallback, FailedCallback, true);

        void FailedCallback(string errMsg)
        {
            Debug.Log("DY --> 登录失败: " + errMsg);
        }
    }
     void OnLoginSuccessCallback(string code, string anonymousCode, bool isLogin)
    {
         Debug.Log($"登录成功，code: {code}, anonymousCode:{anonymousCode}, isLogin{isLogin}");


        StarkSDK.API.GetAccountManager().GetScUserInfo(OnGetScUserInfoSuccessCallback,
            OnGetScUserInfoFailedCallback);

        void OnGetScUserInfoSuccessCallback(ref ScUserInfo scUserInfo)
        {
            Debug.Log($"登录成功获取用户信息成功，nickName: {scUserInfo.nickName}");
            Debug.Log($"登录成功获取用户信息成功，avatarUrl: {scUserInfo.avatarUrl}");

            isLoaginDy = true;

        }

        void OnGetScUserInfoFailedCallback(string errMsg)
        {
            Debug.Log($"登录成功获取用户信息失败，errMsg: {errMsg}");
        }

    }

}
