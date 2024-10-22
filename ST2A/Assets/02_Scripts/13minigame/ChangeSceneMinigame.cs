using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneMinigame : MonoBehaviour
{
    public void ChangeToNextScene()
    {
        SceneManager.LoadScene("13_1minigame_ladenszene");  
    }
}
