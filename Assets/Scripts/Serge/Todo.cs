using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Todo : MonoBehaviour
{
    public Animator animator;
    public GameObject menu;
    private bool menuOpen = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T)) {
            menuOpen = !menuOpen;

            if (menuOpen) {
                GameController.Instance.SetMenuOpen(true);
                animator.SetBool("menuOpen", true);
                animator.SetTrigger("OpenMenu");
                StartCoroutine(ShowMenu());
            } else {
                GameController.Instance.SetMenuOpen(false);
                animator.SetBool("menuOpen", false);
                animator.SetTrigger("CloseMenu");
                StartCoroutine(HideMenu());
            }
        }
    }

    public IEnumerator ShowMenu() {
        yield return new WaitForSeconds(1.4f);
        menu.SetActive(true);
    }
    public IEnumerator HideMenu() {
        yield return new WaitForSeconds(2f);
        menu.SetActive(false);
    }
}
