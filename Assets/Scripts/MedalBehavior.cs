using System;
using System.Collections.Generic;
using UnityEngine;

public class MedalBehavior : MonoBehaviour
{
    private Touch touch;
    private float speedModifier = 0.1f;
    private Vector2 previousTouchPosition;
    public Material background;
    private GameObject obj;
    public List<string> textList;
    public Sprite sprite;
    private void Start()
    {
        //put only child of this object into variable
        obj = transform.GetChild(0).gameObject;
    }

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
                obj.transform.rotation = yRotation * xRotation * obj.transform.rotation;
            }
        }

            // Update the previous touch position for the next frame
            previousTouchPosition = touch.position;
        }
    }
