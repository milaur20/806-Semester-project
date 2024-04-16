using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnefingerRotate : MonoBehaviour
{
    private Touch touch;
    private float speedModifier = 0.1f;
    private Vector2 touchPosition;
    private Quaternion rotationY;
    void Update()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved)
            {
                rotationY = Quaternion.Euler(0f, -touch.deltaPosition.x * speedModifier, 0f);
                transform.rotation = rotationY * transform.rotation;
            }
        }
    }
}
