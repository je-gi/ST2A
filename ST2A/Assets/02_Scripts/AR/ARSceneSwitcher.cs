using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ARSceneSwitcher : MonoBehaviour
{
    private ARSessionResetter arSessionResetter;

    private void Awake()
    {
        arSessionResetter = FindObjectOfType<ARSessionResetter>();
    }

    public void SwitchToNextScene()
    {
        StartCoroutine(ResetARSessionAndLoadNextScene());
    }

    private IEnumerator ResetARSessionAndLoadNextScene()
    {
  
        if (arSessionResetter != null)
        {
            arSessionResetter.ResetARSession();
        }

        
        yield return new WaitForSeconds(1f); 
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
