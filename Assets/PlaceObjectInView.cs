using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR.ARFoundation.Samples;
using UnityEngine;

public class PlaceObjectInView : MonoBehaviour
{
    public float rayDistanceModifier = 10f;

    private GameObject objectToRotate;
    public PrefabImagePairManager prefabImagePairManager;
    void Awake()
    {
        prefabImagePairManager = FindObjectOfType<PrefabImagePairManager>();
    }
    void Update()
    {
        objectToRotate = prefabImagePairManager.instantiatedPrefab;
        PlaceObject();
    }

    public void PlaceObject()
    {
        // Get the main camera
        Camera mainCamera = Camera.main;

        if (mainCamera != null)
        {
            // Cast a ray from the main camera
            Ray ray = mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

            // Modify the ray distance based on the distance modifier
            ray.direction *= rayDistanceModifier;

            // Draw the ray
            Debug.DrawRay(ray.origin, ray.direction, Color.red);
        }
        else
        {
            Debug.LogWarning("Main camera not found.");
        }
    }
}

