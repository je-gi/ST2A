using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FillingRefrigerator : MonoBehaviour
{
    public GameObject[] grayModels;
    public GameObject[] colorfulModels;
    public AudioClip switchSound;
    private AudioSource audioSource;
    private ARSceneSwitcher arSceneSwitcher;

    private float lastTapTime = 0f;
    private float doubleTapDelay = 0.3f;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        for (int i = 0; i < grayModels.Length; i++)
        {
            grayModels[i].SetActive(true);
            colorfulModels[i].SetActive(false);
        }
        arSceneSwitcher = FindObjectOfType<ARSceneSwitcher>();
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

                if (Physics.Raycast(ray, out hit))
                {
                    for (int i = 0; i < grayModels.Length; i++)
                    {
                        if (hit.collider.gameObject == grayModels[i])
                        {
                            if (Time.time - lastTapTime < doubleTapDelay)
                            {
                                SwitchModel(i);
                            }
                            lastTapTime = Time.time;
                        }
                    }
                }
            }
        }
    }

    private void SwitchModel(int index)
    {
        if (index < grayModels.Length)
        {
            grayModels[index].SetActive(false);
            colorfulModels[index].SetActive(true);

            if (audioSource != null && switchSound != null)
            {
                audioSource.PlayOneShot(switchSound);
            }

            bool allModelsSwitched = true;
            for (int i = 0; i < grayModels.Length; i++)
            {
                if (grayModels[i].activeSelf)
                {
                    allModelsSwitched = false;
                    break;
                }
            }

            if (allModelsSwitched)
            {
                StartCoroutine(LoadNextSceneAfterDelay(5f));
            }
        }
    }

    private IEnumerator LoadNextSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

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
