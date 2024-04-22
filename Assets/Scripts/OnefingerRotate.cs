using UnityEngine;

public class OnefingerRotate : MonoBehaviour
{
    private Touch touch;
    private float speedModifier = 0.1f;
    private Vector2 previousTouchPosition;

    void Update()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                Vector2 touchDeltaPosition = touch.position - previousTouchPosition;

                // Calculate rotation around the Y axis
                Quaternion yRotation = Quaternion.Euler(0f, -touchDeltaPosition.x * speedModifier, 0f);
                // Calculate rotation around the X axis
                Quaternion xRotation = Quaternion.Euler(touchDeltaPosition.y * speedModifier, 0f, 0f);

                // Apply rotations in world space
                transform.rotation = yRotation * xRotation * transform.rotation;
            }

            // Update the previous touch position for the next frame
            previousTouchPosition = touch.position;
        }
    }
}
