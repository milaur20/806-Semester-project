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
    private GameObject oldParent;
    GameObject backgroundMask;
    public string backgroundMaskName;
    public Vector3 originalPos;
    public bool somethingIsFocused;
    private Touch touch;

    private GameObject target;
    public GameObject infoScreen;
    private float speedModifier = 0.1f;
    private Vector2 previousTouchPosition;
    private GameObject previousObject;
    public float offset = 1.0f;
    public GameObject backgroundMaskObj;

    private float lastClickTime;
    private float doubleClickTimeThreshold = 0.2f; // Adjust as needed
    private GameObject lastClickedObject;
    private int clickCount;

     private FocusedState currentState = FocusedState.idle;

    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("FocusManager started");
        backgroundMaskObj = GameObject.Find("Background Mask");
        infoScreen = GameObject.Find("InfoScreen");
        Debug.Log("InfoScreen: " + infoScreen);
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
        
        // Save the original position if it's not already saved
        if (originalPos == Vector3.zero)
        {
            originalPos = focusedObject.transform.position;
        }
        //backgroundMaskName = focusedObject.GetComponent<BackgroundMaskData>().backgroundMaskName;
        Debug.Log("BUH");
        // Activate the background mask associated with the focused object
        if(focusedObject.transform.parent != Camera.main.transform)
        {
            Debug.Log(focusedObject.transform.parent+" + "+Camera.main.transform);
            Debug.Log(focusedObject);
            Debug.Log(focusedObject.transform.parent);
            Debug.Log(focusedObject.transform.parent.gameObject);
            Debug.Log(focusedObject.transform.parent.gameObject.name);
            Debug.Log(focusedObject.name+" background");
            backgroundMask = GameObject.Find(focusedObject.name+" background");
        }
        //check if the background mask gameobject is enabled
        Debug.Log(backgroundMask);
        if (!backgroundMask.GetComponent<MeshRenderer>().enabled)
        {
            backgroundMask.GetComponent<MeshRenderer>().enabled = true;
        }
        else
        {
            Debug.LogWarning("Background mask not found for " + focusedObject.transform.parent.gameObject.name);
        }

        // Move the object to the camera's position with an offset
        if (focusedObject.transform.parent != Camera.main.transform)
        {
            oldParent = focusedObject.transform.parent.gameObject;
            focusedObject.transform.SetParent(Camera.main.transform);
            focusedObject.transform.position = Camera.main.transform.position + Camera.main.transform.forward * offset;
        }
        infoScreen.GetComponentsInChildren<CollectionBehaviour>()[0].AddToCollection(focusedObject);
        detectDoubleTap();
    }

    private void Unfocus(GameObject unFocusedObject)
    {
        Debug.Log("LetGoMedal");
        Debug.Log("Line 1");
        unFocusedObject.transform.SetParent(oldParent.transform);
        Debug.Log("Line 2");
        unFocusedObject.transform.position = originalPos;
        Debug.Log("Line 3");
        originalPos = Vector3.zero;
        Debug.Log("Line 4");
        //Debug.Log(oldParent.name);

        backgroundMask = GameObject.Find(unFocusedObject.name+" background");
        Debug.Log("Line 5");
        if (backgroundMask.GetComponent<MeshRenderer>().enabled == true)
        {
            backgroundMask.GetComponent<MeshRenderer>().enabled = false;
        }
        else
        {
            Debug.LogWarning("Background mask not found for " + unFocusedObject.transform.parent.gameObject.name);
        }
        currentState = FocusedState.idle;
        //backgroundMaskObj.SetActive(false);
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
                            target = hit.collider.gameObject.transform.parent.gameObject;
                            Debug.Log("Target: " + target);
                            Debug.Log("Switching state to unfocused");
                            currentState = FocusedState.unfocused;
                        }
                        else
                        {
                            target = hit.collider.gameObject.transform.parent.gameObject;
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