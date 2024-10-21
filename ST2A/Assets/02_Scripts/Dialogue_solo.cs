using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Text_Pflanzung : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public List<string> anzahlTexte;
    public List<AudioClip> audioClips;
    public AudioClip buttonAudioClip;
    public float typingSpeed = 0.05f;
    public GameObject speechBubble;
    public Button nextSceneButton;
    private int currentTextIndex = 0;
    public GameObject elementToShow;
    public int numberOfTextsToShowElement;

    private Coroutine typingCoroutine;
    private bool isTyping = false;
    private bool skipTyping = false;
    private AudioSource audioSource;

    private Vector3 originalScale;
    private Color originalColor;
    private float pulseDuration = 0.2f;
    private float scaleFactor = 0.9f;
    private Color pressedColor = new Color(0.8f, 0.8f, 0.8f);

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        speechBubble.SetActive(false);
        StartCoroutine(DelaySpeechBubble(2f));
        nextSceneButton.gameObject.SetActive(false);
        nextSceneButton.onClick.AddListener(OnNextSceneButtonClick);

        originalScale = nextSceneButton.transform.localScale;
        originalColor = nextSceneButton.GetComponent<Image>().color;

        if (elementToShow != null)
            elementToShow.SetActive(false);
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

        typingCoroutine = StartCoroutine(TypeText(anzahlTexte[currentTextIndex]));
    }

    private IEnumerator TypeText(string text)
    {
        isTyping = true;
        dialogueText.text = "";

        if (currentTextIndex < audioClips.Count && audioClips[currentTextIndex] != null && audioSource != null)
        {
            audioSource.clip = audioClips[currentTextIndex];
            audioSource.Play();

            yield return new WaitForSeconds(audioSource.clip.length);
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

        yield return new WaitForSeconds(0.2f);
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        LoadNextText();
    }

    void Update()
    {
        if (isTyping && Input.GetMouseButtonDown(0))
        {
            skipTyping = true;
        }
    }

    public void LoadNextText()
    {
        currentTextIndex++;
        if (currentTextIndex < anzahlTexte.Count)
        {
            StartTyping();
        }
        else
        {
            dialogueText.text = "";
            speechBubble.SetActive(false);
            StartCoroutine(NextScene());
        }
        if (currentTextIndex == numberOfTextsToShowElement && elementToShow != null)
        {
            elementToShow.SetActive(true);
        }
    }

    public IEnumerator NextScene()
    {
        yield return new WaitForSeconds(2f);
        ShowNextSceneButton();
    }

    private void ShowNextSceneButton()
    {
        nextSceneButton.gameObject.SetActive(true);
    }

    private void OnNextSceneButtonClick()
    {
        StartCoroutine(ButtonPressAnimation());

        if (buttonAudioClip != null && audioSource != null)
        {
            audioSource.clip = buttonAudioClip;
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
