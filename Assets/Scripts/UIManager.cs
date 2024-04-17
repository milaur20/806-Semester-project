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
    public VideoPlayer[] infoVideos;
    public TextMeshProUGUI[] infoTexts;
    public Image[] dotMenus;
    private RenderTexture renderTexture;

    private int currentVideoIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        // Create a new RenderTexture with the same dimensions as the RawImage
        renderTexture = new RenderTexture((int)infoVideoPlayer.rectTransform.rect.width,
                                           (int)infoVideoPlayer.rectTransform.rect.height, 0);
        renderTexture.Create();

        // Set the RenderTexture as the target texture for all VideoPlayers
        foreach (VideoPlayer videoPlayer in infoVideos)
        {
            videoPlayer.targetTexture = renderTexture;
            videoPlayer.Stop(); // Stop all videos initially
        }

        // Set the initial video player to play
        PlayVideo(0);
    }

    // Update is called once per frame
    void Update()
    {
        // Update the RawImage texture to the RenderTexture
        infoVideoPlayer.texture = renderTexture;
    }

    public void QRButtonClick()
    {
        SceneManager.LoadScene("ImageTrackingWithMultiplePrefabs");
    }

    public void GlobalInfoClick()
    {
        int nextVideoIndex = (currentVideoIndex + 1) % infoVideos.Length;
        PlayVideo(nextVideoIndex);
    }

    void PlayVideo(int index)
    {
        foreach (VideoPlayer videoPlayer in infoVideos)
        {
            videoPlayer.Stop();
            videoPlayer.gameObject.SetActive(false);
        }

        foreach (TextMeshProUGUI text in infoTexts)
        {
            text.gameObject.SetActive(false);
        }

        foreach (Image image in dotMenus){
            image.gameObject.SetActive(false);
        }

        infoVideos[index].gameObject.SetActive(true);
        infoVideos[index].Play();

        infoTexts[index].gameObject.SetActive(true);

        dotMenus[index].gameObject.SetActive(true);

        currentVideoIndex = index;
    }
}
