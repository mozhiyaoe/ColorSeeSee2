
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;
using System.Collections;
using System.Collections.Generic;
using Random = System.Random;
using Unity.VisualScripting;
using System.Drawing;
using System;
using UnityEngine.SceneManagement;
using System.Threading.Tasks.Sources;


public class FirstModelController : MonoBehaviour
{


    // 背景图片
    public Image BackgroundImage;


    // 左按钮图片
    public Image LeftButtonImage;


    // 剩余时间
    [HideInInspector]
    public float TimeLeft;

    // 倒计时文本
    public Text CountdownText;


    // 单词文本
    public Text WordText;

    // 右按钮图片
    public Image RightButtonImage;


    // 随机单词
    public Word WordRandom;

    // 单词颜色
    public static Color32 WordColor;

    // 背景颜色
    public static Color32 BackgroundColor;
    // 失败文本
    public Text FaileText;
    // 失败图片
    public Image FalieImage;
    // 重置按钮
    public Button Restart;

    // 左按钮是否正确
    [HideInInspector]
    public bool RightButtonIsRight;
    // 右按钮是否正确
    [HideInInspector]
    public bool LeftButtonIsRight;

    // 左按钮
    public Button LeftButton;
    // 右按钮
    public Button RightButton;
    // 错误音效
    public AudioSource ErrorMusic;
    // 背景音效
    public AudioSource BackgroundMusic;
    // 正确音效
    public AudioSource RightMusic;
    public Text ScoreText;
    [HideInInspector]
    public int Score = 0;
    [HideInInspector]

    public bool IsRight;
    public Button ShareButtton;


    // 颜色1
    public static Color32 Color1 = new Color32(255, 0, 0, 255);
    // 颜色2
    public static Color32 Color2 = new Color32(242, 253, 255, 255);
    // 颜色3
    public static Color32 Color3 = new Color32(255, 255, 0, 255);
    // 颜色4
    public static Color32 Color4 = new Color32(0, 255, 0, 255);
    // 颜色5
    public static Color32 Color5 = new Color32(255, 165, 0, 255);
    // 颜色6
    public static Color32 Color6 = new Color32(0, 255, 255, 255);
    // 颜色7
    public static Color32 Color7 = new Color32(128, 0, 128, 255);




    // 单词类
    public class Word
    {
        // 单词名称
        public string Name { get; set; }
        // 单词颜色
        public UnityEngine.Color Color { get; set; }
        // 构造函数
        public Word(string name, Color32 color)
        {
            Name = name;
            Color = color;
        }
    }



    // 单词1
    public static Word Word1 = new Word("红", Color1);
    // 单词2
    public static Word Word2 = new Word("白", Color2);
    // 单词3
    public static Word Word3 = new Word("黄", Color3);
    // 单词4
    public static Word Word4 = new Word("绿", Color4);
    // 单词5
    public static Word Word5 = new Word("橙", Color5);
    // 单词6
    public static Word Word6 = new Word("青", Color6);
    // 单词7
    public static Word Word7 = new Word("紫", Color7);



    // 颜色数组
    [HideInInspector]
    public Color32[] ColorArry = new Color32[] { Color1, Color2, Color3 ,Color4,
  Color5,Color6, Color7};

    // 单词数组
    public Word[] WordArry = new Word[] { Word1, Word2, Word3, Word4, Word5, Word6, Word7 };






    // 随机选择单词
    public Word WordChange(Word[] arry)
    {
        System.Random ran = new System.Random();
        int n = ran.Next(0, 7);
        return arry[n];
    }



    // 随机选择颜色
    public Color32 ColorChange(Color32[] arr)
    {
        System.Random ran = new System.Random();
        int n = ran.Next(0, 7);
        return arr[n];
    }




    // 颜色变化
    public void Coloring()
    {

        // 背景颜色变化
        BackgroundImage.color = ColorChange(ColorArry);

        // 单词变化
        WordRandom = WordChange(WordArry);


        // 确保背景颜色和单词颜色不同
        while (BackgroundImage.color == WordRandom.Color)
        {

            BackgroundImage.color = ColorChange(ColorArry);

            WordRandom = WordChange(WordArry);
        }

        // 背景颜色
        BackgroundColor = BackgroundImage.color;



        // 单词文本
        WordText.text = WordRandom.Name;

        // 单词颜色
        WordColor = WordRandom.Color;

        // 随机选择一个按钮
        Random ran = new Random();
        int n = ran.Next(1, 3);

        // 左按钮颜色
        if (n == 1)
        {
            LeftButtonImage.color = WordColor;
        }
        else
        {
            LeftButtonImage.color = BackgroundColor;
        }
        // 右按钮颜色
        if (LeftButtonImage.color == WordColor)
        {
            RightButtonImage.color = BackgroundColor;
        }
        else
        {
            RightButtonImage.color = WordColor;
        }




    }
    // 正确
    public void Right()
    {
        Coloring();
        TimeLeft = 5;
        RightMusic.Play();
        IsRight = true;



    }
    // 失败
    public void Fail()
    {



        FaileText.gameObject.SetActive(true);
        FalieImage.gameObject.SetActive(true);
        Restart.gameObject.SetActive(true);
        ShareButtton.gameObject.SetActive(true);

        TimeLeft = 0;
        ErrorMusic.Play();
        BackgroundMusic.Stop();
    }

    // 左按钮判断
    public void LeftGameJudge()
    {


        if (LeftButtonIsRight == true)
        {

            Right();

        }
        else
        {
            Fail();
        }
    }

    // 右按钮判断
    public void RightGameJudge()
    {



        if (RightButtonIsRight == true)
        {
            Right();
        }
        else
        {
            Fail();
        }






    }

    // 开始
    void Start()
    {
        TimeLeft = 5F;


        Coloring();
        FaileText.gameObject.SetActive(false);
        FalieImage.gameObject.SetActive(false);
        Restart.gameObject.SetActive(false);
        ShareButtton.gameObject.SetActive(false);
        BackgroundMusic.Play();
    }
    // 更新
    void Update()
    {
        RightButton.onClick.AddListener(RightGameJudge);
        LeftButton.onClick.AddListener(LeftGameJudge);

        CountdownText.text = TimeLeft.ToString(format: "0.00");


        TimeLeft -= Time.deltaTime;

        if (TimeLeft <= 0)
        {
            FaileText.gameObject.SetActive(true);
            FalieImage.gameObject.SetActive(true);
            Restart.gameObject.SetActive(true);
            TimeLeft = 0;

        }
        // 判断左右按钮是否正确
        if (BackgroundImage.color == RightButtonImage.color)
        {
            RightButtonIsRight = false;
            LeftButtonIsRight = true;
        }
        if (BackgroundImage.color == LeftButtonImage.color)
        {
            RightButtonIsRight = true;
            LeftButtonIsRight = false;
        }
        //自动加分
        if (IsRight == true)
        {
            Score = Score + 1;
            IsRight = false;
        }
        ScoreText.text = Score.ToString();







        


    }
}

