using System.Collections.Generic;
using UnityEngine;

public class Recipe
{
    public string recipeName;  // Name des Rezepts
    public Dictionary<string, float> ingredients;  // Zutaten und Mengen (float für Bruchteile)

    // Konstruktor, um ein neues Rezept zu erstellen
    public Recipe(string name, Dictionary<string, float> ingredientsList)
    {
        recipeName = name;
        ingredients = ingredientsList;
    }
}

public class RecipeTest : MonoBehaviour
{
    void Start()
    {
        // Beispiel für das angepasste Rezept
        Dictionary<string, float> customRecipeIngredients = new Dictionary<string, float>()
        {
            { "Knoblauch", 1 },             // 1 Knoblauchzehe
            { "Zwiebel", 0.5f },            // 0,5 Zwiebel
            { "Butter", 1 },                // 1 Esslöffel Butter
            { "Reis", 150 },                // 150 Gramm Reis
            { "Tomatenpüree", 3 },          // 3 Esslöffel Tomatenpüree
            { "Bouillon", 4 },              // 4 dl Bouillon
            { "Käse", 3 },                  // 3 Esslöffel Käse
            { "Tomaten", 2 }                // 2 Tomaten
        };

        // Rezept erstellen
        Recipe customRecipe = new Recipe("CustomRecipe", customRecipeIngredients);

        // Ausgabe der Rezeptdetails in der Konsole
        Debug.Log("Rezept: " + customRecipe.recipeName);
        foreach (var ingredient in customRecipe.ingredients)
        {
            Debug.Log(ingredient.Key + ": " + ingredient.Value);
        }
    }
}
