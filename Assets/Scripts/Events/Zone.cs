using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone : MonoBehaviour
{
    public GameObject tooltip;
    public CharacterController serge;
    public Key keyController;
    public string taskScene;
    private bool isInZone = false;

    void Start() {
        tooltip.SetActive(false);
        keyController = ScriptableObject.CreateInstance<Key>();
        keyController.Init(KeyCode.F, taskScene, removeTooltip);
    }

    void OnTriggerEnter(Collider other){
        if (other == serge && GameController.Instance.TaskIndex() < 1) {
            isInZone = true;
            keyController.Enter();
            tooltip.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other){
        if (other == serge) {
            isInZone = false;
            keyController.Exit();
            tooltip.SetActive(false);
        }
    }

    public void removeTooltip() {
        tooltip.SetActive(false);
    }

    void Update() {
        if(isInZone) {
            keyController.Update();
        }
    }
}