using System.Collections.Generic;
using UnityEngine;

public class FridgeController : MonoBehaviour
{
    // Verknüpfungen zu den UI-Images für die Lebensmittel
    public GameObject riceImage;  // Referenz zum Reis-Sprite
    public GameObject cheeseImage;  // Referenz zum Käse-Sprite
    public GameObject onionImage;  // Referenz zum Zwiebel-Sprite
    public GameObject garlicImage;  // Referenz zum Knoblauch-Sprite
    public GameObject tomatoPureeImage;  // Referenz zum Tomatenpüree-Sprite

    // Kühlschrank-Inhalt: Die Schlüssel sind die Namen der Lebensmittel, die Werte die Mengen
    public Dictionary<string, int> fridgeInventory = new Dictionary<string, int>()
    {
        { "Reis", 50 },  // 50g Reis
        { "Käse", 75 },   // 75g Käse
        { "Zwiebeln", 2 },  // 2 Zwiebeln
        { "Knoblauch", 2 },  // 2 Knoblauchzehen
        { "Tomatenpuree", 2 }  // 2 Dosen Tomatenpüree
    };

    void Start()
    {
        // Initialisiere die Lebensmittel-Objekte mit den korrekten Werten aus dem fridgeInventory
        InitializeFridgeItems();
    }

    // Diese Methode verknüpft die Daten aus dem Dictionary mit den FridgeItem-Skripten der UI-Elemente
    void InitializeFridgeItems()
    {
        // Reis-Objekt initialisieren
        FridgeItem riceItem = riceImage.GetComponent<FridgeItem>();
        riceItem.itemName = "Reis";
        riceItem.amount = fridgeInventory["Reis"];

        // Käse-Objekt initialisieren
        FridgeItem cheeseItem = cheeseImage.GetComponent<FridgeItem>();
        cheeseItem.itemName = "Käse";
        cheeseItem.amount = fridgeInventory["Käse"];

        // Zwiebel-Objekt initialisieren
        FridgeItem onionItem = onionImage.GetComponent<FridgeItem>();
        onionItem.itemName = "Zwiebeln";
        onionItem.amount = fridgeInventory["Zwiebeln"];

        // Knoblauch-Objekt initialisieren
        FridgeItem garlicItem = garlicImage.GetComponent<FridgeItem>();
        garlicItem.itemName = "Knoblauch";
        garlicItem.amount = fridgeInventory["Knoblauch"];

        // Tomatenpüree-Objekt initialisieren
        FridgeItem tomatoPureeItem = tomatoPureeImage.GetComponent<FridgeItem>();
        tomatoPureeItem.itemName = "Tomatenpüree";
        tomatoPureeItem.amount = fridgeInventory["Tomatenpuree"];
    }
}
