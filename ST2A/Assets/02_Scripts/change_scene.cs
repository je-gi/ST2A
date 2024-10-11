using UnityEngine;
using UnityEngine.SceneManagement;

public class Change_scene : MonoBehaviour
{
    // Diese Methode wechselt zur angegebenen Szene
    public void WechsleZuSzene(string szenenName)
    {
        Debug.Log("Hier käme die neue Szene");
        SceneManager.LoadScene(szenenName);
    }
}
