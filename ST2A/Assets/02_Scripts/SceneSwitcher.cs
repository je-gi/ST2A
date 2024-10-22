using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    // Methode zum Laden der Rezeptüberprüfungs-Szene
    public void LoadRecipeCheckScene()
    {
        SceneManager.LoadScene("13Minigame_schluss");  // Wechselt zur neuen Szene
    }
}
