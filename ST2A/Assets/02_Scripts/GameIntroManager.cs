using UnityEngine;

public class GameIntroManager : MonoBehaviour
{
    public GameObject introPanel;  // Referenz zum Intro-Panel (Kindobjekt des Canvas)
    public GameObject fridge;  // Referenz zum Kühlschrank (Kindobjekt des Canvas)

    void Start()
    {
        // Überprüfe, ob das Intro-Panel und der Kühlschrank korrekt zugewiesen sind
        if (introPanel == null)
        {
            Debug.LogError("IntroPanel ist nicht zugewiesen!");
        }

        if (fridge == null)
        {
            Debug.LogError("Fridge ist nicht zugewiesen!");
        }

        // Das Intro-Panel zu Beginn aktivieren und den Kühlschrank verstecken
        introPanel.SetActive(true);  // Zeige das Intro-Panel
        fridge.SetActive(false);  // Verstecke den Kühlschrank
    }

    // Methode, um das Intro zu schließen
    public void CloseIntro()
    {
        Debug.Log("Intro wird geschlossen, Kühlschrank wird angezeigt.");

        // Das Intro-Panel ausblenden und den Kühlschrank anzeigen
        introPanel.SetActive(false);  // Blende das Intro-Panel aus
        fridge.SetActive(true);  // Zeige den Kühlschrank an
    }
}
