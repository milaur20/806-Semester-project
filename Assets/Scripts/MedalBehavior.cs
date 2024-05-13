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
    private GameObject[] mesh;
    public List<string> textList;
    public Texture texture;

    private void Start()
    {
        //put only child of this object into variable
        obj = GetComponentInChildren<BoxCollider>().gameObject;
        int childCount = obj.transform.childCount;
        mesh = new GameObject[childCount];
        Debug.Log("Number of children: " + childCount);
        for (int i = 0; i < childCount; i++)
        {
            mesh[i] = obj.transform.GetChild(i).gameObject;
        }
    }

    void Update()
    {
        if (FocusManager.currentState == FocusedState.focused)
        {
            if (Input.touchCount > 0)
            {
                touch = Input.GetTouch(0);

                if(touch.phase == TouchPhase.Moved)
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
        }
            // Update the previous touch position for the next frame
            previousTouchPosition = touch.position;
    }

    bool IsAnyChildInView()
    {
        foreach (GameObject child in mesh)
        {
            if (IsObjectInView(child))
            {
                return true;
            }
        }
        return false;
    }

    bool IsObjectInView(GameObject obj)
    {
        GameObject childObj = obj.GetComponentInChildren<BoxCollider>().gameObject;
        Debug.Log("Child object: " + childObj);
        Renderer renderer = childObj.GetComponent<Renderer>();
        if (renderer == null) return false; // If the object doesn't have a renderer, it's not in view

        Bounds bounds = renderer.bounds;
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);

        return GeometryUtility.TestPlanesAABB(planes, bounds);
    }
}
