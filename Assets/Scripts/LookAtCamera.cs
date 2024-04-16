using UnityEngine;
using UnityEngine.XR.ARFoundation.Samples;


public class LookAtMainCamera : MonoBehaviour
{
    private GameObject objectToRotate;
    public PrefabImagePairManager prefabImagePairManager;
    void Awake()
    {
        prefabImagePairManager = FindObjectOfType<PrefabImagePairManager>();
    }
    void Update()
    {
        objectToRotate = prefabImagePairManager.instantiatedPrefab;
    }
    public void lookAtMainCamera()
    {
        // Get the main camera
        Camera mainCamera = Camera.main;

        if (mainCamera != null)
        {
            if(objectToRotate != null)
            {
                // Calculate the direction from this GameObject to the main camera
                Vector3 directionToCamera = mainCamera.transform.position - objectToRotate.transform.position;

                // Set the rotation of the instantiated prefab to look directly at the main camera
                objectToRotate.transform.rotation = Quaternion.LookRotation(directionToCamera, Vector3.up);

            }
            else
            {
                Debug.Log("Object to rotate not set.");
            }
            
        }
        else
        {
            Debug.Log("Main camera not found.");
        }
    }
}
