using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;  // Singleton-Instanz
    public Dictionary<string, int> fridgeItems = new Dictionary<string, int>();  // Kühlschrank-Inhalte
    public Dictionary<string, int> cartItems = new Dictionary<string, int>();  // Einkaufswagen-Inhalte

    void Awake()
    {
        // Singleton-Instanz sicherstellen
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);  // Behalte den InventoryManager bei Szenenwechsel
            InitializeFridge();  // Kühlschrankinhalte festlegen
        }
        else
        {
            Destroy(gameObject);  // Verhindere doppelte Instanzen
        }
    }

    // Initialisierung des Kühlschranks mit festen Inhalten
    void InitializeFridge()
    {
        fridgeItems.Clear();
        fridgeItems.Add("Reis", 100);
        fridgeItems.Add("Käse", 50);
        fridgeItems.Add("Zwiebeln", 2);
        fridgeItems.Add("Knoblauch", 1);
        fridgeItems.Add("Tomatenpüree", 3);
        fridgeItems.Add("Bouillon", 4);
        fridgeItems.Add("Tomaten", 3);

        Debug.Log("Kühlschrank initialisiert mit festen Inhalten.");
    }

    // Hinzufügen von Artikeln in den Einkaufswagen
    public void AddCartItem(string itemName, int amount)
{
    Debug.Log($"Versuche, {itemName} in den Einkaufswagen hinzuzufügen.");

    if (cartItems.ContainsKey(itemName))
    {
        cartItems[itemName] += amount;  // Menge erhöhen, wenn Artikel bereits im Einkaufswagen ist
        Debug.Log($"{itemName} existiert bereits im Einkaufswagen. Neue Menge: {cartItems[itemName]}");
    }
    else
    {
        cartItems[itemName] = amount;  // Neuer Artikel im Einkaufswagen
        Debug.Log($"{itemName} wurde dem Einkaufswagen hinzugefügt. Menge: {cartItems[itemName]}");
    }

    // Überprüfe den Einkaufswagen direkt nach dem Hinzufügen
    Debug.Log("Einkaufswagen nach Hinzufügen:");
    foreach (var item in cartItems)
    {
        Debug.Log($"{item.Key}: {item.Value}");
    }
}


    // Ausgabe des Inhalts von Kühlschrank und Einkaufswagen
    public void PrintInventory()
    {
        Debug.Log("Kühlschrank-Inhalte:");
        foreach (var item in fridgeItems)
        {
            Debug.Log($"{item.Key}: {item.Value}");
        }

        Debug.Log("Einkaufswagen-Inhalte:");
        foreach (var item in cartItems)
        {
            Debug.Log($"{item.Key}: {item.Value}");
        }
    }
}
