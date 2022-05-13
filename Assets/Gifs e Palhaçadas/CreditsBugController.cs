using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class CreditsBugController : MonoBehaviour
{
    public CanvasGroup waitCanvasGroup;
    public CanvasGroup targetCanvasGroup;
    public bool canPlay;
    public bool vanishing;
    public bool vanished;

    public float transitionDuration;
    public double currentClipDuration;

    public int videoIndex;
    public VideoClip[] orderVideoClip;
    private int amountOfVideos;
    public VideoPlayer vPlayer;

    void Awake()
    {
        amountOfVideos = orderVideoClip.Length;
        PrepareNextVideo();
    }

    void Update()
    {
        //waitCanvasGroupAlpha = waitCanvasGroup.alpha;
        //targetCanvasGroupAlpha = targetCanvasGroup.alpha;
        if(targetCanvasGroup.alpha < 1 && !vanished)
        {
            targetCanvasGroup.alpha += Time.deltaTime/3;
        }

        if(targetCanvasGroup.alpha >= 1 && canPlay)
        {
            PlayVideo();
        }

        if(vanishing && !vanished)
        {
            if(targetCanvasGroup.alpha > 0)
            {
                targetCanvasGroup.alpha -= Time.deltaTime/3;
            }
            else
            {

                vanishing = false;
                vanished = true;
            }
        }
    }

    public void PrepareNextVideo()
    {
        vPlayer.clip = orderVideoClip[videoIndex];
        vPlayer.Prepare();
        currentClipDuration = orderVideoClip[videoIndex].length;

        videoIndex++;
        canPlay = true;
    }

    public void PlayVideo()
    {
        vPlayer.Play();
        canPlay = false;
        StartCoroutine(WaitClipToEnd());
    }

    public IEnumerator WaitClipToEnd()
    {
        yield return new WaitForSeconds((float)currentClipDuration/vPlayer.playbackSpeed);
        PrepareNextVideo();
        vanishing = true;
        canPlay = true;
    }
}
