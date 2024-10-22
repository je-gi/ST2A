using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  // Für den Szenenwechsel
using TMPro;  // Import für TextMesh Pro

public class RecipeChecker : MonoBehaviour
{
    public TextMeshProUGUI feedbackText;  // Verwende TextMeshProUGUI für TextMesh Pro
    private InventoryManager inventoryManager;  // Referenz auf den InventoryManager

    // Rezept-Daten
    private Dictionary<string, float> recipeIngredients = new Dictionary<string, float>()
    {
        { "Knoblauch", 2 },
        { "Zwiebel", 0.5f },
        { "Butter", 1 },
        { "Reis", 150 },
        { "Tomatenpüree", 3 },
        { "Bouillon", 4 },
        { "Käse", 3 },
        { "Tomaten", 2 }
    };

    void Start()
    {
        // InventoryManager über die statische Instanz referenzieren
        inventoryManager = InventoryManager.instance;

        if (inventoryManager != null)
        {
            Debug.Log("InventoryManager gefunden. Starte die Rezeptüberprüfung.");
            inventoryManager.PrintInventory();  // Debugging: Überprüfe die Inhalte vor der Überprüfung

            CheckRecipe();  // Rezeptüberprüfung starten

            inventoryManager.PrintInventory();  // Debugging: Überprüfe die Inhalte nach der Überprüfung

            // 10 Sekunden nach dem Anzeigen des Feedbacks die Szene wechseln
            Invoke("ChangeScene", 10f);
        }
        else
        {
            Debug.LogError("InventoryManager nicht gefunden!");
        }
    }

    // Rezeptüberprüfungsmethode
    void CheckRecipe()
    {
        string feedback = "Rezeptüberprüfung:\n";

        foreach (var ingredient in recipeIngredients)
        {
            string itemName = ingredient.Key;
            float requiredAmount = ingredient.Value;

            // Verfügbare Mengen aus Kühlschrank und Einkaufswagen
            float fridgeAmount = inventoryManager.fridgeItems.ContainsKey(itemName) ? inventoryManager.fridgeItems[itemName] : 0;
            float cartAmount = inventoryManager.cartItems.ContainsKey(itemName) ? inventoryManager.cartItems[itemName] : 0;

            // Gesamtmenge berechnen: Kühlschrank + Einkaufswagen
            float totalAvailable = fridgeAmount + cartAmount;
            Debug.Log($"{itemName}: Im Kühlschrank: {fridgeAmount}, im Einkaufswagen: {cartAmount}, Gesamt: {totalAvailable}");

            // Feedback generieren
            if (totalAvailable < requiredAmount)
            {
                feedback += $"{itemName}: Zu wenig vorhanden (benötigt: {requiredAmount}, verfügbar: {totalAvailable})\n";
            }
            else if (totalAvailable == requiredAmount)
            {
                feedback += $"{itemName}: Korrekt eingekauft!\n";
            }
            else
            {
                feedback += $"{itemName}: Zu viel vorhanden (benötigt: {requiredAmount}, verfügbar: {totalAvailable})\n";
            }
        }

        // Feedback im UI anzeigen
        feedbackText.text = feedback;
        Debug.Log("Rezeptüberprüfung abgeschlossen.");
    }

    // Methode, um die Szene zu wechseln
    void ChangeScene()
    {
        Debug.Log("Wechsle zur nächsten Szene...");
        SceneManager.LoadScene("14SceneCompletion");  // Name der nächsten Szene einfügen
    }
}
