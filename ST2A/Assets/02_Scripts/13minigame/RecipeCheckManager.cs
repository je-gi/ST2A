using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeCheckManager : MonoBehaviour
{
    public Text feedbackText;  // UI-Text für das Feedback

    void Start()
    {
        // FridgeController und ShoppingCart suchen
        FridgeController fridgeController = FindObjectOfType<FridgeController>();
        ShoppingCart shoppingCart = FindObjectOfType<ShoppingCart>();

        if (fridgeController != null && shoppingCart != null)
        {
            // Debugging: Ausgabe der Inhalte von Kühlschrank und Einkaufswagen
            Debug.Log("Kühlschrank-Inhalt:");
            foreach (var item in fridgeController.fridgeInventory)
            {
                Debug.Log(item.Key + ": " + item.Value);
            }

            Debug.Log("Einkaufswagen-Inhalt:");
            foreach (var item in shoppingCart.cartItems)
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
                { "Tomatenpüree", 3 },  // Achte darauf, dass der Schlüssel hier mit fridgeInventory übereinstimmt
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
                if (fridgeController.fridgeInventory.ContainsKey(itemName))
                {
                    availableAmount += fridgeController.fridgeInventory[itemName];
                }

                // Überprüfen, was im Einkaufswagen ist
                if (shoppingCart.cartItems.ContainsKey(itemName))
                {
                    availableAmount += shoppingCart.cartItems[itemName];
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
            Debug.LogError("FridgeController oder ShoppingCart nicht gefunden!");
        }
    }
}
