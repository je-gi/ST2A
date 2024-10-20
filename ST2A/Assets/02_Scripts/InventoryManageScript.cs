using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeChecker : MonoBehaviour
{
    public Text feedbackText;  // UI-Text für das Feedback

    void Start()
    {
        // InventoryManager suchen, der die Kühlschrank- und Einkaufswagen-Daten enthält
        InventoryManager inventoryManager = FindObjectOfType<InventoryManager>();

        if (inventoryManager != null)
        {
            Debug.Log("InventoryManager gefunden!");

            // Debugging: Inhalte des Kühlschranks und Einkaufswagens ausgeben
            Debug.Log("Kühlschrank-Inhalt:");
            foreach (var item in inventoryManager.fridgeItems)
            {
                Debug.Log(item.Key + ": " + item.Value);
            }

            Debug.Log("Einkaufswagen-Inhalt:");
            foreach (var item in inventoryManager.cartItems)
            {
                Debug.Log(item.Key + ": " + item.Value);
            }

            // Rezept-Daten: benötigte Zutaten und Mengen
            Dictionary<string, float> recipeIngredients = new Dictionary<string, float>()
            {
                { "Knoblauch", 1 },
                { "Zwiebel", 0.5f },
                { "Butter", 1 },
                { "Reis", 150 },
                { "Tomatenpüree", 3 },
                { "Bouillon", 4 },
                { "Käse", 3 },
                { "Tomaten", 2 }
            };

            string feedback = "Rezeptüberprüfung:\n";

            // Überprüfen, ob die richtigen Mengen eingekauft wurden
            foreach (var ingredient in recipeIngredients)
            {
                string itemName = ingredient.Key;
                float requiredAmount = ingredient.Value;

                int availableAmount = 0;

                // Überprüfen, was im Kühlschrank vorhanden ist
                if (inventoryManager.fridgeItems.ContainsKey(itemName))
                {
                    availableAmount += inventoryManager.fridgeItems[itemName];
                }

                // Überprüfen, was im Einkaufswagen ist
                if (inventoryManager.cartItems.ContainsKey(itemName))
                {
                    availableAmount += inventoryManager.cartItems[itemName];
                }

                // Feedback basierend auf der Menge
                if (availableAmount < requiredAmount)
                {
                    feedback += $"{itemName}: Zu wenig vorhanden (benötigt: {requiredAmount}, verfügbar: {availableAmount})\n";
                }
                else if (availableAmount == requiredAmount)
                {
                    feedback += $"{itemName}: Korrekt eingekauft!\n";
                }
                else
                {
                    feedback += $"{itemName}: Zu viel eingekauft (benötigt: {requiredAmount}, verfügbar: {availableAmount})\n";
                }
            }

            // Feedback im UI anzeigen
            feedbackText.text = feedback;
        }
        else
        {
            Debug.LogError("InventoryManager nicht gefunden!");
        }
    }
}
