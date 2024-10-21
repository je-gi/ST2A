using UnityEngine;
using UnityEngine.EventSystems;

public class StoreItem : MonoBehaviour, IPointerClickHandler
{
    public string productName;  // Name des Produkts (z.B. "Reis")
    public int amount;  // Menge des Produkts, die gekauft wird
    public ShoppingCart cart;  // Referenz zum Einkaufswagen

    // Diese Methode wird aufgerufen, wenn der Spieler auf das Produkt klickt
    public void OnPointerClick(PointerEventData eventData)
    {
        // Produkt in den Einkaufswagen legen
        if (cart != null)
        {
            cart.AddToCart(productName, amount);  // Produkt und Menge an den Einkaufswagen übergeben
            Debug.Log(productName + " wurde in den Einkaufswagen gelegt.");
        }
        else
        {
            Debug.LogWarning("Kein Einkaufswagen verknüpft!");
        }
    }
}
