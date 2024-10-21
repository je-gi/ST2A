using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Dialogue_Tomatoes : MonoBehaviour
{
    public TextMeshProUGUI dialogueText1; // Für Sprecher 1
    public TextMeshProUGUI dialogueText2; // Für Sprecher 2
    public List<string> anzahlTexte;
    public List<AudioClip> audioClips;
    public AudioClip buttonAudioClip; 
    public float typingSpeed = 0.05f;
    public GameObject speechBubble1; // Sprechblase 1
    public GameObject speechBubble2; // Sprechblase 2
    public Button nextSceneButton;

    private int currentTextIndex = 0;
    private AudioSource audioSource;
    private Coroutine typingCoroutine;
    private bool isTyping = false;
    private bool skipTyping = false;

    private Vector3 originalScale;
    private Color originalColor;
    private float pulseDuration = 0.2f;
    private float scaleFactor = 0.9f;
    private Color pressedColor = new Color(0.8f, 0.8f, 0.8f);

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        speechBubble1.SetActive(false);
        speechBubble2.SetActive(false);
        nextSceneButton.gameObject.SetActive(false);
        nextSceneButton.onClick.AddListener(OnNextSceneButtonClick);

        originalScale = nextSceneButton.transform.localScale;
        originalColor = nextSceneButton.GetComponent<Image>().color;

        StartCoroutine(DelaySpeechBubble(2f, speechBubble1, dialogueText1));
    }

    private IEnumerator DelaySpeechBubble(float delay, GameObject SpeechBubble, TextMeshProUGUI dialogueText)
    {
        yield return new WaitForSeconds(delay);
        SpeechBubble.SetActive(true);
        StartTyping(dialogueText);
    }

    public void StartTyping(TextMeshProUGUI dialogueText)
    {
        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        typingCoroutine = StartCoroutine(TypeText(anzahlTexte[currentTextIndex], dialogueText));
    }

    private IEnumerator TypeText(string text, TextMeshProUGUI dialogueText)
    {
        isTyping = true;
        dialogueText.text = "";

        if (currentTextIndex < audioClips.Count && audioClips[currentTextIndex] != null)
        {
            audioSource.clip = audioClips[currentTextIndex];
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

        yield return new WaitForSeconds(0.2f);

        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        LoadNextText(dialogueText);
    }

    void Update()
    {
        if (isTyping && Input.GetMouseButtonDown(0))
        {
            skipTyping = true;
        }
    }

    public void LoadNextText(TextMeshProUGUI currentDialogueText)
    {
        currentTextIndex++;
        if (currentTextIndex < anzahlTexte.Count)
        {
            // Wechsel zwischen Sprechblasen
            if (currentDialogueText == dialogueText1)
            {
                speechBubble1.SetActive(false);
                StartCoroutine(DelaySpeechBubble(1f, speechBubble2, dialogueText2));
            }
            else
            {
                speechBubble2.SetActive(false);
                StartCoroutine(DelaySpeechBubble(1f, speechBubble1, dialogueText1));
            }
        }
        else
        {
            speechBubble1.SetActive(false);
            speechBubble2.SetActive(false);
            StartCoroutine(NextScene());
        }
    }

    public IEnumerator NextScene()
    {
        yield return new WaitForSeconds(3f);
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
            LoadScene();
        }
    }

    private IEnumerator WaitForSoundAndLoadScene()
    {
        yield return new WaitForSeconds(buttonAudioClip.length);
        LoadScene();
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

    private void LoadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
