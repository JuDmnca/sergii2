using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class Video : MonoBehaviour
{
    public VideoPlayer video;

    void Start() {
        video = GetComponent<VideoPlayer>();
        video.loopPointReached += CheckOver;
    }

    void CheckOver(VideoPlayer vp)
    {
        SceneController.Instance.OpenScene("Sergii");
        SceneController.Instance.CloseScene("Intro");
    }
}
