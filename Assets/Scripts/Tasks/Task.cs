using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Task : MonoBehaviour {

    public IEnumerator CancelBerry() {
        yield return new WaitForSeconds(3);
        GameController.Instance.CancelBerry();
    }
}