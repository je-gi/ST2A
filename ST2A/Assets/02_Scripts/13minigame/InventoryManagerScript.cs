using System.Collections.Generic;
using UnityEngine;

public class InventoryManagerScript : MonoBehaviour
{
    // Beispielhafte Datenstruktur für Kühlschrank- und Einkaufswagen-Inhalte
    public Dictionary<string, int> fridgeItems = new Dictionary<string, int>();
    public Dictionary<string, int> cartItems = new Dictionary<string, int>();

    void Start()
    {
        // Kühlschrank- und Einkaufswagen-Inhalte initialisieren
        InitializeInventory();
    }

    void InitializeInventory()
    {
        // Hier kannst du die Kühlschrank- und Einkaufswagen-Daten initialisieren
        fridgeItems.Add("Reis", 50);
        fridgeItems.Add("Käse", 75);
        fridgeItems.Add("Zwiebeln", 2);
        fridgeItems.Add("Knoblauch", 2);
        fridgeItems.Add("Tomatenpüree", 2);
        
        // Beispiel für das Hinzufügen von Elementen zum Einkaufswagen
        cartItems.Add("Reis", 0);
        cartItems.Add("Käse", 0);
        cartItems.Add("Zwiebeln", 0);
        cartItems.Add("Knoblauch", 0);
        cartItems.Add("Tomatenpüree", 0);
    }
}
