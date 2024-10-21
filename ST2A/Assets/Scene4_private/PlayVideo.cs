using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class PlayVideo : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public List<string> anzahlTexte;
    public List<AudioClip> audioClips;
    public float typingSpeed = 0.05f;
    public GameObject speechBubble;
    public Button nextSceneButton;
    public AudioSource audioSource;
    public VideoPlayer videoPlayer;

    private Vector3 originalScale;
    private Color originalColor;
    private Coroutine typingCoroutine;
    private bool isTyping = false;
    private bool skipTyping = false;

    private float pulseDuration = 0.2f;
    private float scaleFactor = 0.9f;
    private Color pressedColor = new Color(0.8f, 0.8f, 0.8f);

    void Start()
    {
        speechBubble.SetActive(false);
        StartCoroutine(DelaySpeechBubble(0.5f));
        nextSceneButton.gameObject.SetActive(false);
        nextSceneButton.onClick.AddListener(OnNextSceneButtonClick);

        originalScale = nextSceneButton.transform.localScale;
        originalColor = nextSceneButton.GetComponent<Image>().color;

        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached += VideoEnded;
        }
    }

    private IEnumerator DelaySpeechBubble(float delay)
    {
        yield return new WaitForSeconds(delay);
        speechBubble.SetActive(true);
        StartTyping();
    }

    public void StartTyping()
    {
        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        typingCoroutine = StartCoroutine(TypeText(anzahlTexte[0]));
    }

    private IEnumerator TypeText(string text)
    {
        isTyping = true;
        dialogueText.text = "";

        if (audioClips.Count > 0)
        {
            audioSource.clip = audioClips[0];
            audioSource.Play();
        }

        for (int i = 0; i < text.Length; i++)
        {
            if (skipTyping)
            {
                dialogueText.text = text;
                break;
            }

            dialogueText.text += text[i];
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
        skipTyping = false;

        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        LoadNextText();
    }

    void Update()
    {
        if (isTyping && Input.GetMouseButtonDown(0))
        {
            skipTyping = true;
        }

        if (videoPlayer.isPlaying && Input.GetMouseButtonDown(0))
        {
            videoPlayer.Stop();
            ShowNextSceneButton();
        }
    }

    public void LoadNextText()
    {
        if (anzahlTexte.Count > 1)
        {
            anzahlTexte.RemoveAt(0);
            StartTyping();
        }
        else
        {
            dialogueText.text = "";
            speechBubble.SetActive(false);
            if (videoPlayer != null)
            {
                videoPlayer.Play();
            }
        }
    }

    private void VideoEnded(VideoPlayer vp)
    {
        ShowNextSceneButton();
    }

    private void ShowNextSceneButton()
    {
        nextSceneButton.gameObject.SetActive(true);
    }

    private void OnNextSceneButtonClick()
    {
        StartCoroutine(ButtonPressAnimation());

        if (audioSource != null && audioSource.clip != null)
        {
            audioSource.Play();
            StartCoroutine(WaitForSoundAndLoadScene());
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    private IEnumerator ButtonPressAnimation()
    {
        Image buttonImage = nextSceneButton.GetComponent<Image>();
        buttonImage.color = pressedColor;

        nextSceneButton.transform.localScale = originalScale * scaleFactor;

        yield return new WaitForSeconds(pulseDuration);

        buttonImage.color = originalColor;
        nextSceneButton.transform.localScale = originalScale;
    }

    private IEnumerator WaitForSoundAndLoadScene()
    {
        yield return new WaitForSeconds(audioSource.clip.length);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void SetTypingSpeed(float speed)
    {
        typingSpeed = speed;
    }
}
