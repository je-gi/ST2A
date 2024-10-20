using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.ARFoundation;

public class PlantGrowthManager : MonoBehaviour
{
    public GameObject[] plants;
    public AudioClip growSound;
    public AudioClip finalSound;
    private AudioSource audioSource;

    private int currentPlantIndex = 0;
    private float lastTapTime = 0f;
    private float doubleTapDelay = 0.3f;

    private ARSceneSwitcher arSceneSwitcher;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        arSceneSwitcher = FindObjectOfType<ARSceneSwitcher>();

        for (int i = 0; i < plants.Length; i++)
        {
            plants[i].SetActive(i == currentPlantIndex);
        }
    }

    private void Update()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Ended)
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == plants[currentPlantIndex])
                {
                    if (Time.time - lastTapTime < doubleTapDelay)
                    {
                        GrowPlant();
                    }
                    lastTapTime = Time.time;
                }
            }
        }
    }

    private void GrowPlant()
    {
        plants[currentPlantIndex].SetActive(false);
        currentPlantIndex++;

        if (currentPlantIndex < plants.Length)
        {
            plants[currentPlantIndex].SetActive(true);
            audioSource.PlayOneShot(growSound);
        }
        else
        {
            audioSource.PlayOneShot(finalSound);
            StartCoroutine(LoadNextScene());
        }
    }

    private IEnumerator LoadNextScene()
    {
        yield return new WaitForSeconds(3f); 

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
