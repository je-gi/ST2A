using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonClick_NextScene : MonoBehaviour
{
    public void LoadNextScene()
    {
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
