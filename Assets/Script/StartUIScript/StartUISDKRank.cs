using System;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using LitJson;
using Random = UnityEngine.Random;
#if DY
using StarkSDKSpace;
#endif

public class StartUISDKRank : MonoBehaviour
{
#if DY
    StarkAdManager.BannerStyle style = new StarkAdManager.BannerStyle();
    StarkAdManager.BannerAd bannerAd = null;
    private StarkAdManager.InterstitialAd m_InterAdIns = null;
    int px2dp(int px) => (int) (px * (160 / Screen.dpi));
    
    private string bannedId = "";
    private string interstitialId = "";
    private string videoId = "";
#endif
    private bool isLoaginDy = false;
    
    void Start()
    {
#if DY
        StarkSDK.API.GetStarkScreenManager().SetKeepScreenOn(true);
        StarkSDK.API.GetStarkAdManager();
        StarkAdManager.IsShowLoadAdToast = false;

        LoadInterstitialDY();
        StarkSDK.API.GetStarkShare().ShowShareMenu();
        StarkSDK.API.GetStarkAppLifeCycle().OnShowWithDict += OnShowOneParam;
        
        LoginDy();
#endif
    }

    void LoginDy()
    {
#if DY
        StarkSDK.API.GetAccountManager().Login(OnLoginSuccessCallback, FailedCallback, true);
#endif
        void FailedCallback(string errMsg)
        {
            Debug.Log("DY --> 登录失败: " + errMsg);
        }
    }
    
    /// <summary>登录成功</summary>
    /// <param name="code">临时登录凭证, 有效期 3 分钟。可以通过在服务器端调用 登录凭证校验接口 换取 openid 和 session_key 等信息。</param>
    /// <param name="anonymousCode">用于标识当前设备, 无论登录与否都会返回, 有效期 3 分钟</param>
    /// <param name="isLogin">判断在当前 APP(头条、抖音等)是否处于登录状态</param>
    void OnLoginSuccessCallback(string code, string anonymousCode, bool isLogin)
    {
         Debug.Log($"登录成功，code: {code}, anonymousCode:{anonymousCode}, isLogin{isLogin}");
      
#if DY
        StarkSDK.API.GetAccountManager().GetScUserInfo(OnGetScUserInfoSuccessCallback,
            OnGetScUserInfoFailedCallback);

        void OnGetScUserInfoSuccessCallback(ref ScUserInfo scUserInfo)
        {
            Debug.Log($"登录成功获取用户信息成功，nickName: {scUserInfo.nickName}");
            Debug.Log($"登录成功获取用户信息成功，avatarUrl: {scUserInfo.avatarUrl}");
            
            isLoaginDy = true;
            UpdateRankData();
        }

        void OnGetScUserInfoFailedCallback(string errMsg)
        {
            Debug.Log($"登录成功获取用户信息失败，errMsg: {errMsg}");
        }
#endif
    }

    /// <summary>
    /// 更新排行榜数据 -- 总星星数
    /// </summary>
    public void UpdateRankData()
    {
        FirstModelController firstModelController = FindObjectOfType<FirstModelController>();
        int starCount = firstModelController.Score;  //todo... 修改为你的数值
        SetImRankList(starCount);
    }
    
    /// <summary>
    /// 游戏进入场景分享
    /// {
    ///     "scene": "021036", // 侧边栏复访
    ///     "query": {},
    ///     "showFrom": 10,
    ///     "launch_from": "homepage",
    ///     "location": "sidebar_card",
    ///     "refererInfo": { "appId": "xxxxxx", "extraData": {} }
    /// }
    /// </summary>
    /// <param name="param"></param>
    private void OnShowOneParam(Dictionary<string, object> param)
    {
        Debug.Log($"DY --> 游戏进入场景: {JsonMapper.ToJson(param)}");
        // "scene": "021036"
        if (param.ContainsKey("scene"))
        {
            Debug.Log($"DY --> 游戏进入场景: {param["scene"]}");
        }
    }


    #region 排行榜

    /// <summary>
    /// 0：数字类型、1：枚举类型;
    ///     数字类型（0）往往适用于游戏的通关分数（103分、105分），
    ///     枚举类型（1）适用于段位信息（青铜、白银）；
    /// </summary>
    private int rankDataType = 0;
    // 排行榜分区标识--(Nullable)默认值为default, 测试：test
    private string zoneId = "default";

    /// <summary>
    /// 设置排行榜分数
    /// </summary>
    public void SetImRankList(int rankValue)
    {
#if DY
        if (!isLoaginDy)
        {
            Debug.Log("需要先登录再执行更新数据...");
            return;
        }
        var paramJson = new StarkSDKSpace.UNBridgeLib.LitJson.JsonData
        {
            ["dataType"] = rankDataType,
            ["value"] = rankValue,
            //["priority"] = int.Parse(priority),
            ["zoneId"] = zoneId
        };
        Debug.Log($"SetImRankData param:{paramJson.ToJson()}");
        StarkSDK.API.GetStarkRank().SetImRankDataV2(paramJson, (isSuccess, errMsg) =>
        {
            if (isSuccess)
            {
                Debug.Log("设置排行榜数据成功");
            }
            else
            {
                Debug.Log("设置排行榜数据成功");
            }
        });
#endif
    }

    /// <summary>
    /// 获取排行榜列表，调用 API 后， 根据参数自动绘制游戏好友排行榜
    /// </summary>
    public void GetImRankList()
    {
#if DY
        // <param name="rankType">代表数据排序周期，day为当日写入的数据做排序；week为自然周，month为自然月，all为半年--(Require)</param>
        // <param name="dataType">由于数字类型的数据与枚举类型的数据无法同时排序，因此需要选择排序哪些类型的数据--(Require)</param>
        // <param name="relationType">选择榜单展示范围。default: 好友及总榜都展示，all：仅总榜单--(Nullable)</param>
        // <param name="suffix">数据后缀，最后展示样式为 value + suffix，若suffix传“分”，则展示 103分、104分--(Nullable)</param>
        // <param name="rankTitle">排行榜标题的文案--(Nullable)</param>
        // <param name="zoneId">排行榜分区标识--(Nullable)</param>
        // <param name="paramJson">以上参数使用json格式传入，例如"{"rankType":"week","dataType":0,"relationType":"all","suffix":"分","rankTitle":"","zoneId":"default"}"</param>
        // <param name="action">回调函数</param>
        var paramJson = new StarkSDKSpace.UNBridgeLib.LitJson.JsonData
        {
            ["rankType"] = RankType.week.ToString(),
            ["dataType"] = rankDataType,
            ["relationType"] = "default",
            ["suffix"] = "颗",
            ["rankTitle"] = "巅峰排行榜",
            ["zoneId"] = zoneId,
        }; 
        Debug.Log($"GetImRankList param:{paramJson.ToJson()}");
        StarkSDK.API.GetStarkRank().GetImRankListV2(paramJson, (isSuccess, errMsg) =>
        {
            if (isSuccess)
            {
            }
            else
            {
            }
        });
#endif
    }
    #endregion
}


/// <summary>
/// 排行数据类型
/// </summary>
public enum RankType
{
    // 天
    day,
    // 自然周
    week,
    // 自然月
    month,
    // 半年
    all
}
