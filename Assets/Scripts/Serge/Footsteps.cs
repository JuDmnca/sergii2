using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class Footsteps : MonoBehaviour
{
    [SerializeField]
    private Movement player;
    private AudioSource _audio;
    // Start is called before the first frame update
    void Start()
    {
        _audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.IsMoving() && !_audio.isPlaying) {
            _audio.volume = Random.Range(0.8f, 1.0f);
            _audio.pitch = Random.Range(0.8f, 1.2f);

            _audio.Play();
        }
    }
}