using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : ScriptableObject
{

    public bool isListening = true;
    public KeyCode key = KeyCode.F;
    public string taskScene;
    public System.Action callback;

    public void Init(KeyCode keyCode, string sceneName, System.Action keyCallback) {
        key = keyCode;
        callback = keyCallback;
        taskScene = sceneName;
    }
    public void Update() {
        if(isListening && Input.GetKeyDown(key)) {
            callback();
            SceneController.Instance.TransitionScene(new string[] {"Sergii"}, new string[] {taskScene});
        }
    }

    public void Enter() {
        isListening = true;
    }

    public void Exit() {
        isListening = false;
    }

}