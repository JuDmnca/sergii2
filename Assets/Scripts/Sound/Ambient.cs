using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Ambient : MonoBehaviour
{
    public AudioMixerSnapshot Berry;
    public AudioMixerSnapshot NoBerry;
    public AudioMixerSnapshot TransitionBerry;
    void Start()
    {
        GameController.Instance.OnEatBerry += BackToNormalMusic;
        GameController.Instance.OnNoBerry += StartSlowMusic;
    }

    void StartSlowMusic() {
        StartCoroutine(SlowMusic());
    }

    IEnumerator SlowMusic()
    {
        for (int i = 0; i < 5; i++)
        {
            float delay = Random.Range(.05f, .1f);
            TransitionBerry.TransitionTo(.1f);
            yield return new WaitForSeconds(delay);
            Berry.TransitionTo(.1f);
            yield return new WaitForSeconds(delay * Random.Range(1.5f, 2.5f));
        }
        NoBerry.TransitionTo(.5f);
    }
    void BackToNormalMusic()
    {
        Berry.TransitionTo(.5f);
    }
}
