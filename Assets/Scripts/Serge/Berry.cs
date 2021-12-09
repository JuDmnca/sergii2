using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Berry : MonoBehaviour
{
    public Animator animator;
    public GameObject berry;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !GameController.Instance.MenuOpen()) {
            StartCoroutine(ShowAndHideBerry());

            animator.SetTrigger("EatBerry");
            GameController.Instance.TakeBerry();
        }
    }

    public IEnumerator ShowAndHideBerry() {
        yield return new WaitForSeconds(0.8f);
        berry.SetActive(true);
        yield return new WaitForSeconds(2f);
        berry.SetActive(false);
    }
}
