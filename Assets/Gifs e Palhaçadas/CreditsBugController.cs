using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class CreditsBugController : MonoBehaviour
{
    private SplashScreenHandler splashScreenHandler;
    public CanvasGroup waitCanvasGroup;
    public CanvasGroup targetCanvasGroup;
    public bool canPlay;
    public bool vanished;
    public bool start;
    public bool canGoToMenu;
    public bool videoPlaying;

    public float transitionDuration;
    public double currentClipDuration;

    public int videoIndex;
    public VideoClip[] orderVideoClip;
    private int amountOfVideos;
    public VideoPlayer vPlayer;

    void Awake()
    {
        splashScreenHandler = GameObject.Find("Canvas").GetComponent<SplashScreenHandler>();
        amountOfVideos = orderVideoClip.Length;
        PrepareNextVideo();
    }

    void Update()
    {
        if(start)
        {
            if(targetCanvasGroup.alpha < 1 && !vanished)
            {
                targetCanvasGroup.alpha += Time.deltaTime/3;
            }

            if(targetCanvasGroup.alpha >= 1)
            {
                canPlay = true;
                
            }

            if(canPlay && !videoPlaying)
            {
                if(vPlayer.isPrepared)
                {
                    PlayVideo();
                }
            }
        }
        else
        {
            targetCanvasGroup.alpha -= Time.deltaTime/3;
            if(targetCanvasGroup.alpha == 0 && canGoToMenu)
            {
                FindObjectOfType<SoundManager>().StopSound("Menu Music");
                FindObjectOfType<SoundManager>().StopSound("Game Music");
                splashScreenHandler.ToMenu();
                
            }
        }
        //waitCanvasGroupAlpha = waitCanvasGroup.alpha;
        //targetCanvasGroupAlpha = targetCanvasGroup.alpha;
        
    }

    public void PrepareNextVideo()
    {
        vPlayer.clip = orderVideoClip[videoIndex];
        vPlayer.Prepare();
        currentClipDuration = orderVideoClip[videoIndex].length;
    }

    public void PlayVideo()
    {
        vPlayer.Play();
        StartCoroutine(WaitClipToEnd());
        canPlay = false;
        videoPlaying = true;
    }

    public IEnumerator WaitClipToEnd()
    {
        UpdateVideoSpeed();
        yield return new WaitForSeconds((float)currentClipDuration/vPlayer.playbackSpeed);
        videoIndex++;
        PrepareNextVideo();
        //vanishing = true;
        canPlay = true;
        videoPlaying = false;
        if(videoIndex == 7)
        {
            start = false;
            canGoToMenu = true;
        }
    }

    public void StartVideoLoop()
    {
        start = true;
    }

    public void UpdateVideoSpeed()
    {
        if(videoIndex == 0)
        {
            vPlayer.playbackSpeed = 2;
        }
        else if(videoIndex == 1)
        {
            vPlayer.playbackSpeed = 1.5f;
        }
        else if(videoIndex == 2)
        {
            vPlayer.playbackSpeed = 1.5f;
        }
        else if(videoIndex == 3)
        {
            vPlayer.playbackSpeed = 1.5f;
        }
        else if(videoIndex == 4)
        {
            vPlayer.playbackSpeed = 1.5f;
        }
        else if(videoIndex == 5)
        {
            vPlayer.playbackSpeed = 1.5f;
        }
        else if(videoIndex == 6)
        {
            vPlayer.playbackSpeed = 1.5f;
        }
        else if(videoIndex == 7)
        {
            vPlayer.playbackSpeed = 1.5f;
        }
    }
}
