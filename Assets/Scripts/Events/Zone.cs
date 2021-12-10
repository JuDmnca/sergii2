using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone : MonoBehaviour
{
    public GameObject tooltip;
    public CharacterController serge;
    public Key keyController;
    public string taskScene;
    public int taskKey;
    private bool isInZone = false;

    void Start() {
        if (tooltip) {
            tooltip.SetActive(false);
        }
        keyController = ScriptableObject.CreateInstance<Key>();
        keyController.Init(KeyCode.F, taskScene, removeTooltip);
    }

    void OnTriggerEnter(Collider other){
        if (other == serge && GameController.Instance.TaskIndex() == taskKey - 1) {
            isInZone = true;
            keyController.Enter();
            if (tooltip) {
                tooltip.SetActive(true);
            }
        }
    }

    void OnTriggerExit(Collider other){
        if (other == serge) {
            isInZone = false;
            keyController.Exit();
            if (tooltip) {
                tooltip.SetActive(false);
            }
        }
    }

    public void removeTooltip() {
        if (tooltip) {
            tooltip.SetActive(false);
        }
    }

    void Update() {
        if(isInZone) {
            keyController.Update();
        }
    }
}