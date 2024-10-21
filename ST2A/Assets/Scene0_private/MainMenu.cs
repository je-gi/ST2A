using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public AudioSource buttonSound;
    public Button playButton;
    public float scaleChangeAmount = 0.9f;
    public float scaleDuration = 0.1f;
    public Color pressedColor = Color.gray;
    private Color originalColor;
    private ColorBlock colorBlock;

    private void Start()
    {
        if (playButton != null)
        {
            originalColor = playButton.GetComponent<Image>().color;
            colorBlock = playButton.colors;
        }
    }

    public void Play()
    {
        StartCoroutine(PlaySoundAndLoadNextScene());
    }

    private IEnumerator PlaySoundAndLoadNextScene()
    {
        if (playButton != null)
        {
            ChangeButtonColor(pressedColor);
            ScaleButton(scaleChangeAmount);
            yield return new WaitForSeconds(scaleDuration);
            ChangeButtonColor(originalColor);
            ScaleButton(1f);
        }

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

    private void ChangeButtonColor(Color newColor)
    {
        if (playButton != null)
        {
            Image buttonImage = playButton.GetComponent<Image>();
            buttonImage.color = newColor;
        }
    }

    private void ScaleButton(float scaleFactor)
    {
        if (playButton != null)
        {
            playButton.transform.localScale = new Vector3(scaleFactor, scaleFactor, 1f);
        }
    }
}
