using System.Collections;
using System.Collections.Generic;
using StarkSDKSpace;
using UnityEngine;
using UnityEngine.UI;

public class StarkSDKTest : MonoBehaviour
{
   
    public Button shareBtn;

    void Start()
    {
      
        shareBtn.onClick.AddListener(ShareVideo);
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
