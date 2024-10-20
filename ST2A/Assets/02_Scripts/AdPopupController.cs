using UnityEngine;

public class AdPopupController : MonoBehaviour
{
    public GameObject adPopupPanel1;  // Erstes Werbe-Popup
    public GameObject adPopupPanel2;  // Zweites Werbe-Popup
    public float adDuration = 3f;     // Dauer der Werbung in Sekunden
    public float adInterval1 = 10f;   // Zeitintervall zwischen Werbungen für Popup 1
    public float adInterval2 = 15f;   // Zeitintervall zwischen Werbungen für Popup 2

    private float timeSinceLastAd1 = 0f;
    private float timeSinceLastAd2 = 0f;

    void Update()
    {
        // Zählt die Zeit für das erste Popup
        timeSinceLastAd1 += Time.deltaTime;

        if (timeSinceLastAd1 >= adInterval1)
        {
            ShowAd(adPopupPanel1);
            timeSinceLastAd1 = 0f;  // Timer für Popup 1 zurücksetzen
        }

        // Zählt die Zeit für das zweite Popup
        timeSinceLastAd2 += Time.deltaTime;

        if (timeSinceLastAd2 >= adInterval2)
        {
            ShowAd(adPopupPanel2);
            timeSinceLastAd2 = 0f;  // Timer für Popup 2 zurücksetzen
        }
    }

    // Methode, um die Werbung anzuzeigen
    public void ShowAd(GameObject adPopupPanel)
    {
        adPopupPanel.SetActive(true);  // Das entsprechende Popup anzeigen
        Invoke("HideAd", adDuration);  // Verstecke das Popup nach der eingestellten Dauer
    }

    // Methode, um die Werbung auszublenden
    private void HideAd()
    {
        adPopupPanel1.SetActive(false);  // Versteckt Popup 1
        adPopupPanel2.SetActive(false);  // Versteckt Popup 2
    }
}
