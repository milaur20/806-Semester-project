using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialElement
{
    public GameObject go;
}

public class TutorialScript : MonoBehaviour
{
    public List<TutorialElement> TutorialElements;
    // Start is called before the first frame update
    void Awake()
    {
        foreach(TutorialElement tutelement in TutorialElements)
        {
            Instantiate(tutelement.go);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Check if there's any touch input
        if (Input.touchCount > 0)
        {
            // Iterate through all the touches
            foreach (Touch touch in Input.touches)
            {
                // Check if the touch phase is just began
                if (touch.phase == TouchPhase.Began)
                {
                    // Check if it's a single tap with one finger
                    if (touch.tapCount == 1 && touch.fingerId == 0)
                    {
                        // Handle the single tap here
                        Debug.Log("Single tap detected!");
                    }
                }
            }
        }
    }

    public void DestroyTutorialElement(GameObject go)
    {
        Destroy(go);
    }
}
