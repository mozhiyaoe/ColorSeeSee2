using System.Collections;
using System.Collections.Generic;
using StarkSDKSpace;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using StarkSDKSpace.UNBridgeLib.LitJson;
public class StarkSDKTest : MonoBehaviour
{
    public Button ShareButton;
    private GameObject Background;
    private FirstModelController FirstModelController;
    private bool GetGameStart;
    private bool GetGameOut;
    public Button GetRankButton;
    private int rankDataType = 0;
    private string zoneId = "default";
    void Start()
    {
        StartVideo();
        Background = GameObject.Find("Background");
        FirstModelController = Background.GetComponent<FirstModelController>();
        ShareButton.onClick.AddListener(ShareVideo);
        GetRankButton.onClick.AddListener(GetImRankList);
    }
    void Update()
    {
        GetGameStart = FirstModelController.GameStart;
        GetGameOut = FirstModelController.GameOut;
        Debug.Log("GetGameOut"+GetGameOut);
        if (GetGameOut == true || GetGameStart == false)
        {
            StopVideo();
        }
        UpdateRankData();
    }
    void StartVideo()
    {
        Debug.Log("抖音 开启录制视频 ...");
        // <param name="isRecordAudio">是否录制声音，默认为录制声音</param>
        // <param name="maxRecordTimeSec">最大录制时长，单位 s。小于等于 0 则无限制。默认为10分钟</param>
        // <param name="startCallback">视频录制开始回调</param>
        // <param name="errorCallback">视频录制失败回调</param>
        bool isStart = StarkSDK.API.GetStarkGameRecorder().StartRecord(true, 200,
            StartCallback, FailedCallback, SuccessCallback);
        Debug.Log("视频开启录制结果 ..." + isStart);
    }
    void StartCallback()
    {
        Debug.Log("视频开始录制回调执行 ...");
        // 开始回调逻辑，比如：显示录屏中按钮
    }
    void FailedCallback(int errCode, string errMsg)
    {
        Debug.Log("录制视频失败回调执行 ... 错误码是：" + errCode + " ，错误消息是：" + errMsg);
        // 失败回调逻辑，比如：隐藏录屏中按钮
    }
    void SuccessCallback(string videoPath)
    {
        Debug.Log("视频录制完成实际路径：" + videoPath);
        // 成功回调逻辑，比如：隐藏录屏中按钮
    }
    void ShareVideo()
    {
        Debug.Log("ShareVideo ShareVideo ...");
        // 只有回调，不带预定义标题和话题
        StarkSDK.API.GetStarkGameRecorder().ShareVideo(SuccessCallback, FailedCallback, CancelledCallback);
        // <param name="successCallback">分享成功回调</param>
        // <param name="failedCallback">分享失败回调</param>
        // <param name="cancelledCallback">分享取消回调</param>
        // <param name="title">分享视频的标题，如不需要设置标题，可以传null或空字符串</param>
        // <param name="topics">分享视频的话题，如不需要设置话题，可以传null或空列表</param>
        //StarkSDK.API.GetStarkGameRecorder().ShareVideoWithTitleTopics(SuccessCallback, FailedCallback,
        //    CancelledCallback, "自定义标题", new List<string>() {"自定义话题1", "自定义话题2"});
        void SuccessCallback(Dictionary<string, object> dictionary)
        {
            Debug.Log("视频分享成功回调 ...");
            // 成功回调逻辑，比如：弹窗提示并发放奖励
        }
        void CancelledCallback()
        {
            Debug.Log("取消分享回调 ...");
            // 取消回调逻辑，比如：弹窗提示
        }
        void FailedCallback(string errMsg)
        {
            Debug.Log("分享视频失败回调执行 ... " + " ，错误消息是：" + errMsg);
            // 失败回调逻辑，比如：弹窗提示
        }
    }
    void StopVideo()
    {
        Debug.Log("抖音 停止录制视频...");
        bool isStop = StarkSDK.API.GetStarkGameRecorder().StopRecord(SuccessCallback, FailedCallback, null, false);
        Debug.Log("停止录制视频状态.." + isStop);
    }
       public void UpdateRankData()
    {
        int starCount = FirstModelController.Score;  //todo... 修改为你的数值
        SetImRankList(starCount);
    }
    public void SetImRankList(int rankValue)
    {
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
    }
     public void GetImRankList()
    {
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
            ["rankType"] = RankType.month.ToString(),
            ["dataType"] = rankDataType,
            ["relationType"] = "default",
            ["suffix"] = "分",
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
    }
}
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
