using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HomeButton : MonoBehaviour
{
        public void LoadHomeScene()
    {
      SceneManager.LoadScene("00_StartScene");
    
    }
}
