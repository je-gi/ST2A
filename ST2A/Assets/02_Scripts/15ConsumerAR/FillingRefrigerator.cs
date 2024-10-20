using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FillingRefrigerator : MonoBehaviour
{
    public GameObject[] grayModels;
    public GameObject[] colorfulModels;
    public AudioClip switchSound;
    private AudioSource audioSource;

    private int currentModelIndex = 0;
    private float lastTapTime = 0f;
    private float doubleTapDelay = 0.3f;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        for (int i = 0; i < grayModels.Length; i++)
        {

            if (i == currentModelIndex)
            {
                grayModels[i].SetActive(true);
                colorfulModels[i].SetActive(false);
            }
            else
            {
                grayModels[i].SetActive(true); 
                colorfulModels[i].SetActive(false); 
            }
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

                if (Physics.Raycast(ray, out hit) && hit.collider.gameObject.layer == LayerMask.NameToLayer("ButtonLayer"))
                {
                    if (Time.time - lastTapTime < doubleTapDelay)
                    {
                        SwitchModel();
                    }
                    lastTapTime = Time.time;
                }
            }
        }
    }

    private void SwitchModel()
    {
        if (currentModelIndex < grayModels.Length)
        {
            grayModels[currentModelIndex].SetActive(false);
            colorfulModels[currentModelIndex].SetActive(true);

            if (audioSource != null && switchSound != null)
            {
                audioSource.PlayOneShot(switchSound);
            }

            currentModelIndex++;

            if (currentModelIndex >= grayModels.Length)
            {
                StartCoroutine(LoadNextSceneAfterDelay(5f));
            }
        }
    }

    private IEnumerator LoadNextSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
