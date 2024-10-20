using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Play()
    {
        StartCoroutine(LoadNextScene());
    }

    private IEnumerator LoadNextScene()
    {
        yield return new WaitForSeconds(1f);

        ARSceneSwitcher arSceneSwitcher = FindObjectOfType<ARSceneSwitcher>();

        if (arSceneSwitcher != null)
        {
            arSceneSwitcher.SwitchToNextScene();
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
