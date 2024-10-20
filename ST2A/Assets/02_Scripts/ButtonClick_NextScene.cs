using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonClick_NextScene : MonoBehaviour
{
    public void LoadNextScene()
    {
        StartCoroutine(LoadNextSceneCoroutine());
    }

    private IEnumerator LoadNextSceneCoroutine()
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
