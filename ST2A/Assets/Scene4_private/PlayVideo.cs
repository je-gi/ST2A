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
    private int currentTextIndex = 0;
    private AudioSource audioSource;
    public VideoPlayer videoPlayer;


    private Coroutine typingCoroutine;
    private bool isTyping = false;
    private bool skipTyping = false;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        speechBubble.gameObject.SetActive(false);
        StartCoroutine(DelaySpeechBubble(0.5f));
        nextSceneButton.gameObject.SetActive(false);
        nextSceneButton.onClick.AddListener(() => LoadScene());
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

            if (videoPlayer != null)
        {
            videoPlayer.Play();
        }
        }
    }

    public IEnumerator NextScene()
    {
        yield return new WaitForSeconds(21f); //Hier die Länge des Videos einfügen
        ShowNextSceneButton();
    }

    private void ShowNextSceneButton()
    {
        nextSceneButton.gameObject.SetActive(true);
    }

    private void LoadScene()
    {
        SceneManager.LoadScene(5);
    }

    public void SetTypingSpeed(float speed)
    {
        typingSpeed = speed;
    }
}
