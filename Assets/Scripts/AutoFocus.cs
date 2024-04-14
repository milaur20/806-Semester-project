using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class AutoFocus : MonoBehaviour
{
    ARCameraManager arCameraManager;

    void Start()
    {
        arCameraManager = GetComponent<ARCameraManager>();

        if (arCameraManager == null)
        {
            Debug.LogError("ARCameraManager component not found");
        }
        else
        {
            // Enable auto-focus
            arCameraManager.autoFocusRequested = true;
        }
    }
}
