using UnityEngine;

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
    public float offset = 1.0f;
    public GameObject backgroundMaskObj;
    private float lastClickTime;
    private float doubleClickTimeThreshold = 0.2f; // Adjust as needed
    private GameObject lastClickedObject;
    private int clickCount;
    static public FocusedState currentState = FocusedState.idle;
    [SerializeField] private bool debugging;

    void Start()
    {
        if(debugging)
        {
            Debug.Log("FocusManager started");
            Debug.Log("InfoScreen: " + infoScreen);
        }
        Vibration.Init();
        backgroundMaskObj = GameObject.Find("Background Mask");
        infoScreen = GameObject.Find("InfoScreen");
    }

    private void Update()
    {
        if(debugging)
        {
            Debug.Log("Current state: " + currentState);
        }
        
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
        
        // Save the original position if it's not already saved
        if (originalPos == Vector3.zero)
        {
            originalPos = focusedObject.transform.position;
        }

        //backgroundMaskName = focusedObject.GetComponent<BackgroundMaskData>().backgroundMaskName;

        // Activate the background mask associated with the focused object
        if(focusedObject.transform.parent != Camera.main.transform)
        {
            backgroundMask = GameObject.Find(focusedObject.name+" background");
        }

        //check if the background mask gameobject is enabled
        if (!backgroundMask.GetComponent<MeshRenderer>().enabled)
        {
            backgroundMask.GetComponent<MeshRenderer>().enabled = true;
        }
        else
        {
            if(debugging)
            {
                Debug.Log("Background mask already enabled for " + focusedObject.transform.parent.gameObject.name);
            }
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
        unFocusedObject.transform.SetParent(oldParent.transform);
        unFocusedObject.transform.position = originalPos;
        originalPos = Vector3.zero;

        backgroundMask = GameObject.Find(unFocusedObject.name+" background");
        if (backgroundMask.GetComponent<MeshRenderer>().enabled == true)
        {
            backgroundMask.GetComponent<MeshRenderer>().enabled = false;
        }
        else
        {
            if(debugging)
            {
                Debug.LogWarning("Background mask not found for " + unFocusedObject.transform.parent.gameObject.name);
            }
        }
        currentState = FocusedState.idle;
    }

    private void idle()
    {
        detectDoubleTap();
    }
    private void detectDoubleTap()
    {
        if (touch.phase == TouchPhase.Began)
        {
            if(debugging)
            {
                Debug.Log("Touch Began");
            }

            // Perform raycast at touch position
            Ray ray = Camera.main.ScreenPointToRay(touch.position);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // Check if the hit object has the tag "medal"
                if (hit.collider.CompareTag("Medal"))
                {
                    if(debugging)
                    {
                        Debug.Log("Medal hit");
                    }
                    
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
                            Vibration.VibratePeek();
                            if(debugging)
                            {
                                Debug.Log("Target: " + target);
                                Debug.Log("Switching state to unfocused");
                            }
                            currentState = FocusedState.unfocused;
                        }
                        else
                        {
                            target = hit.collider.gameObject.transform.parent.gameObject;
                            Vibration.VibrateNope();
                            if(debugging)
                            {
                                Debug.Log("Target: " + target);
                                Debug.Log("Switching state to focused");
                            }
                            currentState = FocusedState.focused;
                        }
                        clickCount = 0; // Reset click count
                    }
                }
            }
        }
    }
}