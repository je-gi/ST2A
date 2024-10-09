using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Text_Pflanzung : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public List<string> anzahlTexte;
    public float typingSpeed = 0.05f;
    public List<int> pauseIndices; // Längere Pause einstellen
    public float longPauseDuration = 1.0f; // Dauer der langen Pause

    public GameObject speechBubble;
    private int currentTextIndex = 0;

    private Coroutine typingCoroutine;
    private bool isTyping = false;
    private bool skipTyping = false;
    void Start()
    {
        StartTyping(); // Beginnt den Prozess
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
        for (int i = 0; i < text.Length; i++)
        {
            if (skipTyping)
            {
                dialogueText.text = text;
                break;
            }

            dialogueText.text += text[i];

            if (pauseIndices.Contains(i))
            {
                yield return new WaitForSeconds(longPauseDuration);
            }
            else
            {
                yield return new WaitForSeconds(typingSpeed);
            }
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
        }
    }

    public void SetTypingSpeed(float speed)
    {
        typingSpeed = speed;
    }
}