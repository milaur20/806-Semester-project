using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InfoScreenManager : MonoBehaviour
{
    private bool contentsEnabled = false; // Variable to track the state of the contents

    public Canvas infoScreen; 

    // Start is called before the first frame update
    void Start()
    {
    }

    void Update()
    {
    }

    public void CollectionButtonClick()
    {
        // Find the canvas GameObject with the name "InfoScreen"
        GameObject infoScreenCanvas = GameObject.Find("InfoScreen");

        if (infoScreenCanvas != null)
        {
            // Find the parent GameObject with the tag "collection" under the canvas
            GameObject collectionObject = GameObject.FindGameObjectWithTag("collection");

            // Toggle the contents of the collection object and its children
            if (collectionObject != null)
            {
                // Toggle the Image, RawImage, and TextMeshPro components of the collection object and its children
                ToggleObjectContents(collectionObject);
            }

            // Update the state for the next button click
            contentsEnabled = !contentsEnabled;
        }
    }

    // Toggle the contents (Image, RawImage, TextMeshPro) of the object and its children recursively
    private void ToggleObjectContents(GameObject obj)
    {
        // Get the Image component on the current object
        Image image = obj.GetComponent<Image>();

        // If there is an Image component, toggle its visibility
        if (image != null)
        {
            image.enabled = !contentsEnabled;
        }

        // Get the RawImage component on the current object
        RawImage rawImage = obj.GetComponent<RawImage>();

        // If there is a RawImage component, toggle its visibility
        if (rawImage != null)
        {
            rawImage.enabled = !contentsEnabled;
        }

        // Get the TextMeshPro component on the current object
        TextMeshProUGUI textMeshPro = obj.GetComponent<TextMeshProUGUI>();

        // If there is a TextMeshPro component, toggle its visibility
        if (textMeshPro != null)
        {
            textMeshPro.enabled = !contentsEnabled;
        }

        // Loop through all child objects
        foreach (Transform child in obj.transform)
        {
            // Recursively toggle the contents of Image, RawImage, and TextMeshPro components on children
            ToggleObjectContents(child.gameObject);
        }
    }
}
