using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Berry : MonoBehaviour
{
    public Animator animator;
    public GameObject berry;

    private bool isWaiting = false;

    void Start()
    {
        GameController.Instance.OnNoBerry += handleNoBerry;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !GameController.Instance.MenuOpen()) {
            if (isWaiting) {
                StartCoroutine(HideBerry());

                animator.SetTrigger("TakeBerry");
                isWaiting = false;
            } else {
                StartCoroutine(ShowAndHideBerry());

                animator.SetTrigger("EatBerry");
            }
        }
    }

    public IEnumerator ShowAndHideBerry() {
        yield return new WaitForSeconds(0.8f);
        berry.SetActive(true);
        yield return new WaitForSeconds(2f);
        berry.SetActive(false);
        GameController.Instance.TakeBerry();
    }

    public IEnumerator ShowBerry() {
        yield return new WaitForSeconds(0.8f);
        berry.SetActive(true);
    }

    void handleNoBerry()
    {
        animator.SetTrigger("GetBerry");
        StartCoroutine(ShowBerry());
        StartCoroutine(CancelBerry());
        isWaiting = true;
    }

    public IEnumerator HideBerry() {
        yield return new WaitForSeconds(1.3f);
        berry.SetActive(false);
        GameController.Instance.TakeBerry();
    }

    public IEnumerator CancelBerry() {
        yield return new WaitForSeconds(5f);
        if (!GameController.Instance.IsOnBerry()) {
            isWaiting = false;
            animator.SetTrigger("CancelBerry");
            yield return new WaitForSeconds(1f);
            berry.SetActive(false);
        }
    }
}
