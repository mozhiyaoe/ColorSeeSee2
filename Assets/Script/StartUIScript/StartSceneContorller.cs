using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneContorller : MonoBehaviour
{
    public void SecondModelStart()
    {
        SceneManager.LoadScene("SecondModel");
    }
    public void FirstModelStart()
    {
        SceneManager.LoadScene("FirstModel");
    }
    public void GameOut()
    {
        Application.Quit();
        Debug.Log("gameover");
    }
}
