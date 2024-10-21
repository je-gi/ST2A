using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class IntroductionManager : MonoBehaviour
{
    public Canvas infoCanvas;       
    public Button startButton;        
    public AudioSource audioSource;   

    private Vector3 originalScale;     
    private Color originalColor;       

    private float pulseDuration = 0.2f; 
    private float scaleFactor = 0.9f;    
    private Color pressedColor = new Color(0.8f, 0.8f, 0.8f); 

    void Start()
    {
        infoCanvas.gameObject.SetActive(true);

        if (startButton != null)
        {
            originalScale = startButton.transform.localScale; 
            originalColor = startButton.GetComponent<Image>().color;

            startButton.onClick.AddListener(OnStartButtonClick);
        }

        if (audioSource == null)
        {
            Debug.LogWarning("AudioSource ist nicht zugewiesen.");
        }
    }

    void OnStartButtonClick()
    {
        StartCoroutine(ButtonPressAnimation());

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

    IEnumerator ButtonPressAnimation()
    {
        Image buttonImage = startButton.GetComponent<Image>();
        buttonImage.color = pressedColor;

        startButton.transform.localScale = originalScale * scaleFactor;

        yield return new WaitForSeconds(pulseDuration);

        buttonImage.color = originalColor;
        startButton.transform.localScale = originalScale;
    }

    void HideCanvas()
    {
        infoCanvas.gameObject.SetActive(false);
    }
}
