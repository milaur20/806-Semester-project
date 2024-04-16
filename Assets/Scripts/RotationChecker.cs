using UnityEngine;

[System.Serializable]
public class SegmentObject
{
    public GameObject gameObject;
}

public class RotationChecker : MonoBehaviour
{
    private float initialRotationY;
    private float segmentAngle;
    public SegmentObject[] segmentObjects;

    private void Start()
    {
        // Store the initial rotation when the object is instantiated
        initialRotationY = transform.eulerAngles.y;

        // Calculate the angle for each segment
        segmentAngle = 360f / segmentObjects.Length;
    }

    private void Update()
    {
        // Calculate the total accumulated rotation angle
        float currentRotationY = transform.eulerAngles.y;
        float totalRotationAngle = (currentRotationY - initialRotationY + 360f) % 360f;

        // Calculate the segment index based on the total accumulated rotation angle
        int segmentIndex = Mathf.FloorToInt(totalRotationAngle / segmentAngle);

        // Ensure segment index stays within [0, segmentObjects.Length-1] range
        segmentIndex %= segmentObjects.Length;

        // If the segment index becomes negative, wrap it around
        if (segmentIndex < 0)
        {
            segmentIndex += segmentObjects.Length;
        }

        Debug.Log("Segment Index: " + segmentIndex);

        // Enable the GameObjects based on the segment numbers
        for (int i = 0; i < segmentObjects.Length; i++)
        {
            if (i == segmentIndex)
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