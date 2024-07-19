using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FirstModelRestartButton : MonoBehaviour
{
    public Button RestartButton;
    public Button BackToMeumButton;
    void Start()
    {
        RestartButton.onClick.AddListener(OnRestart);
        BackToMeumButton.onClick.AddListener(BackToMeum);

    }
    public void OnRestart()
    {
        SceneManager.LoadScene("FirstModel");
    }
    public void BackToMeum()
    {
        SceneManager.LoadScene("Start");
    }
}
