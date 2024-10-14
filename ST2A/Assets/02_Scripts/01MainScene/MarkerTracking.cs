using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class SimpleMarkerTracking : MonoBehaviour
{
    public ARTrackedImageManager trackedImageManager;
    public GameObject markerObject;  

    void OnEnable()
    {

        trackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    void OnDisable()
    {
    
        trackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

   
    void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        
        foreach (ARTrackedImage trackedImage in eventArgs.updated)
        {
       
            if (trackedImage.referenceImage.name == "Marker1")
            {
        
                markerObject.SetActive(true);
                markerObject.transform.position = trackedImage.transform.position;
                markerObject.transform.rotation = trackedImage.transform.rotation;
            }
        }
    }
}
