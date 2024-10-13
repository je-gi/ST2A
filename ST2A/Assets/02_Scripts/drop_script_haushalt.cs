using UnityEngine;
using UnityEngine.SceneManagement; // Für den Szenenwechsel

public class DropZoneObject : MonoBehaviour
{
    public string korrekteAntwort; // Der Name des korrekten Antwortobjekts
    private static int korrektZuordnungen = 0; // Zähler für korrekte Zuordnungen
    public int gesamtAnzahl = 5; // Gesamtanzahl der Antwort-Objekte, die zugeordnet werden müssen
    public GameObject erfolgsCanvas; // Das Canvas, das eingeblendet wird
    public AudioClip korrektSound; // Der Audioclip für das Feedback
    private AudioSource audioSource; // Referenz zur AudioSource

    // Hinzugefügt für den Hintergrundwechsel
    public GameObject hintergrundObjekt; // Referenz auf das GameObject mit dem SpriteRenderer
    public Sprite korrektBackground; // Das neue Sprite, das als Hintergrund verwendet wird

    void Start()
    {
        korrektZuordnungen = 0; // Setze den Zähler zu Beginn auf null
        erfolgsCanvas.SetActive(false); // Verstecke das Canvas zu Beginn

        // Hole die AudioSource-Komponente
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("AudioSource fehlt auf diesem Objekt!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Antwort")) // Überprüfen, ob das Objekt den richtigen Tag hat
        {
            DraggableObject draggable = other.GetComponent<DraggableObject>();
            if (draggable != null)
            {
                if (other.name == korrekteAntwort) // Überprüfen, ob der Name des Objekts korrekt ist
                {
                    Debug.Log("Richtig zugeordnet!");

                    // Sound abspielen
                    if (audioSource != null && korrektSound != null)
                    {
                        audioSource.PlayOneShot(korrektSound);
                    }

                    // Erhöhe den Zähler für korrekte Zuordnungen
                    korrektZuordnungen++;

                    // Überprüfe, ob alle Elemente korrekt zugeordnet sind
                    if (korrektZuordnungen >= gesamtAnzahl)
                    {
                        // Blende das Canvas ein, wenn alle zugeordnet sind
                        erfolgsCanvas.SetActive(true);
                    }

                    // Objekt an die Position des Zielbereichs setzen
                    other.transform.position = transform.position;

                    // Drag-and-Drop-Skript vollständig entfernen, um weitere Bewegungen zu verhindern
                    Destroy(draggable);

                    // Hintergrund ändern, um anzuzeigen, dass es korrekt platziert wurde
                    if (hintergrundObjekt != null && korrektBackground != null)
                    {
                        SpriteRenderer spriteRenderer = hintergrundObjekt.GetComponent<SpriteRenderer>();
                        if (spriteRenderer != null)
                        {
                            spriteRenderer.sprite = korrektBackground; // Setze den neuen Hintergrund
                        }
                        else
                        {
                            Debug.LogError("Hintergrund-Objekt hat keine SpriteRenderer-Komponente!");
                        }
                    }

                    // Rigidbody sperren, um physikalische Bewegungen zu verhindern
                    Rigidbody rb = other.GetComponent<Rigidbody>();
                    if (rb != null)
                    {
                        rb.constraints = RigidbodyConstraints.FreezeAll; // Objekt vollständig sperren
                    }
                }
                else
                {
                    Debug.Log("Falsch zugeordnet.");
                    // Setze das Objekt an die Startposition zurück
                    draggable.ResetPosition();
                }
            }
        }
    }


}
