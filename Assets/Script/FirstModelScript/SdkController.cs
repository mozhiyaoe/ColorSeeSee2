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
        // ֻ�лص�������Ԥ�������ͻ���
        StarkSDK.API.GetStarkGameRecorder().ShareVideo(SuccessCallback, FailedCallback, CancelledCallback);

        // <param name="successCallback">����ɹ��ص�</param>
        // <param name="failedCallback">����ʧ�ܻص�</param>
        // <param name="cancelledCallback">����ȡ���ص�</param>
        // <param name="title">������Ƶ�ı��⣬�粻��Ҫ���ñ��⣬���Դ�null����ַ���</param>
        // <param name="topics">������Ƶ�Ļ��⣬�粻��Ҫ���û��⣬���Դ�null����б�</param>
        //StarkSDK.API.GetStarkGameRecorder().ShareVideoWithTitleTopics(SuccessCallback, FailedCallback,
        //    CancelledCallback, "�Զ������", new List<string>() {"�Զ��廰��1", "�Զ��廰��2"});

        void SuccessCallback(Dictionary<string, object> dictionary)
        {
            Debug.Log("��Ƶ����ɹ��ص� ...");
            // �ɹ��ص��߼������磺������ʾ�����Ž���
        }

        void CancelledCallback()
        {
            Debug.Log("ȡ������ص� ...");
            // ȡ���ص��߼������磺������ʾ
        }

        void FailedCallback(string errMsg)
        {
            Debug.Log("������Ƶʧ�ܻص�ִ�� ... " + " ��������Ϣ�ǣ�" + errMsg);
            // ʧ�ܻص��߼������磺������ʾ
        }

    }
}
