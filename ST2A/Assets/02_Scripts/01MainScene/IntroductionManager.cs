using UnityEngine;
using UnityEngine.UI;

public class IntroductionManager : MonoBehaviour
{
    public Canvas infoCanvas;       
    public Button startButton;        
    public AudioSource audioSource;   
    public string modelTag = "modelObject"; 

    private GameObject modelObject;   
    private bool prefabIsActive = false; 

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

    void Update()
    {
      
        modelObject = GameObject.FindWithTag(modelTag);

      
        if (IsPrefabActive() && !prefabIsActive)
        {
            prefabIsActive = true;
        }
        else if (!IsPrefabActive() && prefabIsActive)
        {
            prefabIsActive = false;
        }
    }

    void OnStartButtonClick()
    {
        if (audioSource != null)
        {
            audioSource.Play();
        }


        Invoke("HideCanvas", 0.1f); 
    }

    void HideCanvas()
    {
        infoCanvas.gameObject.SetActive(false);
    }

    bool IsPrefabActive()
    {
        return modelObject != null && modelObject.activeInHierarchy;
    }
}
