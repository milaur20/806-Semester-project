using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject[] infoAnimations;
    public TextMeshProUGUI[] infoTexts;
    public Image[] dotMenus;

    private int currentAnimationIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        // Set the initial animation to play
        PlayAnimation(0);
    }

    public void QRButtonClick()
    {
        SceneManager.LoadScene("ImageTrackingWithMultiplePrefabs");
    }

    public void GlobalInfoClick()
    {
        int nextAnimationIndex = (currentAnimationIndex + 1) % infoAnimations.Length;
        PlayAnimation(nextAnimationIndex);
    }

    void PlayAnimation(int index)
    {
        foreach (GameObject animationObject in infoAnimations)
        {
            animationObject.SetActive(false);
        }

        foreach (TextMeshProUGUI text in infoTexts)
        {
            text.gameObject.SetActive(false);
        }

        foreach (Image image in dotMenus)
        {
            image.gameObject.SetActive(false);
        }

        infoAnimations[index].SetActive(true);

        infoTexts[index].gameObject.SetActive(true);

        dotMenus[index].gameObject.SetActive(true);

        currentAnimationIndex = index;
    }
}
