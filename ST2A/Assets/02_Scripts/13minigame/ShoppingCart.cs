using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  // Um auf UI-Elemente zuzugreifen

public class ShoppingCart : MonoBehaviour
{
    public Dictionary<string, int> cartItems = new Dictionary<string, int>();  // Speichert die Produkte im Einkaufswagen
    public GameObject cartPanelContent;  // Referenz zum Content-Bereich des Einkaufswagen-UI
    public GameObject cartItemPrefab;  // Prefab für jedes Produkt in der UI

    // Methode, um Produkte in den Einkaufswagen zu legen
    public void AddToCart(string productName, int amount)
    {
        if (cartItems.ContainsKey(productName))
        {
            cartItems[productName] += amount;
        }
        else
        {
            cartItems[productName] = amount;
            AddProductToUI(productName, amount);  // Neues Produkt in der UI anzeigen
        }

        UpdateCartItemUI(productName);  // Aktualisiere die Anzeige des Produkts
        Debug.Log(productName + " wurde in den Einkaufswagen gelegt. Menge: " + cartItems[productName]);
    }

    // Methode, um ein neues Produkt in der UI anzuzeigen
    void AddProductToUI(string productName, int amount)
    {
        // Instanziiere ein neues UI-Element basierend auf dem Prefab
        GameObject newItem = Instantiate(cartItemPrefab, cartPanelContent.transform);  // Erstelle eine neue Zeile in der UI

        // Suche das Text-Element im neuen UI-Objekt
        Text itemText = newItem.GetComponentInChildren<Text>();  // Hier wird der Text des UI-Elements gefunden
        
        // Aktualisiere den Text mit dem Produktnamen und der Menge
        itemText.text = productName + ": " + amount;
    }

    // Methode, um die Menge eines Produkts in der UI zu aktualisieren
    void UpdateCartItemUI(string productName)
    {
        foreach (Transform item in cartPanelContent.transform)  // Gehe durch alle UI-Elemente im Einkaufswagen
        {
            Text itemText = item.GetComponentInChildren<Text>();
            if (itemText.text.Contains(productName))
            {
                itemText.text = productName + ": " + cartItems[productName];  // Aktualisiere die Menge des Produkts
                break;
            }
        }
    }
}
