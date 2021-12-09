using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAppear : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CancelBerry());
    }

    public IEnumerator CancelBerry() {
        yield return new WaitForSeconds(3);
        GameController.Instance.CancelBerry();
    }
}
