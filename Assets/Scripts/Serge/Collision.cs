using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Collision : MonoBehaviour
{
    private AudioSource[] audios;
    private bool isPlaying = false;
    private CharacterController serge;
    public Collider endCollider;
    public AudioSource endAudio;
    public Animator animator;
    void Start() {
        audios = GetComponents<AudioSource>();
        serge = GetComponent<CharacterController>();
    }
    void OnTriggerEnter(Collider other){
        if (other.GetType() == typeof(CharacterController) && other != serge && !isPlaying)
        {
            isPlaying = true;
            StartCoroutine(PlaySound());
        } else if (other == endCollider && GameController.Instance.TaskIndex() == 1) {
            endAudio.Play();
        }
    }

    IEnumerator PlaySound() {
        int random = Random.Range(0, audios.Length);
        audios[random].Play();
        if (random == audios.Length - 1) {
            animator.SetTrigger("Molo");
        } else {
            animator.SetTrigger("Greating");
        }
        yield return new WaitForSeconds (audios[random].clip.length);
        isPlaying = false;
    }
}