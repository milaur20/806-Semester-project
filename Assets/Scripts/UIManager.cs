using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class UIManager : MonoBehaviour
{

    public RawImage infoVideoPlayer;
    public VideoPlayer infoVideo1;
    public VideoPlayer infoVideo2;
    private RenderTexture renderTexture;

    private bool isPlayingFirstVideo = true;

    // Start is called before the first frame update
    void Start()
    {
        // Create a new RenderTexture with the same dimensions as the RawImage
        renderTexture = new RenderTexture((int)infoVideoPlayer.rectTransform.rect.width, 
                                           (int)infoVideoPlayer.rectTransform.rect.height, 0);
        renderTexture.Create();

        // Set the RenderTexture as the target texture for both VideoPlayers
        infoVideo1.targetTexture = renderTexture;
        infoVideo2.targetTexture = renderTexture;

        // Set the initial video player to play
        infoVideo1.Play();
    }

    // Update is called once per frame
    void Update()
    {
        // Update the RawImage texture to the RenderTexture
        infoVideoPlayer.texture = renderTexture;
    }

    public void QRButtonClick() {
        SceneManager.LoadScene("ImageTrackingWithMultiplePrefabs");
    }

    public void GlobalInfoClick(){
        if (isPlayingFirstVideo)
        {
            // Pause the current video player and play the second one
            Debug.Log("Switching to video 2");
            infoVideo1.Pause();
            infoVideo2.Play();
            infoVideo1.gameObject.SetActive(false);
        }
        else
        {
            // Pause the current video player and play the first one
            Debug.Log("Switching to video 1");
            infoVideo2.Pause();
            infoVideo1.gameObject.SetActive(true);
            infoVideo1.Play();
        }

        isPlayingFirstVideo = !isPlayingFirstVideo;
    }
}