using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class MultipleImagesTrackingManager: MonoBehaviour
{
    private ARTrackedImageManager trackedImageManager;

    [System.Serializable]
    public struct MarkerPrefab
    {
        public string markerName;
        public GameObject prefab;
    }

    public List<MarkerPrefab> markerPrefabs = new List<MarkerPrefab>();

    private Dictionary<string, GameObject> instantiatedObjects = new Dictionary<string, GameObject>();

    void Awake()
    {
        trackedImageManager = GetComponent<ARTrackedImageManager>();
    }

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
            string markerName = trackedImage.referenceImage.name;

            if (trackedImage.trackingState == UnityEngine.XR.ARSubsystems.TrackingState.Tracking)
            {
                SpawnOrShowObject(markerName, trackedImage.transform.position, trackedImage.transform.rotation);
            }
            else
            {
                HideObject(markerName);
            }
        }
    }

    private void SpawnOrShowObject(string markerName, Vector3 position, Quaternion rotation)
    {
        GameObject prefabToSpawn = null;

        foreach (MarkerPrefab markerPrefab in markerPrefabs)
        {
            if (markerPrefab.markerName == markerName)
            {
                prefabToSpawn = markerPrefab.prefab;
                break;
            }
        }

        if (prefabToSpawn != null)
        {
            if (!instantiatedObjects.ContainsKey(markerName))
            {
                GameObject newObject = Instantiate(prefabToSpawn, position, rotation);
                instantiatedObjects.Add(markerName, newObject);
            }
            else
            {
                GameObject existingObject = instantiatedObjects[markerName];
                existingObject.SetActive(true);
                existingObject.transform.position = position;
                existingObject.transform.rotation = rotation;
            }
        }
    }

    private void HideObject(string markerName)
    {
        if (instantiatedObjects.ContainsKey(markerName))
        {
            instantiatedObjects[markerName].SetActive(false);
        }
    }
}
