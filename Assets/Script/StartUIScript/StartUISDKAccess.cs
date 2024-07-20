using System.Collections;
using System.Collections.Generic;
using System.IO;
using LitJson;
using StarkSDKSpace;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class StartUISDKAccess : MonoBehaviour
{
    // 定义两个静态字符串变量
    public static string Code = "";
    public static string AnonymousCode = "";
    [HideInInspector]

    // 定义一个游戏对象变量，用于存储测试按钮的预制体
    public GameObject TestItemButtonPrefab;
    [HideInInspector]

    // 定义一个Transform变量，用于存储内容
    public Transform m_Content;
    [HideInInspector]


    // 定义两个Text变量，用于存储日志信息和环境信息
    public Text m_LogMessage;
    [HideInInspector]

    public Text m_EnvMessage;

    // 定义一个受保护的字符串变量，用于存储消息
    [HideInInspector]
    protected string m_Message;

   public Text dataTypeGet;
   public Text valueGet;
   public Text priorityGet;
   public Text zoneIdGet;



    void Start()
    {
        // 调用TestNoForceLogin方法
        ForceLogin();
        //SetJson = File.ReadAllText(Application.streamingAssetsPath + "/Set.json");
    }








    // 定义一个TestNoForceLogin方法，用于测试不强制登录
    public void ForceLogin()
    {
        // 调用RealLogin方法，传入参数true
        RealLogin(true);
    }
    // 定义一个RealLogin方法，用于登录
    public void RealLogin(bool force)
    {
        // 将Code和AnonymousCode置空
        Code = "";
        AnonymousCode = "";

        // 调用ShowTipsWhenDontUse方法，传入参数CanIUse.StarkAccount.Login
        ShowTipsWhenDontUse(CanIUse.StarkAccount.Login);
        // 调用StarkSDK的API，获取AccountManager，并调用Login方法，传入回调函数
        StarkSDK.API.GetAccountManager().Login((c1, c2, isLogin) =>
            {
                // 调用PrintText方法，传入参数
                PrintText($"TestLogin: force:{force},code:{c1},anonymousCode:{c2},isLogin:{isLogin}");
                // 将c1赋值给Code
                Code = c1;
                // 将c2赋值给AnonymousCode
                AnonymousCode = c2;
            },
            // 定义一个回调函数，用于处理登录失败的情况
            (msg) => { PrintText($"TestLogin: force:{force},{msg}"); }, force);
    }
    // 定义一个ShowTipsWhenDontUse方法，用于显示提示信息
    public bool ShowTipsWhenDontUse(bool caniuse)
    {
        // 判断当前平台是否为Android，且caniuse为false
        if (Application.platform == RuntimePlatform.Android && !caniuse)
        {
            // 输出错误信息
            UnityEngine.Debug.LogError("当前宿主的Container版本过低，不可使用该接口");
            // 调用AndroidUIManager的ShowToast方法，显示提示信息
            AndroidUIManager.ShowToast("当前宿主的Container版本过低，不可使用该接口");
        }

        // 返回caniuse
        return caniuse;
    }
    // 定义一个PrintText方法，用于打印文本
    protected void PrintText(string msg, params object[] args)
    {
        try
        {
            // 将msg格式化，并赋值给formated
            string formated = string.Format(msg, args);
            // 将formated赋值给m_Message
            m_Message = formated;
        }
        catch
        {
            // 将msg赋值给m_Message
            m_Message = msg;
        }
        // 输出m_Message
        Debug.Log(m_Message);

    }
    void SetImRankDataNew()
    {
        var dataType = dataTypeGet.text;
        var value = valueGet.text;
        var priority = priorityGet.text;
        var zoneId = zoneIdGet.text;
        var paramJson = new JsonData
        {
            ["dataType"] = int.Parse(dataType),
            ["value"] = value,
            ["priority"] = int.Parse(priority),
            ["zoneId"] = zoneId
        };
        Debug.Log($"SetImRankData param:{paramJson.ToJson()}");
        /*
        StarkSDK.API.GetStarkRank().SetImRankDataV2(StarkSDKSpace.UNBridgeLib.LitJson.JsonData.paramJson, (isSuccess, errMsg) =>
        {
            if (isSuccess)
            {
            }
            else
            {
            }
        });
        */
    }




}