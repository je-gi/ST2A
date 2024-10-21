using UnityEngine;
using UnityEngine.EventSystems; // Für IPointerClickHandler
using UnityEngine.UI; // Für das Text-Element

public class FridgeItem : MonoBehaviour, IPointerClickHandler
{
    public string itemName; // Der Name des Lebensmittels
    public int amount; // Die Menge des Lebensmittels im Kühlschrank
    public Text itemInfoText; // Text-UI-Element, das die Informationen anzeigt

    // Diese Methode wird aufgerufen, wenn der Spieler auf das Lebensmittel klickt
    public void OnPointerClick(PointerEventData eventData)
    {
        // Aktualisiert das Textfeld mit dem Namen und der Menge des angeklickten Lebensmittels
        itemInfoText.text = itemName + ": " + amount + " Gramm/Stück vorhanden";
        Debug.Log(itemName + " wurde geklickt"); // Zum Debuggen, um sicherzustellen, dass der Klick funktioniert
    }
}
