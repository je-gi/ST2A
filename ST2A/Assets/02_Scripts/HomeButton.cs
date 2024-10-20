using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HomeButton : MonoBehaviour
{
    public AudioSource buttonSound;
    
  
    public void LoadHomeScene()
    {
        StartCoroutine(PlaySoundAndLoadScene());
    }


    private IEnumerator PlaySoundAndLoadScene()
    {
        if (buttonSound != null) 
        {
            buttonSound.Play();
            
            
            yield return new WaitForSeconds(buttonSound.clip.length);
        }
        
       
        SceneManager.LoadScene("00_StartScene");
    }
}
