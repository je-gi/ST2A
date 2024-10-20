using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ARSessionResetter : MonoBehaviour
{
    private ARSession arSession;

    private void Awake()
    {
        arSession = GetComponent<ARSession>();
    }

    public void ResetARSession()
    {
        if (arSession != null)
        {
            arSession.Reset();
        }
    }
}