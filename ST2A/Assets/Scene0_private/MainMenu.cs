using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioSource buttonSound; 

    public void Play()
    {
        StartCoroutine(PlaySoundAndLoadNextScene());
    }

    private IEnumerator PlaySoundAndLoadNextScene()
    {
        
        if (buttonSound != null)
        {
            buttonSound.Play(); 
            yield return new WaitForSeconds(buttonSound.clip.length); 
        }
        else
        {
           
            yield return new WaitForSeconds(1f);
        }

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
