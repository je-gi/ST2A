using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    // Methode zum Laden der Rezeptüberprüfungs-Szene
    public void LoadRecipeCheckScene()
    {
        SceneManager.LoadScene("13_2minigame_schluss");  // Wechselt zur neuen Szene
    }
}
