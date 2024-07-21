using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartSceneContorller : MonoBehaviour
{
    public Button FirstModelButton;


   

    

    void Start()
    {
       
   


        FirstModelButton.onClick.AddListener(FirstModelStart);
      






    }

    public void FirstModelStart()
    {
        SceneManager.LoadScene("FirstModel");
    }
 




}
