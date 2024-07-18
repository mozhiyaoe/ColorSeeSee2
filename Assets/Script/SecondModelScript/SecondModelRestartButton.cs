using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SecondModelRestartButton : MonoBehaviour
{
   public void OnRestart()
    {
        SceneManager.LoadScene("FirstModel");
    }
}
