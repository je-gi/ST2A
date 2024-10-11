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
    public float typingSpeed = 0.05f;
    public GameObject speechBubble;
    public Button nextSceneButton;
    private int currentTextIndex = 0;
    private AudioSource audioSource;


    private Coroutine typingCoroutine;
    private bool isTyping = false;
    private bool skipTyping = false;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        speechBubble.gameObject.SetActive(false);
        StartCoroutine(DelaySpeechBubble(2f));
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

    private void LoadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void SetTypingSpeed(float speed)
    {
        typingSpeed = speed;
    }
}