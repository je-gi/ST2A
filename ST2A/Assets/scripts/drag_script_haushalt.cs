using UnityEngine;

public class DraggableObject : MonoBehaviour
{
    private Vector3 startPosition; // Speichert die ursprüngliche Position des Objekts
    private Vector3 offset;
    private bool dragging = false;
    private Rigidbody rb; // Referenz zum Rigidbody

    void Awake()
    {
        // Speichere die Startposition des Objekts
        startPosition = transform.position;
    }

    void Start()
    {
        // Hol die Rigidbody-Komponente
        rb = GetComponent<Rigidbody>();

        // Stelle sicher, dass der Rigidbody vorhanden ist
        if (rb == null)
        {
            Debug.LogError("Rigidbody fehlt auf diesem Objekt!");
        }
        else
        {
            // Schwerkraft deaktivieren, damit das Objekt nicht herunterfällt
            rb.useGravity = false;

            // Drehung sperren, um das Rotieren zu verhindern
            rb.constraints = RigidbodyConstraints.FreezeRotation;
        }
    }

    void OnMouseDown()
    {
        offset = transform.position - GetMouseWorldPosition();
        dragging = true;
    }

    void OnMouseDrag()
    {
        if (dragging)
        {
            transform.position = GetMouseWorldPosition() + offset;
        }
    }

    void OnMouseUp()
    {
        dragging = false;
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = Camera.main.WorldToScreenPoint(transform.position).z;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    // Methode zum Zurücksetzen des Objekts an die Ursprungsposition
    public void ResetPosition()
    {
        if (rb != null)
        {
            // Setze die Geschwindigkeit und Drehgeschwindigkeit auf null
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            // Setze die Position des Objekts zurück
            rb.MovePosition(startPosition);
        }
        else
        {
            // Falls kein Rigidbody vorhanden ist, setze die Transform-Position zurück
            transform.position = startPosition;
        }
    }
}
