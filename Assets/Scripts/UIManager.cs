using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject[] infoAnimations;
    public Image[] dotMenus;
    //public GameObject infoscreen;

    public bool looping;

    private int currentAnimationIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        // Set the initial animation to play
        PlayAnimation(0);
        //infoscreen = GameObject.Find("InfoScreen");

    }
    void Update()
    {
        
    }

    public void QRButtonClick()
    {
        SceneManager.LoadScene("ImageTrackingWithMultiplePrefabs");
    }

    public void GlobalInfoClick()
    {
        /*
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
                        //Debug.Log("Single tap detected!");
                        GlobalInfoClick();
                        
                    }
                }
            }
        }
        if(Input.GetMouseButtonDown(0))
        {
            GlobalInfoClick();
        }
        */
        Debug.Log(currentAnimationIndex);
        // Increment the currentAnimationIndex by 1
        currentAnimationIndex++;

        // Check if the currentAnimationIndex exceeds the length of infoAnimations
        if (currentAnimationIndex >= infoAnimations.Length)
        {
            if (looping)
            {
                // If looping is enabled, reset currentAnimationIndex to 0
                currentAnimationIndex = 0;
            }
            else
            {
                // If looping is disabled, disable the GameObject and exit the function
                gameObject.SetActive(false);
                return; // Exit the function early to prevent accessing an out-of-bounds index
            }
        }

        // If the currentAnimationIndex is within bounds, play the next animation
        PlayAnimation(currentAnimationIndex);
    }

    void PlayAnimation(int index)
    {
        foreach (GameObject animationObject in infoAnimations)
        {
            animationObject.SetActive(false);
        }

        foreach (Image image in dotMenus)
        {
            image.gameObject.SetActive(false);
        }

        infoAnimations[index].SetActive(true);

        dotMenus[index].gameObject.SetActive(true);

        currentAnimationIndex = index;
    }

    public void ClickDanish(){
        SceneManager.LoadScene("StartInfo");
    }

    public void ClickEnglish(){
        SceneManager.LoadScene("StartInfo_EN");
    }

}
