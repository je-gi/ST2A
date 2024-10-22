using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShoppingCart : MonoBehaviour
{
    public GameObject cartPanelContent;  // Referenz zum Content-Bereich des Einkaufswagen-UI
    public GameObject cartItemPrefab;  // Prefab für jedes Produkt in der UI
    private InventoryManager inventoryManager;

    void Start()
    {
        // Referenz auf den InventoryManager instanziieren
        inventoryManager = InventoryManager.instance;
    }

    // Methode, um Produkte in den Einkaufswagen zu legen
    public void AddToCart(string productName, int amount)
    {
        if (inventoryManager != null)
        {
            inventoryManager.AddCartItem(productName, amount);  // Synchronisiere mit dem InventoryManager
            AddProductToUI(productName, amount);  // Neues Produkt in der UI anzeigen
        }
        else
        {
            Debug.LogError("InventoryManager nicht gefunden!");
        }
    }

    // Methode, um ein neues Produkt in der UI anzuzeigen
    void AddProductToUI(string productName, int amount)
    {
        // Instanziiere ein neues UI-Element basierend auf dem Prefab
        GameObject newItem = Instantiate(cartItemPrefab, cartPanelContent.transform);

        // Suche das Text-Element im neuen UI-Objekt
        Text itemText = newItem.GetComponentInChildren<Text>();

        // Aktualisiere den Text mit dem Produktnamen und der Menge
        itemText.text = productName + ": " + amount;
    }

    // Methode, um die Menge eines Produkts in der UI zu aktualisieren
    void UpdateCartItemUI(string productName)
    {
        foreach (Transform item in cartPanelContent.transform)
        {
            Text itemText = item.GetComponentInChildren<Text>();
            if (itemText.text.Contains(productName))
            {
                itemText.text = productName + ": " + inventoryManager.cartItems[productName];  // Aktualisiere die Menge des Produkts
                break;
            }
        }
    }
}
