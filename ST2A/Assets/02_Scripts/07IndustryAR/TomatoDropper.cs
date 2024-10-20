using System.Collections;
using UnityEngine;

public class TomatoDropper : MonoBehaviour
{
    public GameObject emptyBox1;
    public GameObject filledBox1;
    public GameObject emptyBox2;
    public GameObject filledBox2;
    public AudioClip boxChangeSound;
    private AudioSource audioSource;
    private float lastTapTime = 0f;
    private float doubleTapDelay = 0.3f;
    private int currentToggle = 0;

    
    private ARSceneSwitcher arSceneSwitcher;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        filledBox1.SetActive(false);
        filledBox2.SetActive(false);

       
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
                    if (hit.collider.gameObject == gameObject)
                    {
                        if (Time.time - lastTapTime < doubleTapDelay)
                        {
                            ToggleBoxes();
                        }
                        lastTapTime = Time.time;
                    }
                }
            }
        }
    }

    private void ToggleBoxes()
    {
        if (currentToggle == 0)
        {
            emptyBox1.SetActive(false);
            filledBox1.SetActive(true);
            audioSource.PlayOneShot(boxChangeSound);
            currentToggle++;
        }
        else if (currentToggle == 1)
        {
            emptyBox2.SetActive(false);
            filledBox2.SetActive(true);
            audioSource.PlayOneShot(boxChangeSound);
            StartCoroutine(LoadNextSceneAfterDelay(4f));
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
            
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
