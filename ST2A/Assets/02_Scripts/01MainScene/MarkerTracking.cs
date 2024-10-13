using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class MarkerTracking : MonoBehaviour
{
    public ARTrackedImageManager trackedImageManager;

    public GameObject marker1Object;  
    public GameObject marker2Object;  
    public GameObject marker3Object;  
    public GameObject marker4Object;

    public GameObject lockObject;   

    private Dictionary<string, bool> markerStatus = new Dictionary<string, bool>(); 

    void OnEnable()
    {
        
        trackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;

       
        markerStatus["Marker1"] = true;
        markerStatus["Marker2"] = false;
        markerStatus["Marker3"] = false;
        markerStatus["Marker4"] = false;
    }

    void OnDisable()
    {
        trackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    
    void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (ARTrackedImage trackedImage in eventArgs.updated)
        {
            string markerName = trackedImage.referenceImage.name;

            if (markerStatus.ContainsKey(markerName))
            {
                if (markerStatus[markerName])
                {
                    ActivateMarker(markerName);
                }
                else
                {
                    ShowLock(trackedImage);
                }
            }
        }
    }

    
    public void ActivateMarker(string markerName)
    {
        switch (markerName)
        {
            case "Marker1":
                marker1Object.SetActive(true);  
                lockObject.SetActive(false);    
                break;
            case "Marker2":
                marker2Object.SetActive(true);
                lockObject.SetActive(false);
                break;
            case "Marker3":
                marker3Object.SetActive(true);
                lockObject.SetActive(false);
                break;
            case "Marker4":
                marker4Object.SetActive(true);
                lockObject.SetActive(false);
                break;
        }
    }

    public void ShowLock(ARTrackedImage trackedImage)
    {
        lockObject.SetActive(true);
        lockObject.transform.position = trackedImage.transform.position; 
    }

    public void OnMarker1Interaction()
    {
        SceneManager.LoadScene("02FieldScene");
    }

    public void ReturnToMainScene()
    {
        markerStatus["Marker1"] = false;
        markerStatus["Marker2"] = true;

        SceneManager.LoadScene("01MainScene");
    }
}
