using UnityEngine;
using UnityEngine.UI;

public class IntroductionManager : MonoBehaviour
{
    public Canvas infoCanvas;       
    public Button startButton;        
    public AudioSource audioSource;   

    void Start()
    {
        infoCanvas.gameObject.SetActive(true);

        if (startButton != null)
        {
            startButton.onClick.AddListener(OnStartButtonClick);
        }
        else
        {
            Debug.LogError("Start Button ist nicht zugewiesen!");
        }

        if (audioSource == null)
        {
            Debug.LogWarning("AudioSource ist nicht zugewiesen.");
        }
    }

    void OnStartButtonClick()
    {
        if (audioSource != null)
        {
            audioSource.Play();
            Invoke("HideCanvas", audioSource.clip.length);
        }
        else
        {
          
            HideCanvas();
        }
    }

    void HideCanvas()
    {
        infoCanvas.gameObject.SetActive(false);
    }
}
