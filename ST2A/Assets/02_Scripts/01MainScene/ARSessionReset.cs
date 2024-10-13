using UnityEngine;
using UnityEngine.XR.ARFoundation;
using System.Collections;


public class ARSessionReset : MonoBehaviour
{
    public ARSession arSession;

    void Start()
    {
        ResetARSession();
    }

    public void ResetARSession()
    {
        if (arSession != null)
        {
            StartCoroutine(RestartARSession());
        }
    }

    private IEnumerator RestartARSession()
    {
        arSession.Reset();
        yield return null;
    }
}
