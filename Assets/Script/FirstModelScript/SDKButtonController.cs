using System.Collections;
using System.Collections.Generic;
using StarkSDKSpace;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class StarkSDKTest : MonoBehaviour
{
   
    public Button ShareButton;
 
    void Start()
    {
        StarkSDK.API.GetStarkShare().ShowShareMenu() ;
        StartVideo();

        ShareButton.onClick.AddListener(ShareVideo);
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
}
