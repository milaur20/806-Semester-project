using UnityEngine;

[System.Serializable]
public class SegmentObject
{
    public GameObject gameObject;
    public int segmentNumber;
}

public class RotationChecker : MonoBehaviour
{
    private Quaternion initialRotation;
    private const int numSegments = 8;
    private float segmentAngle;
    public SegmentObject[] segmentObjects = new SegmentObject[numSegments];

    private void Start()
    {
        // Store the initial rotation when the object is instantiated
        initialRotation = transform.rotation;
        
        // Calculate the angle for each segment
        segmentAngle = 360f / numSegments;
    }

    private void Update()
    {
        // Calculate the rotation difference
        Quaternion currentRotation = transform.rotation;
        Quaternion rotationDifference = Quaternion.Inverse(initialRotation) * currentRotation;

        // Convert the rotation difference to angles
        Vector3 rotationDifferenceAngles = rotationDifference.eulerAngles;

        // Calculate the angle turned around y-axis
        float rotationAroundYAxis = rotationDifferenceAngles.z;

        // Calculate the segment index based on the angle turned
        int segmentIndex = Mathf.FloorToInt(rotationAroundYAxis / segmentAngle);

        // Enable the GameObjects based on the segment numbers
        for (int i = 0; i < segmentObjects.Length; i++)
        {
            if (segmentObjects[i].segmentNumber == segmentIndex)
            {
                segmentObjects[i].gameObject.SetActive(true);
            }
            else
            {
                segmentObjects[i].gameObject.SetActive(false);
            }
        }
    }
}
