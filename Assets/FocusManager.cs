using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public enum FocusedState
    {
        idle,
        unfocused,
        focused
    }
public class FocusManager : MonoBehaviour
{
    public Vector3 originalPos;
    public bool somethingIsFocused;
    private Touch touch;

    private GameObject target;
    private bool switch1;
    private float speedModifier = 0.1f;
    private Vector2 previousTouchPosition;
    public float offset = 1.0f;
    private GameObject backgroundMaskObj;

    private float lastClickTime;
    private float doubleClickTimeThreshold = 0.2f; // Adjust as needed
    private GameObject lastClickedObject;
    private int clickCount;

     private FocusedState currentState = FocusedState.idle;

    
    // Start is called before the first frame update
    void Start()
    {
        backgroundMaskObj = GameObject.Find("Background Mask");
        backgroundMaskObj.SetActive(false);
    }

    // Update is called once per frame
    private void Update()
    {
        Debug.Log("Current state: " + currentState);
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
        }

        switch (currentState)
        {
            case FocusedState.idle:
                idle();
                break;
            case FocusedState.unfocused:
                Unfocus(target);
                break;
            case FocusedState.focused:
                Focus(target);
                break;
        }
    }

    private void Focus(GameObject focusedObject)
    {
        Debug.Log("Focusing on object");
        if(originalPos == Vector3.zero)
        {
            originalPos = focusedObject.transform.position;
        }
        Debug.Log(focusedObject.transform.parent.name+" "+"background");
        Debug.Log("M7(Clone) background");
        //Debug.Log("Background Mask: " + backgroundMaskObj);

        //find object with name M7(Clone) background
        GameObject.Find(focusedObject.name + " background").SetActive(true);

        if(focusedObject.transform.parent != Camera.main.transform)
        {
            focusedObject.transform.SetParent(Camera.main.transform);
            focusedObject.transform.position = Camera.main.transform.position + Camera.main.transform.forward * offset;
        }
        Debug.Log("hallo");
        detectDoubleTap();
    }

    private void Unfocus(GameObject unFocusedObject)
    {
        Debug.Log("LetGoMedal");
        unFocusedObject.transform.SetParent(null);
        currentState = FocusedState.idle;
        unFocusedObject.transform.position = originalPos;
        originalPos = Vector3.zero;
        GameObject.Find(unFocusedObject.name + " background").SetActive(false);
        backgroundMaskObj.SetActive(false);
    }

    private void idle()
    {
        detectDoubleTap();
    }
    private void detectDoubleTap()
    {
        if (touch.phase == TouchPhase.Began)
        {
            Debug.Log("Touch Began");
            // Perform raycast at touch position
            Ray ray = Camera.main.ScreenPointToRay(touch.position);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // Check if the hit object has the tag "medal"
                if (hit.collider.CompareTag("Medal"))
                {
                    Debug.Log("Medal hit");
                    // Check for double click on the same object
                    if (hit.collider.gameObject == lastClickedObject && Time.time - lastClickTime < doubleClickTimeThreshold)
                    {
                        clickCount++;
                    }
                    else
                    {
                        clickCount = 1;
                    }

                    // Update last clicked object and time
                    lastClickedObject = hit.collider.gameObject;
                    lastClickTime = Time.time;

                    // If double-clicked
                    if (clickCount == 2)
                    {
                        if(currentState == FocusedState.focused)
                        {
                            target = hit.collider.gameObject;
                            Debug.Log("Target: " + target);
                            Debug.Log("Switching state to unfocused");
                            currentState = FocusedState.unfocused;
                        }
                        else
                        {
                            target = hit.collider.gameObject;
                            Debug.Log("Target: " + target);
                            Debug.Log("Switching state to focused");
                            currentState = FocusedState.focused;
                        }
                        clickCount = 0; // Reset click count
                    }
                }
            }
        }
    }
}