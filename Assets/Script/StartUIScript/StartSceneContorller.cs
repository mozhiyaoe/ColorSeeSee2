using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneContorller : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

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
