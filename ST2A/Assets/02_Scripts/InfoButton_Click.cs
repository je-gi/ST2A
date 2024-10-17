using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoButton_Click : MonoBehaviour
{
    public GameObject OpenMenu;

    void Start()
    {
        if (OpenMenu != null)
            OpenMenu.SetActive(false);
    }

    public void OpenInfo()
    {
        if (OpenMenu != null)
            OpenMenu.SetActive(true);
    }

    public void CloseInfo()
    {
        if (OpenMenu != null)
            OpenMenu.SetActive(false);
    }
}
