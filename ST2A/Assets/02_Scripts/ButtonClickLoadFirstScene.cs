using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonClickLoadFirstScene : MonoBehaviour
{
    public void LoadFirstScene()
    {
        StartCoroutine(LoadFirstSceneCoroutine());
    }

    private IEnumerator LoadFirstSceneCoroutine()
    {
        yield return new WaitForSeconds(1f);

            SceneManager.LoadScene(0);
    }
}
