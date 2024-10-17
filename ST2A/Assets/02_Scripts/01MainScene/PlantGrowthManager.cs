using UnityEngine;
using UnityEngine.SceneManagement;

public class PlantGrowthManager : MonoBehaviour
{
    public GameObject[] plants;
    private int currentPlantIndex = 0;

    private void Start()
    {
        for (int i = 0; i < plants.Length; i++)
        {
            plants[i].SetActive(i == currentPlantIndex);
        }
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform.CompareTag("Plant"))
                    {
                        HandleTouch();
                    }
                }
            }
        }
    }

    private void HandleTouch()
    {
        currentPlantIndex++;
        if (currentPlantIndex < plants.Length)
        {
            plants[currentPlantIndex - 1].SetActive(false);
            plants[currentPlantIndex].SetActive(true);
        }
        else
        {
            PlayFinalSound();
            LoadNextScene();
        }
    }

    private void PlayFinalSound()
    {
    
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
