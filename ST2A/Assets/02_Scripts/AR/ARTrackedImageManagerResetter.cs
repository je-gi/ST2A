using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARTrackedImageManagerResetter : MonoBehaviour
{
    public ARTrackedImageManager arTrackedImageManager;
    public XRReferenceImageLibrary correctImageLibrary;

    private void Start()
    {
        
        ResetTrackedImageManager();
    }

    public void ResetTrackedImageManager()
    {
        if (arTrackedImageManager != null && correctImageLibrary != null)
        {
            
            arTrackedImageManager.enabled = false;

            
            foreach (var trackedImage in arTrackedImageManager.trackables)
            {
                Destroy(trackedImage.gameObject); 
            }

            
            arTrackedImageManager.referenceLibrary = correctImageLibrary;

            
            arTrackedImageManager.enabled = true;
        }
        else
        {
            Debug.LogError("ARTrackedImageManager or CorrectImageLibrary is not set!");
        }
    }
}
