using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatSounds : MonoBehaviour
{
    private bool isAwake = false;
    private AudioSource[] audios;
    // Start is called before the first frame update
    void Start()
    {
        isAwake = true;
        audios = GetComponents<AudioSource>();

        SceneController.Instance.OnCloseScene += RemoveListeners;

        StartCoroutine(playAudioSequentially());
    }

    IEnumerator playAudioSequentially()
    {
        while (isAwake == true) {
            yield return null;

            for (int i = 0; i < audios.Length; i++)
            {
                audios[i].Play();

                while (audios[i].isPlaying)
                {
                    yield return null;
                }
            }
        }
    }

    void RemoveListeners(string scene) {
        isAwake = false;
    }
}
