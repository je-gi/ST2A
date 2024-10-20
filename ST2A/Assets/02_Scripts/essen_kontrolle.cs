using UnityEngine;
using UnityEngine.UI;

public class FridgeItem : MonoBehaviour
{
    public string itemName; // Der Name des Lebensmittels (z.B. "Milk")
    public int amount; // Die Menge des Lebensmittels im Kühlschrank
    public Text itemInfoText; // Text-UI-Element, um Informationen anzuzeigen

    // Diese Methode wird aufgerufen, wenn der Spieler auf das Lebensmittel klickt
    public void OnClick()
    {
        itemInfoText.text = itemName + ": " + amount + " vorhanden";
    }
}
